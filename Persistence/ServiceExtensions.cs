using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repository;

namespace Persistence
{
    /// <ServiceExtensions>
    /// In this class we will create the connection with the database, which will be our data persistence
    /// for any call or flow that we want to make in the database.
    /// </ServiceExtensions>
    public static class ServiceExtensions
    {
        /// <AddPersistenceInfraestructure>
        /// The connection with the database is called and also the migration of classes
        /// </AddPersistenceInfraestructure>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddPersistenceInfraestructure(this IServiceCollection services,IConfiguration configuration)
        {
            ///<GetConnectionString>
            ///The database connection string that is called in DefaultConnection is called from the Program which in turn calls from the appsettings.json
            ///<GetConnectionString>
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b=>b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            ///<AddTransient>
            ///As this is a pattern that generates generic repositories depending on the class that is passed, 
            ///therefore a dependency injection is made where the IRepositoryAsync type will implement a MyRepositoryAsync, 
            ///no matter the type of dependency that is passed to it, it will always be implemented with these first two
            ///</AddTransient>
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
        }
    }
}
