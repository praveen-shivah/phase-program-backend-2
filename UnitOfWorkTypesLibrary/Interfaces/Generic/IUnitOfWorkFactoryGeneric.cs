namespace UnitOfWorkTypesLibrary
{
    using System;

    using Microsoft.EntityFrameworkCore;

    public interface IUnitOfWorkFactoryGeneric<T> where T : DbContext
    {
        IUnitOfWork Create(Func<T, WorkItemResultEnum> function);
    }
}
