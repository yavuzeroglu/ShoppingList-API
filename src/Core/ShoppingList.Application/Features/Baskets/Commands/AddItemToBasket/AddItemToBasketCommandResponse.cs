namespace ShoppingList.Application.Features.Baskets.Commands.AddItemToBasket;

public class AddItemToBasketCommandResponse 
{ 
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
}
