using Microsoft.AspNetCore.Mvc;
using ShoppingList.Application.Repositories.Categories;
using ShoppingList.Domain.Entities;

namespace ShoppingList.WebAPI.Controllers;

public class CategoryController : BaseApiController
{
   private readonly ICategoryReadRepository _categoryReadRepository;
   private readonly ICategoryWriteRepository _categoryWriteRepository;

   public CategoryController(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository)
   {
      _categoryReadRepository = categoryReadRepository;
      _categoryWriteRepository = categoryWriteRepository;
   }


   [HttpGet]
   public IActionResult GetAll()
   {
      var categories = _categoryReadRepository.GetAll();
      if (categories is null)
         return NotFound();

      return Ok(categories);
   }

   [HttpGet("{id:int}")]
   public async Task<IActionResult> GetById([FromRoute(Name = "id")] int id)
   {
      var category = await _categoryReadRepository.GetByIdAsync(id, false);
      if (category is null)
         return NotFound();

      return Ok(category);
   }

   [HttpPost]
   public async Task<IActionResult> Create([FromBody] Category category)
   {
      if (category is null)
         return BadRequest();

      await _categoryWriteRepository.AddAsync(category);
      await _categoryWriteRepository.SaveAsync();
      return Ok(category);
   }

   [HttpPut("{id:int}")]
   public async Task<IActionResult> Update([FromRoute(Name = "id")] int id, [FromBody] Category category)
   {
      var entity = await _categoryReadRepository.GetByIdAsync(id, false);

      if (entity is null)
         return NotFound();
         // throw new Exception($"Not Found Category => {id} ");

      if (category is null)
         return BadRequest();

      entity.Name = category.Name;
      entity.ParentCategoryId = category.ParentCategoryId ?? entity.ParentCategoryId;
      
      _categoryWriteRepository.Update(entity);
      await _categoryWriteRepository.SaveAsync();

      return Ok();
   }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remove([FromRoute(Name = "id")] int id)
    {
        var category = await _categoryReadRepository.GetByIdAsync(id, false);
        if (category is null)
            return NotFound();

        await _categoryWriteRepository.RemoveAsync(id);
        await _categoryWriteRepository.SaveAsync();
        return NoContent();
    }
}