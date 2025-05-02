using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.DTOs.Baskets;

namespace ShoppingList.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Admin")]
public class BasketItemController : BaseApiController
{
    private readonly IBasketItemService _basketItemService;

    public BasketItemController(IBasketItemService basketItemService, IMediator mediator) : base(mediator)
    {
        _basketItemService = basketItemService;
    }

    [HttpGet("{basketId}")]
    public async Task<IActionResult> GetBasketItems(int basketId)
    {
        var items = await _basketItemService.GetBasketItemsAsync(basketId);
        return Ok(items);
    }

    [HttpGet("detail/{basketItemId}")]
    public async Task<IActionResult> GetBasketItemById(int basketItemId)
    {
        var item = await _basketItemService.GetBasketItemByIdAsync(basketItemId);
        return Ok(item);
    }

    [HttpPost("{basketId}")]
    public async Task<IActionResult> AddItemToBasket(int basketId, [FromBody] AddItemToBasketDTO dto)
    {
        var item = await _basketItemService.AddItemToBasketAsync(basketId, dto.ProductId, dto.Quantity);
        return CreatedAtAction(nameof(GetBasketItemById), new { basketItemId = item.Id }, item);
    }

    [HttpPut("quantity/{basketItemId}")]
    public async Task<IActionResult> UpdateQuantity(int basketItemId, [FromBody] int quantity)
    {
        await _basketItemService.UpdateQuantityAsync(basketItemId, quantity);
        return NoContent();
    }

    [HttpPut("toggle-purchase/{basketItemId}")]
    public async Task<IActionResult> TogglePurchaseStatus(int basketItemId)
    {
        await _basketItemService.TogglePurchaseStatusAsync(basketItemId);
        return NoContent();
    }

    [HttpPut("assign-user/{basketItemId}")]
    public async Task<IActionResult> AssignUser(int basketItemId, [FromBody] AssignUserToBasketItemDTO dto)
    {
        await _basketItemService.AssignUserToBasketItemAsync(basketItemId, dto.UserId);
        return NoContent();
    }

    [HttpDelete("assign-user/{basketItemId}")]
    public async Task<IActionResult> RemoveUserFromBasketItem(int basketItemId)
    {
        await _basketItemService.RemoveUserFromBasketItemAsync(basketItemId);
        return NoContent();
    }

    [HttpDelete("{basketItemId}")]
    public async Task<IActionResult> RemoveBasketItem(int basketItemId)
    {
        await _basketItemService.RemoveBasketItemAsync(basketItemId);
        return NoContent();
    }
}

