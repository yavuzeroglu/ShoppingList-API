using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.DTOs.Categories;
using ShoppingList.Application.Features.Categories.Commands.CreateCategory;
using ShoppingList.Application.Features.Categories.Commands.DeleteCategory;
using ShoppingList.Application.Features.Categories.Commands.UpdateCategory;
using ShoppingList.Application.Features.Categories.Queries.GetAllCategory;
using ShoppingList.Application.Features.Categories.Queries.GetByIdCategory;
using ShoppingList.Application.Mapping;
using ShoppingList.Application.Repositories.Categories;
using ShoppingList.Domain.Entities;

namespace ShoppingList.WebAPI.Controllers;

[Route("api/Categories")]
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


   [HttpGet("{Id}")]
   public async Task<IActionResult> GetOneCategory([FromRoute] GetByIdCategoryQueryRequest request)
   {
      GetByIdCategoryQueryResponse response = await _mediator.Send(request);
      return Ok(response);
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
      return StatusCode(StatusCodes.Status201Created);
   }

   [HttpPut]
   public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommandRequest request)
   {
      await _mediator.Send(request);
      return Ok();
   }

   [HttpDelete]
   public async Task<ActionResult> Remove (DeleteCategoryCommandRequest request)
   {
      await _mediator.Send(request);
      return NoContent();
   }
}