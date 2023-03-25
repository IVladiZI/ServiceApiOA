using FluentValidation.Results;

namespace Application.Exceptions
{
    /// <ValidationException>
    /// Perzonalized exceptions are created for the client where the .Net base exception is also inherited.
    /// </ValidationException>
    public class ValidationException : Exception
    {
        public List<string> Errors { get; }
        /// <ValidationException>
        /// Call to the channel where first if there is any validation for the interface it will be sent to validate and if not it will proceed with the execution, the errors found will be sent in the Error List. 
        /// </ValidationException>
        public ValidationException() : base("An error has occurred in the validation of the request")
        {
            Errors = new List<string>();
        }
        /// <ValidationException>
        /// All errors are collected and added to the list of errors to be sent.
        /// </ValidationException>
        /// <param name="failures"></param>
        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
                Errors.Add(failure.ErrorMessage);
        }
    }

}
