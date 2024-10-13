using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Entities.Common;

namespace ShoppingList.Application.Abstractions.Repositories;

public interface IRepository<T> where T : BaseEntity
{
   DbSet<T> Table { get; }
}
