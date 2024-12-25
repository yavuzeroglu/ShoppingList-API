using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Entities.Common;

namespace ShoppingList.Application.Common.Abstractions.Repositories;

public interface IRepository<T> where T : BaseEntity
{
   DbSet<T> Table { get; }
}
