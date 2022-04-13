namespace UnitOfWorkTypesLibrary
{
    using Microsoft.EntityFrameworkCore;

    public interface IUnitOfWorkContextContainerFactory<T> where T : DbContext
    {
        IUnitOfWorkContextContainer<T> CreateContainer();
    }
}