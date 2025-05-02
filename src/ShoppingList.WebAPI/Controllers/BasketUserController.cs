using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Common.Abstractions.Services;
using ShoppingList.Application.DTOs.Baskets;


namespace ShoppingList.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize (AuthenticationSchemes = "Admin")]
public class BasketUserController : BaseApiController
{
    private readonly IBasketUserService _basketUserService;

    public BasketUserController(IBasketUserService basketUserService, IMediator mediator) : base(mediator)
    {
        _basketUserService = basketUserService;
    }

    [HttpGet("{basketId}/users")]
    public async Task<IActionResult> GetBasketUsers(int basketId)
    {
        var users = await _basketUserService.GetBasketUsersAsync(basketId);
        return Ok(users);
    }

    [HttpGet("{basketId}/user/{userId}/role")]
    public async Task<IActionResult> GetUserRole(int basketId, string userId)
    {
        var role = await _basketUserService.GetUserRoleInBasketAsync(basketId, userId);
        return Ok(role);
    }

    [HttpPost("{basketId}/users")]
    public async Task<IActionResult> AddUserToBasket(int basketId, [FromBody] AddUserToBasketDTO dto)
    {
        var basketUser = await _basketUserService.AddUserToBasketAsync(basketId, dto.UserId, dto.Role);
        return CreatedAtAction(nameof(GetBasketUsers), new { basketId }, basketUser);
    }

    [HttpPut("{basketId}/user/{userId}/role")]
    public async Task<IActionResult> UpdateUserRole(int basketId, string userId, [FromBody] UpdateUserRoleDTO dto)
    {
        await _basketUserService.UpdateUserRoleAsync(basketId, userId, dto.NewRole);
        return NoContent();
    }

    [HttpDelete("{basketId}/user/{userId}")]
    public async Task<IActionResult> RemoveUserFromBasket(int basketId, string userId)
    {
        await _basketUserService.RemoveUserFromBasketAsync(basketId, userId);
        return NoContent();
    }
} 