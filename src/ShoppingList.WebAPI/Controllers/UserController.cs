using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Features.AppUsers.Commands.CreateUser;
using ShoppingList.Application.Features.AppUsers.Commands.LoginUser;

namespace ShoppingList.WebAPI.Controllers;

public class UserController : BaseApiController
{
    readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
    {
        CreateUserCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}

