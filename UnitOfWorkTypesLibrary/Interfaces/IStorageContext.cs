namespace UnitOfWorkTypesLibrary
{
    using System;

    public interface IStorageContext : IDisposable
    {
        void CompleteTransaction();

        IDisposable StartTransaction();
    }
}