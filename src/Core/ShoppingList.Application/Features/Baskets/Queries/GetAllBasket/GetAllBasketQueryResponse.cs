using ShoppingList.Application.DTOs.Baskets;

namespace ShoppingList.Application.Features.Baskets.Queries.GetAllBasket;

public class GetAllBasketQueryResponse
{
    public int Id { get; set; }
    public string CreatedByUserId { get; set; } = string.Empty;
    public string BasketName { get; set; } = string.Empty;
    public bool IsPurchased { get; set; } = false;
    public decimal TotalAmount { get; set; }
    public List<BasketItemViewModel> Items { get; set; } = [];
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
}
