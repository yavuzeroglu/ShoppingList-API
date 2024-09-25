using FluentValidation;
using MediatR;

namespace ShoppingList.Application.Behaviors;

public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
   where TRequest : IRequest<TResponse>
{

   private readonly IEnumerable<IValidator<TRequest>> _validator;

   public FluentValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
   {
      _validator = validator;
   }

   public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
   {
      var context = new ValidationContext<TRequest>(request);
      var failures = _validator
         .Select(v => v.Validate(context))
         .SelectMany(result => result.Errors)
         .GroupBy(x => x.ErrorMessage)
         .Select(x => x.First())
         .Where(x => x is not null)
         .ToList();

         if(failures.Any())
            throw new ValidationException(failures);
         
         return next();
   }
}