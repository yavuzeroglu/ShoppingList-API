using FluentValidation;
using MediatR;

namespace ShoppingList.Application.Common.Behaviors;

public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
   where TRequest : IRequest<TResponse>
{

   private readonly IEnumerable<IValidator<TRequest>> _validator;

   public FluentValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
   {
      _validator = validator;
   }

   public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
   {
      if (_validator.Any())
      {
         var context = new ValidationContext<TRequest>(request);

         var validationResults = await Task.WhenAll(
             _validator.Select(v => v.ValidateAsync(context, cancellationToken)));

         var failures = validationResults
             .SelectMany(r => r.Errors)
             .Where(f => f != null)
             .ToList();

         if (failures.Count != 0)
            throw new ValidationException(failures);
      }

      return await next();
   }
}