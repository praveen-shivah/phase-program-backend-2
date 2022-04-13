namespace UnitOfWorkTypesLibrary
{
    using System.Collections.Generic;

    public interface IWorkItemList : IList<IWorkItem>
    {
        void AddWorkItem(IWorkItem workItem);
        void AddWorkItems(IWorkItem[] workItems);
    }
}