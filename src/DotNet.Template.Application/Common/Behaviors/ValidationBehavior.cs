using FluentValidation;
using MediatR;

namespace DotNet.Template.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
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
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Any())
            {
                var errors = failures.Select(f => f.ErrorMessage).ToList();

                // Handle Result<T> responses
                if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
                {
                    var resultType = typeof(TResponse).GetGenericArguments()[0];
                    var failureMethod = typeof(Result<>).MakeGenericType(resultType).GetMethod("Failure", new[] { typeof(List<string>) });
                    return (TResponse)failureMethod!.Invoke(null, new object[] { errors })!;
                }

                // Handle Result responses
                if (typeof(TResponse) == typeof(Result))
                {
                    return (TResponse)(object)Result.Failure(errors);
                }

                throw new ValidationException(failures);
            }
        }

        return await next();
    }
}
