namespace UnitOfWorkTypesLibrary
{
    public interface IUnitOfWork
    {
        void AddWorkItem(IWorkItem workItem);

        void AddWorkItems(IWorkItem[] workItems);

        WorkItemResultEnum Execute();
    }
}