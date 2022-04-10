namespace DataCore3EFClassLibrary
{
    using System;

    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.DependencyInjection;

    public class ScaffoldingDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection services)
        {
            try
            {
                var options = ReverseEngineerOptions.DbContextAndEntities;
                services.AddHandlebarsScaffolding(options);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}