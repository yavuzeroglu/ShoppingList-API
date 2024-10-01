namespace ShoppingList.Application.DTOs;


public class TokenDTO
{
   public string AccessToken { get; set; } = string.Empty;
   public DateTime Expiration { get; set; }
   public string RefreshToken { get; set; } = string.Empty;
}