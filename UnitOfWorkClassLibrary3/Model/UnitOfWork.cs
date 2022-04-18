namespace UnitOfWorkClassLibrary
{
    using System;
    using System.Threading.Tasks;

    using LoggingLibrary;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;

    using UnitOfWorkTypesLibrary;

    public sealed class UnitOfWork<T> : IUnitOfWork where T : DbContext
    {
        private readonly ILogger logger;
        private readonly IUnitOfWorkContextContainer<T> unitOfWorkContextContainer;
        private readonly IWorkItemList workItemList = new WorkItemList();

        public UnitOfWork(ILogger logger, IUnitOfWorkContextContainer<T> unitOfWorkContextContainer)
        {
            this.logger = logger;
            this.unitOfWorkContextContainer = unitOfWorkContextContainer;
        }

        void IUnitOfWork.AddWorkItem(IWorkItem workItem)
        {
            this.workItemList.AddWorkItem(workItem);
        }

        void IUnitOfWork.AddWorkItems(IWorkItem[] workItems)
        {
            this.workItemList.AddWorkItems(workItems);
        }

        async Task<WorkItemResultEnum> IUnitOfWork.ExecuteAsync()
        {
            return await this.executeUnitOfWork();
        }

        private WorkItemResultEnum doUnitOfWork(DbContext context)
        {
            foreach (var item in this.workItemList)
            {
                var result = item.Execute(context);
                if (result != WorkItemResultEnum.doneContinue)
                {
                    return result;
                }
            }

            return WorkItemResultEnum.commitSuccessfullyCompleted;
        }

        private async Task<WorkItemResultEnum> executeUnitOfWork()
        {
            var result = WorkItemResultEnum.notSet;

            // If a concurrency exception occurs, then the top unit of work will rerun it.
            // For nested Units of work
            // Only one transaction will be Started
            byte retriesRemaining = 3;

            while (retriesRemaining > 0)
            {
                IDbContextTransaction dbContextTransaction;
                if (this.unitOfWorkContextContainer.CurrentDbContext.Database.CurrentTransaction != null)
                {
                    dbContextTransaction = this.unitOfWorkContextContainer.CurrentDbContext.Database.CurrentTransaction;
                }
                else
                {
                    dbContextTransaction = this.unitOfWorkContextContainer.CurrentDbContext.Database.BeginTransaction();
                    this.unitOfWorkContextContainer.NumberOfTransactions++;
                }

                try
                {
                    result = this.doUnitOfWork(this.unitOfWorkContextContainer.CurrentDbContext);
                    switch (result)
                    {
                        case WorkItemResultEnum.cancelWithoutError:
                        case WorkItemResultEnum.rollbackExit:
                            await this.rollBack(dbContextTransaction);
                            this.unitOfWorkContextContainer.Refresh();
                            break;
                        case WorkItemResultEnum.doneContinue:
                        case WorkItemResultEnum.commitExit:
                        case WorkItemResultEnum.commitSuccessfullyCompleted:
                            await this.unitOfWorkContextContainer.CurrentDbContext.SaveChangesAsync();
                            if (this.unitOfWorkContextContainer.NumberOfTransactions == 1)
                            {
                                await dbContextTransaction.CommitAsync();
                            }

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                }
                catch (DbUpdateConcurrencyException)
                {
                    await this.rollBack(dbContextTransaction);
                    this.unitOfWorkContextContainer.Refresh();
                    retriesRemaining--;
                    result = WorkItemResultEnum.concurrencyError;
                }

                // catch (SqlException ex)
                // {
                //    result = WorkItemResultEnum.exceptionError;
                //    if (ex.ErrorCode == 1205)
                //    {
                //        this.rollBack(dbContextTransaction);
                //        this.unitOfWorkContextContainer.Refresh();
                //        retriesRemaining--;
                //        result = WorkItemResultEnum.deadlockError;
                //    }
                // }
                catch (Exception ex)
                {
                    if (ex.Message.ToUpper()
                            .Contains("DEADLOCK") || (ex.InnerException != null && ex.InnerException.Message.ToUpper()
                            .Contains("DEADLOCK")))
                    {
                        result = WorkItemResultEnum.deadlockError;
                        this.unitOfWorkContextContainer.Refresh();
                        retriesRemaining--;
                    }
                    else
                    {
                        await this.rollBack(dbContextTransaction);
                        throw;
                    }
                }
                finally
                {
                    this.unitOfWorkContextContainer.Refresh();
                }
            }

            if (result != WorkItemResultEnum.commitSuccessfullyCompleted && result != WorkItemResultEnum.cancelWithoutError)
            {
                 this.logger.Info(LogClass.General, $"UnitOfWork error: {result} ");
            }

            return result;
        }

        private async Task rollBack(IDbContextTransaction dbContextTransaction)
        {
            try
            {
                if (dbContextTransaction == null)
                {
                    return;
                }

                await dbContextTransaction.RollbackAsync();
            }
            catch
            {
            }
        }
    }
}