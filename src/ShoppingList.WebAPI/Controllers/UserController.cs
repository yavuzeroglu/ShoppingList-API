using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Abstractions.Services;
using ShoppingList.Application.Features.AppUsers.Commands.CreateUser;
using ShoppingList.Application.Features.AppUsers.Commands.LoginUser;
using ShoppingList.Application.Features.AppUsers.Commands.UpdatePassword;

namespace ShoppingList.WebAPI.Controllers;

public class UserController : BaseApiController
{
    readonly IMediator _mediator;
    private readonly IMailService _mailService;

    public UserController(IMediator mediator, IMailService mailService)
    {
        _mediator = mediator;
        _mailService = mailService;

    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
    {
        CreateUserCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest request)
    {
        await _mediator.Send(request);
        return StatusCode(StatusCodes.Status200OK, "Şifre Başarıyla Güncellendi!");
    }
}

