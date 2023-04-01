namespace Application.Wrappres
{
    /// <summary>
    /// Different types of response were created so that the client can manage it based on customized status and messages.
    /// </summary>
    /// <typeparam name="T">This is the object that will be returned in the response, which can be of any type, so type T was placed.</typeparam>
    public class Response<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
        public Response()
        {

        }
        /// <Response>
        /// successful response where the default status is returned as true, a message 
        /// and the response which is the data where the content of the information is contained.
        /// </Response>
        /// <param name="data">Information is contained</param>
        /// <param name="message">Any message sent to the customer</param>
        public Response(T data, string? message = null)
        {
            Success = true;
            Message = message;
            Data = data;
        }
        /// <Response>
        /// Default response where we send as failed or false status to indicate that it went wrong where we also send the message that is a detail of the error or cause.
        /// </Response>
        /// <param name="message">the message that is a detail of the error or cause</param>
        public Response(string? message)
        {
            Success = false;
            Message = message;
        }

    }
}
