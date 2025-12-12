using Domain.Abstractions.Result;
using Domain.Errors;
using FluentValidation;
using MediatR;



namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TResponse : IAppResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var validationContext = new ValidationContext<TRequest>(request);
                
                var validationFailures = (await Task.WhenAll(
                        _validators.Select(validator =>
                         validator.ValidateAsync(validationContext, cancellationToken))))
                                  .SelectMany(validationResut => validationResut.Errors)
                                  .ToList();
               
                if (validationFailures.Any())
                {
                 var errorMessages = validationFailures.Select(failure=>failure.ErrorMessage).ToList();
                 
                 var error = Error.GetValidation(string.Join(",", errorMessages));
                
                 return (TResponse)(object) Result.Failure(error);
                }

            }
            
             var response = await  next();

            return response;
        }

    }
}
