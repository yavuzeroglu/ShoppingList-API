using ShoppingList.Application.DTOs.Users;

namespace ShoppingList.Application.Abstractions.Services;

public interface IUserService
{
   Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model);
}