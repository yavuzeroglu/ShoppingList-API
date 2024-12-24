using Microsoft.AspNetCore.Identity;

namespace ShoppingList.Domain.Entities.Identity;


public class AppUser : IdentityUser<string>
{
   public string? RefreshToken { get; set; }
   public DateTime? RefreshTokenEndDate { get; set; }
   public ICollection<BasketUser> BasketUsers { get; set; }
}
