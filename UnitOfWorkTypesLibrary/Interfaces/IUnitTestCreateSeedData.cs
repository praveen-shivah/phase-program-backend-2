namespace UnitOfWorkTypesLibrary
{
    using Microsoft.EntityFrameworkCore;

    public interface IUnitTestCreateSeedData
    {
        void Create(DbContext context);
    }
}
