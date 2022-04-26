namespace UnitOfWorkTypesLibrary
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        void AddWorkItem(IWorkItem workItem);

        void AddWorkItems(IWorkItem[] workItems);

        Task<WorkItemResultEnum> ExecuteAsync();
    }
}