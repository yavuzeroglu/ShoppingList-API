using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Features.AppUsers.Commands.LoginUser;
using ShoppingList.Application.Features.AppUsers.Commands.PasswordReset;
using ShoppingList.Application.Features.AppUsers.Commands.RefreshTokenLogin;
using ShoppingList.Application.Features.AppUsers.Commands.VerifyResetToken;

namespace ShoppingList.WebAPI.Controllers;


public class AuthController : BaseApiController
{
    public AuthController(IMediator mediator) : base(mediator)
    {

    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request)
    {
        LoginUserCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> RefreshTokenLogin(RefreshTokenLoginCommandRequest request)
    {
        RefreshTokenLoginCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest request)
    {
        VerifyResetTokenCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}