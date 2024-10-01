using ShoppingList.Application.DTOs;

namespace ShoppingList.Application.Features.AppUsers.Commands.LoginUser;

public class LoginUserCommandResponse
{
   public string Token { get; set; } = string.Empty;
   public string RefreshToken { get; set; } = string.Empty;
   public DateTime Expiration { get; set; }
}

