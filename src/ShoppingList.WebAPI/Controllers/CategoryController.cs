using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.DTOs.Categories;
using ShoppingList.Application.Mapping;
using ShoppingList.Application.Repositories.Categories;
using ShoppingList.Domain.Entities;

namespace ShoppingList.WebAPI.Controllers;

public class CategoryController : BaseApiController
{
   private readonly ICategoryReadRepository _categoryReadRepository;
   private readonly ICategoryWriteRepository _categoryWriteRepository;
   private readonly IMapper _mapper;

   public CategoryController(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IMapper mapper)
   {
      _categoryReadRepository = categoryReadRepository;
      _categoryWriteRepository = categoryWriteRepository;
      _mapper = mapper;
   }


   [HttpGet]
   public async Task<IActionResult> GetAll()
   {
      var categories = await _categoryReadRepository
         .GetAll()
         .Include(c => c.Products)
         .Include(c => c.SubCategories)
         .Include(c => c.ParentCategory)
         .ToListAsync();
         
      if (categories is null)
         return NotFound();

      var result = _mapper.Map<List<ListCategoryDTO>>(categories);

      return Ok(result);
   }

   [HttpGet("GetDetails")]
   public IActionResult GetDetails()
   {
      var categories = _categoryReadRepository
         .GetAll()
         .Include(c => c.SubCategories)
         .Include(c => c.Products)
         .Include(c => c.ParentCategory);

      var result = _mapper.Map<List<ListCategoryDTO>>(categories);

      return Ok(result);

   }


   [HttpGet("{detailId:int}")]
   public async Task<IActionResult> GetOneDetail([FromRoute] int detailId)
   {
     var category = await _categoryReadRepository
         .GetWhere(c => c.Id.Equals(detailId))
         .Include(c => c.Products)
         .Include(c => c.SubCategories)
         .Include(c => c.ParentCategory)
         .FirstOrDefaultAsync();


      var vm = _mapper.Map<ListCategoryDTO>(category);

      return Ok(vm);
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