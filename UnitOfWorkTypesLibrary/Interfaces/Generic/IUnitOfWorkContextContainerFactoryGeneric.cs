namespace UnitOfWorkTypesLibrary
{
    using Microsoft.EntityFrameworkCore;

    public interface IUnitOfWorkContextContainerFactoryGeneric<T> where T : DbContext
    {
        IUnitOfWorkContextContainerGeneric<T> CreateContainer();
    }
}