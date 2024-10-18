using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Abstractions.Repositories.ProductImage;
using ShoppingList.Application.Abstractions.Repositories.Products;
using ShoppingList.Application.Abstractions.Storage;
using ShoppingList.Application.Features.Images.Commands.DeleteImage;
using ShoppingList.Application.Features.Images.Commands.UploadImage;
using ShoppingList.Application.Features.Products.Commands.CreateProduct;
using ShoppingList.Application.Features.Products.Commands.DeleteProduct;
using ShoppingList.Application.Features.Products.Commands.UpdateProduct;
using ShoppingList.Application.Features.Products.Queries.GetAllProducts;
using ShoppingList.Application.Features.Products.Queries.GetByIdProduct;
using ShoppingList.Domain.Entities;


namespace ShoppingList.WebAPI.Controllers;

public class ProductController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IProductImageWriteRepository _productImageWriteRepository;
    private readonly IStorageService _storageService;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IConfiguration configuration;

    public ProductController(IMediator mediator, IProductImageWriteRepository productImageWriteRepository, IStorageService storageService, IProductReadRepository productReadRepository, IConfiguration configuration)
    {
        _mediator = mediator;
        _productImageWriteRepository = productImageWriteRepository;
        _storageService = storageService;
        _productReadRepository = productReadRepository;
        this.configuration = configuration;

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

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateOneProductAsync(CreateProductCommandRequest request)
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

    [HttpDelete]
    public async Task<IActionResult> Remove(DeleteProductCommandRequest request)
    {
        await _mediator.Send(request);
        return NoContent();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UploadAsync(UploadImageCommandRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> RemoveProductImageAsync(DeleteImageCommandRequest request)
    {
        await _mediator.Send(request);
        return NoContent();
    }
}

