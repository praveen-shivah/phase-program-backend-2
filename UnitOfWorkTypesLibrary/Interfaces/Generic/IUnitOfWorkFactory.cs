namespace UnitOfWorkTypesLibrary
{
    using System;

    using Microsoft.EntityFrameworkCore;

    public interface IUnitOfWorkFactory<T> where T : DbContext
    {
        IUnitOfWork Create(Func<T, WorkItemResultEnum> function);
    }
}
