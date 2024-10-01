using MediatR;

namespace ShoppingList.Application.Features.AppUsers.Commands.RefreshTokenLogin;

public class RefreshTokenLoginCommandRequest : IRequest<RefreshTokenLoginCommandResponse>
{
   public  string RefreshToken { get; set; }
}
