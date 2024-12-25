namespace ShoppingList.Application.DTOs.Baskets;

public class BasketItemViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
}
