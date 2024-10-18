using MediatR;

namespace ShoppingList.Application.Features.Images.Commands.DeleteImage;

public class DeleteImageCommandRequest : IRequest<Unit>
{
  public int ProductId { get; set; }
  public int ImageId { get; set; }
}

