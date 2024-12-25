namespace ShoppingList.Application.Features.Baskets.Commands.CreateBasket;

public sealed class CreateBasketCommandResponse 
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
}