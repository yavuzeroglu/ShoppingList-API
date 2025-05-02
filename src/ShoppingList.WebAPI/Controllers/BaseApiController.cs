using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingList.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]s/[action]")]
public abstract class BaseApiController : ControllerBase
{
   public readonly IMediator _mediator;

    protected BaseApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

}