using Microsoft.AspNetCore.Mvc;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.WebAPI.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly ShoppingListDbContext _context;

        public ProductController(ShoppingListDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context
            .Products
            // .Include(p => p.Category)
            .ToList();

            if (products.Count == 0)
                return NotFound();

            return Ok(products);
        }

        [HttpPost]
        public IActionResult CreateOneProduct([FromBody] Product product)
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
            _context.Products.Add(entity);
            _context.SaveChanges();
            return Ok(new {
                Name = entity.Name,
                CreatedDate = entity.CreatedDate,
                CategoryName = _context
                    .Categories
                    .Where(c => c.Id == entity.CategoryId)
                    .Select(c => c.Name)
                    .FirstOrDefault()
            });
        }
    }
}
