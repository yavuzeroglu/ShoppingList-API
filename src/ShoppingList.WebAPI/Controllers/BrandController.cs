using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Repositories.Brands;
using ShoppingList.Domain.Entities;
using ShoppingList.Persistance.Context;

namespace ShoppingList.WebAPI.Controllers;

public class BrandController : BaseApiController
{
   private readonly IBrandWriteRepository _brandWriteRepository;
   private readonly IBrandReadRepository _brandReadRepository;
   private readonly ShoppingListDbContext _context;

   public BrandController(IBrandWriteRepository brandWriteRepository, IBrandReadRepository brandReadRepository, ShoppingListDbContext context)
   {
      _brandWriteRepository = brandWriteRepository;
      _brandReadRepository = brandReadRepository;
      _context = context;
   }


   [HttpGet]
   public IActionResult GetAll()
   {
      var brands = _brandReadRepository.GetAll().OrderBy(b => b.Id);
      return Ok(brands);
   }

   [HttpGet("{id:int}")]
   public async Task<IActionResult> GetById([FromRoute(Name = "id")] int id)
   {
      var brand = await _brandReadRepository.GetByIdAsync(id);
      if (brand is null)
         return NotFound();

      return Ok(brand);
   }

   [HttpPost]
   public async Task<IActionResult> Create([FromBody] Brand brand)
   {
      if (brand is null)
         return BadRequest();

      var brandNames = _brandReadRepository.GetAll();

      foreach (var item in brandNames)
      {
         if (brand.Name.ToLower() == item.Name.ToLower())
         {
            return BadRequest("Aynı isimde bir marka bulunuyor.");
         }
      }

      await _brandWriteRepository.AddAsync(brand);
      await _brandWriteRepository.SaveAsync();

      return Ok(brand);
   }

   [HttpPut("{id:int}")]
   public async Task<IActionResult> Update([FromRoute(Name = "id")] int id, [FromBody] Brand brand)
   {
      var entity = await _brandReadRepository.GetByIdAsync(id, false);
      if (entity is null)
         return NotFound(); 

      entity.Name = brand.Name;

      _brandWriteRepository.Update(entity);
      await _brandWriteRepository.SaveAsync();
      return Ok(entity);
   }

   [HttpDelete("{id:int}")]
   public async Task<IActionResult> Remove([FromRoute(Name = "id")] int id)
   {
      var entity = await _brandReadRepository.GetByIdAsync(id, false);
      if (entity is null)
         return NotFound();

      _brandWriteRepository.Remove(entity);
      await _brandWriteRepository.SaveAsync();
      return StatusCode((int)HttpStatusCode.NoContent);
   }
}