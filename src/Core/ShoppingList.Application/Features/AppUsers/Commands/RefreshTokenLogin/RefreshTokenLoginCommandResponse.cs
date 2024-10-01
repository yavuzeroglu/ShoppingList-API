namespace ShoppingList.Application.Features.AppUsers.Commands.RefreshTokenLogin;

public class RefreshTokenLoginCommandResponse 
{
   public string? AccessToken { get; set; }
   public string? RefreshToken { get; set; }
}
