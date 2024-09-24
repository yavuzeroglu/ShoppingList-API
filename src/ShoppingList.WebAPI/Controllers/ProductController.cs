using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ShoppingList.Application.DTOs.Products;
using ShoppingList.Application.Features.Products.Commands.CreateProduct;
using ShoppingList.Application.Features.Products.Commands.DeleteProduct;
using ShoppingList.Application.Features.Products.Commands.UpdateProduct;
using ShoppingList.Application.Features.Products.Queries.GetAllProducts;
using ShoppingList.Application.Mapping;
using ShoppingList.Application.Repositories.Products;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

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


            // var products = _productReadRepository.GetAll().OrderBy(c => c.Id);
            // if (products is null)
            //     return NotFound();

            // return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var tempData = await _productReadRepository
                .Table
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id.Equals(id));

            //Getwhere ile deneyelimm...

            var product = _productReadRepository
                .GetWhere(x => x.Id.Equals(id))
                .Include(p => p.Brand)
                .Include(p => p.Category);

            if (tempData is null)
                return NotFound();

            var productDto = _mapper.Map<ListProductDTO>(tempData);

            return Ok(productDto);
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

            // Product product = await _productReadRepository.GetByIdAsync(productDTO.Id, false);
            // if (product is null)
            //     throw new InvalidOperationException("Product not found!");

            // var updateProduct = _mapper.Map<Product>(productDTO);
            // updateProduct.CreatedDate = product.CreatedDate;

            // _productWriteRepository.Update(updateProduct);
            // await _productWriteRepository.SaveAsync();
            // return Ok(updateProduct);
        }


        // action adi softDelete mi olmali - sadece delete mi ?
        [HttpDelete]
        public async Task<IActionResult> DeleteOneProduct(DeleteProductCommandRequest request)
        {
            await _mediator.Send(request);
            return NoContent();

            // var entity = await _productReadRepository.GetByIdAsync(id, true);
            // if (entity is null)
            //     return NotFound();

            // await _productWriteRepository.RemoveAsync(id);
            // await _productWriteRepository.SaveAsync();
            // return NoContent();
        }
    }
}
