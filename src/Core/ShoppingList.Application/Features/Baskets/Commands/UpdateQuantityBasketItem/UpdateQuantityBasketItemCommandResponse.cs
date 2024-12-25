namespace ShoppingList.Application.Features.Baskets.Commands.UpdateQuantityBasketItem;

public class UpdateQuantityBasketItemCommandResponse 
{
    public decimal NewLineTotal { get; set; }
    public decimal NewBasketTotal { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
}
