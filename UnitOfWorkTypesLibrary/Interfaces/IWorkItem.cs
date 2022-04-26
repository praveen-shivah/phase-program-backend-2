namespace UnitOfWorkTypesLibrary
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public enum WorkItemResultEnum
    {
        notSet,

        doneContinue,

        cancelWithoutError,

        rollbackExit,

        commitExit,

        commitSuccessfullyCompleted,

        deadlockError,

        concurrencyError,

        exceptionError
    }

    public interface IWorkItem
    {
        Task<WorkItemResultEnum> ExecuteAsync(DbContext context);
    }
}