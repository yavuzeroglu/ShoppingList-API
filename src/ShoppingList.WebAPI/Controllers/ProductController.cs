using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using ShoppingList.Application.Repositories.Products;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.WebAPI.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productReadRepository.GetAll();
            if (products is null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _productReadRepository.GetByIdAsync(id, false);
            if (product is null)
                return NotFound();


            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneProduct([FromBody] Product product)
        {
            if (product is null)
                return BadRequest();

            Product entity = new()
            {
                Name = product.Name,
                CategoryId = product.CategoryId,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };
            await _productWriteRepository.AddAsync(entity);
            await _productWriteRepository.SaveAsync();
            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneProductAsync([FromRoute(Name = "id")] int id, [FromBody] Product product)
        {
            // check product
            var entity = await _productReadRepository.GetByIdAsync(id, false);
            
            
            if(entity is null)
                return NotFound(); // 404


            // check id
            if (id != product.Id)
                return BadRequest(); // 400

            entity.Name = product.Name;
            entity.CategoryId = product.CategoryId;
            entity.IsActive = product.IsActive;
            
            _productWriteRepository.Update(entity);
            await _productWriteRepository.SaveAsync();

            return Ok(entity);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneProduct([FromRoute(Name = "id")] int id)
        {
            var entity = await _productReadRepository.GetByIdAsync(id, true);
            if(entity is null)
                return NotFound();

            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return NoContent();
        }
    }
}
