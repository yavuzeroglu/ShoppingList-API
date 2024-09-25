using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.DTOs.Categories;
using ShoppingList.Application.Features.Categories.Commands.CreateCategory;
using ShoppingList.Application.Features.Categories.Commands.DeleteCategory;
using ShoppingList.Application.Features.Categories.Commands.UpdateCategory;
using ShoppingList.Application.Features.Categories.Queries.GetAllCategory;
using ShoppingList.Application.Mapping;
using ShoppingList.Application.Repositories.Categories;
using ShoppingList.Domain.Entities;

namespace ShoppingList.WebAPI.Controllers;

public class CategoryController : BaseApiController
{
   private readonly ICategoryReadRepository _categoryReadRepository;
   private readonly ICategoryWriteRepository _categoryWriteRepository;
   private readonly IMapper _mapper;
   private readonly IMediator _mediator;

   public CategoryController(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IMapper mapper, IMediator mediator)
   {
      _categoryReadRepository = categoryReadRepository;
      _categoryWriteRepository = categoryWriteRepository;
      _mapper = mapper;
      _mediator = mediator;
   }


   [HttpGet]
   public async Task<IActionResult> GetAll()
   {
      // var categories = await _categoryReadRepository
      //    .GetAll()
      //    .ToListAsync();

      // if (categories is null)
      //    return NotFound();

      // // var result = _mapper.Map<List<ListCategoryDTO>>(categories);

      // return Ok(categories);

      var response = await _mediator.Send(new GetAllCategoryQueryRequest());
      return Ok(response);
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
   public async Task<IActionResult> Create(CreateCategoryCommandRequest request)
   {
      await _mediator.Send(request);
      return StatusCode((int)StatusCodes.Status201Created);
      // if (category is null)
      //    return BadRequest();

      // await _categoryWriteRepository.AddAsync(category);
      // await _categoryWriteRepository.SaveAsync();
      // return Ok(category);
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

   [HttpPut]
   public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommandRequest request)
   {
      await _mediator.Send(request);
      return Ok();
   }

   [HttpDelete]
   public async Task<ActionResult> Remove([FromBody] DeleteCategoryCommandRequest request)
   {
      await _mediator.Send(request);
      return NoContent();

      // var category = await _categoryReadRepository.GetByIdAsync(id, false);
      // if (category is null)
      //    return NotFound();

      // await _categoryWriteRepository.RemoveAsync(id);
      // await _categoryWriteRepository.SaveAsync();
      // return NoContent();
   }
}