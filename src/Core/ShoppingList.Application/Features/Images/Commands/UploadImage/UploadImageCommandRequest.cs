using MediatR;
using Microsoft.AspNetCore.Http;

namespace ShoppingList.Application.Features.Images.Commands.UploadImage;

public class UploadImageCommandRequest : IRequest<Unit>
{
  public int ProductId { get; set; }
  public required IFormFile Photo { get; set; } 
}
