using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Abstractions.Repositories.ProductImage;
using ShoppingList.Application.Abstractions.Storage;
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

    public ProductController(IMediator mediator, IProductImageWriteRepository productImageWriteRepository, IStorageService storageService)
    {
        _mediator = mediator;
        _productImageWriteRepository = productImageWriteRepository;
        _storageService = storageService;

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

    [HttpPost("[action]/{productId}")]
    public async Task<IActionResult> UploadAsync(int productId, IFormFile file)
    {
        (string path, string fileName) = await _storageService.UploadAsync("images", file);
        var image = new Image()
        {
            FileName = fileName,
            Path = path,
            ProductId = productId,
        };
        await _productImageWriteRepository.AddAsync(image);
        await _productImageWriteRepository.SaveAsync();

        return StatusCode(StatusCodes.Status200OK);
    }
}

