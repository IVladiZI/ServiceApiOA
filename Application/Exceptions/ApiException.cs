using System.Globalization;

namespace Application.Exceptions
{
    /// <ApiException>
    /// We inherit from the Exception base class and handle 3 types of constructors, this to customize API type errors.
    /// </ApiException>
    public class ApiException : Exception
    {
        /// <ApiException>
        /// Error by default
        /// </ApiException>
        public ApiException() : base() { }
        /// <ApiException>
        /// Error with a message
        /// </ApiException>
        /// <param name="message">Custom message</param>
        public ApiException(string message) : base(message) { }
        /// <ApiException>
        /// Error with a message and params of type object
        /// </ApiException>
        /// <param name="message">Custom message</param>
        /// <param name="args">objects params</param>
        public ApiException(string message,params object[] args) : base($"{CultureInfo.CurrentCulture} {message} {args}") 
        {
        }
    }
}
