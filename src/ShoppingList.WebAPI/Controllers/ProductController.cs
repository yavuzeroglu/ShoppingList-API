using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Features.Products.Commands.CreateProduct;
using ShoppingList.Application.Features.Products.Commands.DeleteProduct;
using ShoppingList.Application.Features.Products.Commands.UpdateProduct;
using ShoppingList.Application.Features.Products.Queries.GetAllProducts;
using ShoppingList.Application.Features.Products.Queries.GetByIdProduct;
using ShoppingList.Application.Repositories.Products;

namespace ShoppingList.WebAPI.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IMapper mapper, IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllProductQueryRequest());
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
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
    }
}
