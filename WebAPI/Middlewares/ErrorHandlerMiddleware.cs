using Application.Wrappres;
using System.Net;
using System.Text.Json;

namespace WebAPI.Middlewares
{
    /// <ErrorHandlerMiddleware>
    /// This class will help us to capture errors in the API query 
    /// before it reaches the core, http type errors will be captured.
    /// </ErrorHandlerMiddleware>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// In this method we customize the type of error that the API will return when receiving a 
        /// request from the client, we will use the standard Response that we created to map the errors 
        /// and we will return them in json format, this helps to control all the errors of type API.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel =new Response<string>() { Success  = false, Message = error?.Message, };

                switch (error) 
                {   
                    case Application.Exceptions.ApiException ex:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case Application.Exceptions.ValidationException ex:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = ex.Errors;
                        break;
                    case KeyNotFoundException ex:
                        //not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        //unhandle error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
