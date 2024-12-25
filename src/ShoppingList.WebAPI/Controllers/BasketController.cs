using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Features.Baskets.Commands.AddItemToBasket;
using ShoppingList.Application.Features.Baskets.Commands.ADdItemToBasket;
using ShoppingList.Application.Features.Baskets.Commands.CreateBasket;
using ShoppingList.Application.Features.Baskets.Commands.RemoveBasket;
using ShoppingList.Application.Features.Baskets.Commands.RemoveBasketItem;
using ShoppingList.Application.Features.Baskets.Commands.UpdateQuantityBasketItem;
using ShoppingList.Application.Features.Baskets.Queries.GetAllBasket;
using ShoppingList.Application.Features.Baskets.Queries.GetByIdBasket;

namespace ShoppingList.WebAPI.Controllers;


[Authorize(AuthenticationSchemes = "Admin")]
public class BasketController : BaseApiController
{
  public BasketController(IMediator mediator) : base(mediator)
  {
  }

  [HttpGet]
  public async Task<IActionResult> GetBaskets()
  {
    var response = await _mediator.Send(new GetAllBasketQueryRequest());
    return Ok(response);
  }

  [HttpPost]
  public async Task<IActionResult> GetById(GetByIdBasketQueryRequest request)
  {
    GetByIdBasketQueryResponse response = await _mediator.Send(request);
    return Ok(response);
  }

  [HttpPost]
  public async Task<IActionResult> CreateBasket([FromBody] CreateBasketCommandRequest request)
  {
    CreateBasketCommandResponse response = await _mediator.Send(request);
    return Ok(response);
  }

  [HttpPost]
  public async Task<IActionResult> AddItemToBasket([FromBody] AddItemToBasketCommandRequest request)
  {
    AddItemToBasketCommandResponse response = await _mediator.Send(request);
    return Ok(response);
  }

  [HttpDelete]
  public async Task<IActionResult> RemoveBasketItem(RemoveBasketItemCommandRequest request)
  {
    await _mediator.Send(request);
    return Ok();
  }

  [HttpPut]
  public async Task<IActionResult> UpdateQuantity(UpdateQuantityBasketItemCommandRequest request)
  {
    UpdateQuantityBasketItemCommandResponse response = await _mediator.Send(request);
    return Ok(response);
  }

  [HttpDelete]
  public async Task<IActionResult> RemoveBasket(RemoveBasketCommandRequest request)
  {
    RemoveBasketCommandResponse response = await _mediator.Send(request);
    return Ok(response);
  }

}