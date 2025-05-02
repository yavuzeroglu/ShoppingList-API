using ShoppingList.Domain.Enums;

namespace ShoppingList.Application.DTOs.Baskets;

public class AddUserToBasketDTO
{
    public string UserId { get; set; } = string.Empty;
    public BasketUserRole Role { get; set; }
}
