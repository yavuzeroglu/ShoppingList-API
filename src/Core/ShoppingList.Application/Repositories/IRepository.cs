using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Entities.Common;

namespace ShoppingList.Application.Repositories;

public interface IRepository<T> where T : BaseEntity
{
   DbSet<T> Table { get; }
}

public interface IWriteRepository<T> : IRepository<T> where T: BaseEntity
{
   Task<bool> AddAsync(T entity);
   Task<bool> AddRangeAsync(List<T> entities);
   bool Remove(T entity);
   bool RemoveRanger(List<T> entities);
   Task<bool> RemoveAsync(T entity);
   bool Update(T entity);

   Task<int> SaveAsync();
}