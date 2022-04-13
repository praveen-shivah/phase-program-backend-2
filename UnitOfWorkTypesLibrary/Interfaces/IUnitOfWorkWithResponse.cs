namespace UnitOfWorkTypesLibrary
{
    public interface IUnitOfWorkWithResponse<T>
    {
        void AddWorkItem(IWorkItem workItem);

        T Execute();
    }
}