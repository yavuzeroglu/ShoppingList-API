using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;

namespace ShoppingList.Application.Features.AppUsers.Commands.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, Unit>
{
   private readonly IUserService _userService;

   public UpdatePasswordCommandHandler(IUserService userService)
   {
      _userService = userService;
   }

   public async Task<Unit> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
   {
      if (!request.Password.Equals(request.PasswordConfirm))
         throw new Exception("Şifre ve Şifre Tekrarı aynı olmalıdır.");

      await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);
      return Unit.Value;
   }
}