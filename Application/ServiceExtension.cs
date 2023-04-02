using Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    /// <ServiceExtension>
    /// ServiceExtension method is like the mediator where the project will call all the 
    /// methods created in this layer so they must be enrolled or called in this class.
    /// </ServiceExtension>
    public static class ServiceExtension
    {
        /// <AddApplicationLayer>
        /// All the methods that we will use in this layer must be enrolled or called.
        /// </AddApplicationLayer>
        /// <param name="services"></param>
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            //Automatically records the mappings made in the class biobibliotheca
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //Allows to make the project reference
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //We register or link to the ValidationBehaviours class in order to have our validators.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviours<,>));
        }
    }
}
