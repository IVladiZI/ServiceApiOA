using WebAPI.Middlewares;

namespace WebAPI.Extension
{
    /// <AppExtensions>
    /// With this class we call our ErrorHandlerMiddleware that we created and inject it 
    /// into the UseMiddleware and then call it from the program.
    /// </AppExtensions>
    public static class AppExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app) 
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
