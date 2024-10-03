using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Abstractions.Services;
using ShoppingList.Application.Features.AppUsers.Commands.CreateUser;
using ShoppingList.Application.Features.AppUsers.Commands.LoginUser;

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

    [HttpGet]
    public async Task<IActionResult> ExampleMailTest()
    {
        await _mailService.SendMessageAsync("nuray.ilhan.eroglu@icloud.com", "Test Mail", $"Anne bu bir deneme mailidir. <i>Seni</i> <b>Seviyorum</b>");
        return Ok();
    }
}

