using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.DTOs.Baskets;
using ShoppingList.Application.Features.Baskets.Commands.AddItemToBasket;
using ShoppingList.Application.Features.Baskets.Commands.ADdItemToBasket;
using ShoppingList.Application.Features.Baskets.Commands.CreateBasket;
using ShoppingList.Application.Features.Baskets.Commands.RemoveBasket;
using ShoppingList.Application.Features.Baskets.Commands.RemoveBasketItem;
using ShoppingList.Application.Features.Baskets.Commands.SwitchPurchaseBasketItem;
using ShoppingList.Application.Features.Baskets.Queries.GetAllBasket;
using ShoppingList.Application.Features.Baskets.Queries.GetByIdBasket;
using ShoppingList.Domain.Enums;

namespace ShoppingList.WebAPI.Controllers;


[Authorize(AuthenticationSchemes = "Admin")]
public class BasketController : BaseApiController
{
  private readonly IBasketUserService _basketUserService;
  private readonly IBasketService _basketService;

  public BasketController(IMediator mediator, IBasketService basketService, IBasketUserService basketUserService) : base(mediator)
  {
    _basketService = basketService;
    _basketUserService = basketUserService;
  }

  [HttpGet]
  public async Task<IActionResult> GetBasket()
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

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateBasket(int id, [FromBody] UpdateBasketDTO dto)
  {
    await _basketService.UpdateBasketAsync(id, dto.NewBasketName);
    return NoContent();
  }

  [HttpPut("{id}/planned-date")]
  public async Task<IActionResult> UpdatePlannedPurchaseDate(int id, [FromBody] UpdatePlannedDateDTO dto)
  {
    await _basketService.UpdatePlannedPurchaseDateAsync(id, dto.PlannedPurchaseDate);
    return NoContent();
  }

  [HttpPost("{id}/duplicate")]
  public async Task<IActionResult> DuplicateBasket(int id, [FromBody] DuplicateBasketDTO dto)
  {
    var newBasket = await _basketService.DuplicateBasketAsync(id, dto.NewBasketName);
    return CreatedAtAction(nameof(GetById), new { id = newBasket.Id }, newBasket);
  }

  [HttpDelete]
  public async Task<IActionResult> RemoveBasket(RemoveBasketCommandRequest request)
  {
    RemoveBasketCommandResponse response = await _mediator.Send(request);
    return Ok(response);
  }

  [HttpPost]
  public async Task<IActionResult> SwitchPurchaseBasketItem(SwitchPurchaseBasketItemCommandRequest request)
  {
    await _mediator.Send(request);
    return Ok();
  }

  [HttpPost]
  public async Task<IActionResult> CreateBasketUser(int basketId, string userId)
  {
    var result = await _basketUserService.AddUserToBasketAsync(basketId, userId, BasketUserRole.Viewer);
    return Ok(result);
  }
}
