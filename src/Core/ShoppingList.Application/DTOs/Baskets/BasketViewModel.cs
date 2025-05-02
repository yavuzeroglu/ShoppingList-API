namespace ShoppingList.Application.DTOs.Baskets;

public class BasketViewModel
{
    public int Id { get; set; }
    public string BasketName { get; set; } = string.Empty;
    public string CreatedByUser { get; set; } = string.Empty;
    public bool IsPurchased { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public List<BasketItemDTO> Items { get; set; } = new();
}
