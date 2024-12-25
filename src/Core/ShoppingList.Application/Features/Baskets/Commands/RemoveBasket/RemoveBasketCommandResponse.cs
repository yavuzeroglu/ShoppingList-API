namespace ShoppingList.Application.Features.Baskets.Commands.RemoveBasket;
public sealed class RemoveBasketCommandResponse 
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
}
