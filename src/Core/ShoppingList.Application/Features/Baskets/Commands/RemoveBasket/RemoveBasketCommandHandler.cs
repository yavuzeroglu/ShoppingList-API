using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;


namespace ShoppingList.Application.Features.Baskets.Commands.RemoveBasket;

public class RemoveBasketCommandHandler : IRequestHandler<RemoveBasketCommandRequest, RemoveBasketCommandResponse>
{
    private readonly IBasketService _basketService;

    public RemoveBasketCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<RemoveBasketCommandResponse> Handle(RemoveBasketCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketService.RemoveBasketAsync(request.BasketId);

        return new RemoveBasketCommandResponse()
        {
            IsSuccess = true,
            Message = "Sepet başarıyla silindi"
        };
    }
}
