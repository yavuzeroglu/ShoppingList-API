using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;


namespace ShoppingList.Application.Features.Baskets.Commands.RemoveBasketItem;

public class RemoveBasketItemCommandHandler : IRequestHandler<RemoveBasketItemCommandRequest, RemoveBasketItemCommandResponse>
{
    private readonly IBasketService _basketService;

    public RemoveBasketItemCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<RemoveBasketItemCommandResponse> Handle(RemoveBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketService.RemoveBasketItemAsync(request.BasketItemId);

        return new RemoveBasketItemCommandResponse()
        {
            IsSuccess = true,
            Message = "Ürün sepetten başarıyla kaldırıldı"
        };
    }
}