namespace UnitOfWorkClassLibrary
{
    using System.Collections;
    using System.Collections.Generic;

    using UnitOfWorkTypesLibrary;

    public class WorkItemList : IWorkItemList
    {
        private readonly IList<IWorkItem> list = new List<IWorkItem>();

        int ICollection<IWorkItem>.Count => this.list.Count;

        bool ICollection<IWorkItem>.IsReadOnly => false;

        IWorkItem IList<IWorkItem>.this[int index]
        {
            get => this.list[index];
            set => this.list[index] = value;
        }

        void ICollection<IWorkItem>.Add(IWorkItem item)
        {
            this.list.Add(item);
        }

        void IWorkItemList.AddWorkItem(IWorkItem workItem)
        {
            this.list.Add(workItem);
        }

        void IWorkItemList.AddWorkItems(IWorkItem[] workItems)
        {
            foreach (var workItem in workItems)
            {
                this.list.Add(workItem);
            }
        }

        void ICollection<IWorkItem>.Clear()
        {
            this.list.Clear();
        }

        bool ICollection<IWorkItem>.Contains(IWorkItem item)
        {
            return this.list.Contains(item);
        }

        void ICollection<IWorkItem>.CopyTo(IWorkItem[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        IEnumerator<IWorkItem> IEnumerable<IWorkItem>.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        int IList<IWorkItem>.IndexOf(IWorkItem item)
        {
            return this.list.IndexOf(item);
        }

        void IList<IWorkItem>.Insert(int index, IWorkItem item)
        {
            this.list.Insert(index, item);
        }

        bool ICollection<IWorkItem>.Remove(IWorkItem item)
        {
            return this.list.Remove(item);
        }

        void IList<IWorkItem>.RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }
    }
}