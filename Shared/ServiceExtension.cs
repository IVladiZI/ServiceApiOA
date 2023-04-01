using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <ServiceExtension>
    /// In this class we will create the connection with the database, which will be our data persistence
    /// for any call or flow that we want to make in the database.
    /// </ServiceExtension>
    public static class ServiceExtension
    {
        /// <summary>
        /// The connection with the database is called and also the migration of classes
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSharedInfraestructure(this IServiceCollection services,IConfiguration configuration)
        {
            ///<AddTransient>
            ///As this is a pattern that generates generic repositories depending on the class that is passed, 
            ///therefore a dependency injection is made where the IRepositoryAsync type will implement a MyRepositoryAsync, 
            ///no matter the type of dependency that is passed to it, it will always be implemented with these first two
            ///</AddTransient>
            services.AddTransient<IDateTimeService, DateTimeServices>();
        }
    }
}
