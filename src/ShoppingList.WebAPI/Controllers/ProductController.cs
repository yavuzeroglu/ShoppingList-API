using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.DTOs.Products;
using ShoppingList.Application.Features.Products.Commands.CreateProduct;
using ShoppingList.Application.Features.Products.Commands.DeleteProduct;
using ShoppingList.Application.Features.Products.Commands.UpdateProduct;
using ShoppingList.Application.Features.Products.Queries.GetAllProducts;
using ShoppingList.Application.Features.Products.Queries.GetByIdProduct;
using ShoppingList.Application.Repositories.Products;
using ShoppingList.Domain.Entities;

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
            // var tempData = await _productReadRepository
            //     .Table
            //     .Include(p => p.Brand)
            //     .Include(p => p.Category)
            //     .FirstOrDefaultAsync(p => p.Id.Equals(id));

            // //Getwhere ile deneyelimm...

            // var product = _productReadRepository
            //     .GetWhere(x => x.Id.Equals(id))
            //     .Include(p => p.Brand)
            //     .Include(p => p.Category);

            // if (tempData is null)
            //     return NotFound();

            // var productDto = _mapper.Map<ListProductDTO>(tempData);

            // return Ok(productDto);

            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return StatusCode((int)StatusCodes.Status201Created);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOneProduct([FromBody] CreateProductDTO productDTO)
        {
            if (productDTO is null)
                return BadRequest();

            var convertEnt = _mapper.Map<Product>(productDTO);
            await _productWriteRepository.AddAsync(convertEnt);
            await _productWriteRepository.SaveAsync();
            return Ok(productDTO);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOneProductAsync([FromBody] UpdateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }


        // action adi softDelete mi olmali - sadece delete mi ?
        [HttpDelete]
        public async Task<IActionResult> DeleteOneProduct(DeleteProductCommandRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
