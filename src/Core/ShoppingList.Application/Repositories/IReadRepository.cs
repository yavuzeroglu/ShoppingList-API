using System.Linq.Expressions;
using ShoppingList.Domain.Entities.Common;

namespace ShoppingList.Application.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
{
   IQueryable<T> GetAll(bool tracking = false);
   IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = true);
   Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = true);
   Task<T> GetByIdAsync(int id, bool tracking = true);
}