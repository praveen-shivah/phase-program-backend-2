namespace UnitOfWorkTypesLibrary
{
    using System;

    using Microsoft.EntityFrameworkCore;

    public interface IEntityContextFrameWorkFactory<T> : IDisposable where T : DbContext
    {
        T CreateContext(string dbName = null);
    }
}
