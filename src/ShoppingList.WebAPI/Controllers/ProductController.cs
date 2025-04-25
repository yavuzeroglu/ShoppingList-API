using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Features.Images.Commands.DeleteImage;
using ShoppingList.Application.Features.Images.Commands.UploadImage;
using ShoppingList.Application.Features.Products.Commands.CreateProduct;
using ShoppingList.Application.Features.Products.Commands.DeleteProduct;
using ShoppingList.Application.Features.Products.Commands.PatchProduct;
using ShoppingList.Application.Features.Products.Commands.UpdateProduct;
using ShoppingList.Application.Features.Products.Queries.GetAllProducts;
using ShoppingList.Application.Features.Products.Queries.GetByIdProduct;



namespace ShoppingList.WebAPI.Controllers;

public class ProductController : BaseApiController
{
    public ProductController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _mediator.Send(new GetAllProductQueryRequest());
        return Ok(response);
    }


    [Authorize(AuthenticationSchemes = "Admin")]
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] GetByIdProductQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOneProductAsync([FromBody] CreateProductCommandRequest request)
    {
        await _mediator.Send(request);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOneProductAsync([FromBody] UpdateProductCommandRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProductAsync(int id, [FromBody] PatchProductCommandRequest request)
    {
        request.Id = id; // Route'dan gelen ID'yi request'e atıyoruz
        await _mediator.Send(request);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(DeleteProductCommandRequest request)
    {
        await _mediator.Send(request);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> UploadAsync(UploadImageCommandRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveProductImageAsync(DeleteImageCommandRequest request)
    {
        await _mediator.Send(request);
        return NoContent();
    }
}

