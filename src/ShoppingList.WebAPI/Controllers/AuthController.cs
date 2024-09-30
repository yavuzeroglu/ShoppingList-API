using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Features.AppUsers.Commands.LoginUser;

namespace ShoppingList.WebAPI.Controllers;


public class AuthController : BaseApiController
{
   private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody]LoginUserCommandRequest request)
    {
        LoginUserCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}