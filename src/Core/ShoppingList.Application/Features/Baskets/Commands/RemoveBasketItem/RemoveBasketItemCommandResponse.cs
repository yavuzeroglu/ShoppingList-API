namespace ShoppingList.Application.Features.Baskets.Commands.RemoveBasketItem;

public class RemoveBasketItemCommandResponse 
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
}