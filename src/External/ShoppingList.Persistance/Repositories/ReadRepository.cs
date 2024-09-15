using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Application.Repositories;
using ShoppingList.Domain.Entities.Common;
using ShoppingList.Persistance.Context;

namespace ShoppingList.Persistance.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly ShoppingListDbContext _context;

    public ReadRepository(ShoppingListDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool tracking = false)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();

        return query;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true)
    {
        var query = Table.Where(expression);
        if(!tracking)
            query = query.AsNoTracking();

        return query;
    }

    public async Task<T> GetByIdAsync(int id, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if(!tracking) 
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(data => data.Id.Equals(id));
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if(!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(expression);
    }

    public async Task<T> GetByIdAsync(string id, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if(!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(data => data.Id.Equals(Guid.Parse(id)));
    }
}
