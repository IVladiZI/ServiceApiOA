using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Extension
{
    /// <ServiceExtensions>
    /// This class is the enroller with which we will call the versioning of controllers from the program.
    /// </ServiceExtensions>
    public static class ServiceExtension
    {
        /// <AddApiVersioningExtension>
        /// We place in this method the necessary configurations for the controllers
        /// </AddApiVersioningExtension>
        /// <param name="services"></param>
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                //Version API
                config.DefaultApiVersion = new ApiVersion(1, 0);
                //We indicate that if the client does not send us the Api version, we take as default the version 1
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}
