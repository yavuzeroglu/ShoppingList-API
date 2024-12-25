using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Features.Brands.Commands.CreateBrand;
using ShoppingList.Application.Features.Brands.Commands.DeleteBrand;
using ShoppingList.Application.Features.Brands.Commands.UpdateBrand;
using ShoppingList.Application.Features.Brands.Queries.GetAllBrand;
using ShoppingList.Application.Features.Brands.Queries.GetByIdBrand;

namespace ShoppingList.WebAPI.Controllers;

public class BrandController : BaseApiController
{
   public BrandController(IMediator mediator) : base(mediator)
   {
   }


   [HttpGet]
   public async Task<IActionResult> GetAll()
   {
      var response = await _mediator.Send(new GetAllBrandQueryRequest());
      return Ok(response);
   }

   [HttpGet("{Id}")]
   public async Task<IActionResult> GetById([FromRoute] GetByIdBrandQueryRequest request)
   {
      var response = await _mediator.Send(request);
      return Ok(response);
   }

   [HttpPost]
   public async Task<IActionResult> Create([FromBody] CreateBrandCommandRequest request)
   {
      await _mediator.Send(request);
      return StatusCode(StatusCodes.Status201Created);
   }


   [HttpPut]
   public async Task<IActionResult> Update([FromBody] UpdateBrandCommandRequest request)
   {
      await _mediator.Send(request);
      return Ok();
   }

   [HttpDelete]
   public async Task<IActionResult> Remove(DeleteBrandCommandRequest request)
   {
      await _mediator.Send(request);
      return NoContent();
   }
}