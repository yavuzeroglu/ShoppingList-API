using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Features.AppUsers.Commands.CreateUser;
using ShoppingList.Application.Features.AppUsers.Commands.UpdatePassword;

namespace ShoppingList.WebAPI.Controllers;

public class UserController : BaseApiController
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
    {
        CreateUserCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest request)
    {
        await _mediator.Send(request);
        return StatusCode(StatusCodes.Status200OK, "Şifre Başarıyla Güncellendi!");
    }
}

