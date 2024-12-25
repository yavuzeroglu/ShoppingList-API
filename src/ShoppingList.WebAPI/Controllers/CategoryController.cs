using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Common.Abstractions.Repositories.Categories;
using ShoppingList.Application.DTOs.Categories;
using ShoppingList.Application.Features.Categories.Commands.CreateCategory;
using ShoppingList.Application.Features.Categories.Commands.DeleteCategory;
using ShoppingList.Application.Features.Categories.Commands.UpdateCategory;
using ShoppingList.Application.Features.Categories.Queries.GetAllCategory;
using ShoppingList.Application.Features.Categories.Queries.GetByIdCategory;


namespace ShoppingList.WebAPI.Controllers;


public class CategoryController : BaseApiController
{
   private readonly ICategoryReadRepository _categoryReadRepository;
   private readonly IMapper _mapper;

   public CategoryController(IMapper mapper, IMediator mediator, ICategoryReadRepository categoryReadRepository) : base(mediator)
   {
      _mapper = mapper;
      _categoryReadRepository = categoryReadRepository;
   }


   [HttpGet]
   public async Task<IActionResult> GetAll()
   {
      var response = await _mediator.Send(new GetAllCategoryQueryRequest());
      return Ok(response);
   }

   [HttpGet]
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
   public async Task<ActionResult> Remove(DeleteCategoryCommandRequest request)
   {
      await _mediator.Send(request);
      return NoContent();
   }
}