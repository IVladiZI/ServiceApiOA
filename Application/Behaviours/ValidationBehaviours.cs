using FluentValidation;
using MediatR;

namespace Application.Behaviours
{

    /// <ValidationBehaviours>
    /// With the help of installed nugget dependencies such as validation, auto mapper and mediaTR.
    /// Here it receives a request and a response and implements the pipeline interface.
    /// The validation class receives two types request and response and implements an interface where Irequest and Tresponse are mediaTR's own.
    /// This Pipeline is called before it is sent to the business logic or data insertion in order to validate all our filters on the incoming class data.
    /// </ValidationBehaviours>
    /// <typeparam name="Trequest"> Request </typeparam>
    /// <typeparam name="TResponse">Response </typeparam>
    public class ValidationBehaviours<Trequest, TResponse> : IPipelineBehavior<Trequest, TResponse> where Trequest : IRequest<TResponse>
    {
        /// <validators>
        ///A validator is created before entering the handle, it will validate and filter
        ///all the content that enters the webApi through the commands or queries.
        /// </validators>
        private readonly IEnumerable<IValidator<Trequest>> _validators;

        public ValidationBehaviours(IEnumerable<IValidator<Trequest>> validators)
        {
            _validators = validators;
        }
        /// <Handle>
        /// Call to the channel where first if there is any validation for the interface it will be sent to validate and if not it will proceed with the execution.
        /// </Handle>
        /// <param name="request">Request</param>
        /// <param name="next">Next Process</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TResponse> Handle(Trequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //Query if there is any type of validator for the handle channel.
            if (_validators.Any())
            {
                //If it exists, it will start calling the validator by sending it the request that arrived.
                var context = new FluentValidation.ValidationContext<Trequest>(request);
                //The results of these context validations are available.
                var validationResults = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));
                //The errors found in the validator are collected, these errors are the ones we create for
                //the business rules we need, they are collected and we are asked to return the error list.
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                //We query if any type of error was found and return it as a custom exception created by us.
                if (failures.Count != 0) throw new Exceptions.ValidationException(failures);
            }
            return await next();
        }
    }
}
