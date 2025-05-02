using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;


namespace ShoppingList.Application.Features.Baskets.Commands.RemoveBasketItem;

public class RemoveBasketItemCommandHandler : IRequestHandler<RemoveBasketItemCommandRequest, RemoveBasketItemCommandResponse>
{
    private readonly IBasketItemService _basketItemService;

    public RemoveBasketItemCommandHandler(IBasketItemService basketItemService)
    {
        _basketItemService = basketItemService;
    }


    public async Task<RemoveBasketItemCommandResponse> Handle(RemoveBasketItemCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketItemService.RemoveBasketItemAsync(request.BasketItemId);

        return new RemoveBasketItemCommandResponse()
        {
            IsSuccess = true,
            Message = "Ürün sepetten başarıyla kaldırıldı"
        };
    }
}