using MediatR;
using ShoppingList.Application.Common.Abstractions.Services;

namespace ShoppingList.Application.Features.Baskets.Commands.CreateBasket;

public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommandRequest, CreateBasketCommandResponse>
{
    private readonly IBasketService _basketService;

    public CreateBasketCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<CreateBasketCommandResponse> Handle(CreateBasketCommandRequest request, CancellationToken cancellationToken)
    {
        await _basketService.CreateBasketAsync(request.Name);

        return new CreateBasketCommandResponse()
        {
            IsSuccess = true,
            Message = "Sepet başarıyla oluşturuldu"
        };
    }
}