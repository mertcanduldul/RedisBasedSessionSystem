using Microsoft.EntityFrameworkCore;
using RedisBasedSessionSystem.Context;
using RedisBasedSessionSystem.Entities;

namespace RedisBasedAuthSystem.Repositories;

using Microsoft.EntityFrameworkCore;

public class SqlRepository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public SqlRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<T?> GetByKeyAsync(string key)
    {
        if (typeof(T) == typeof(MenuRecord))
        {
            return await _context.MenuRecords.FirstOrDefaultAsync(m => m.Role == key) as T;
        }
        else if (typeof(T) == typeof(SessionRecord))
        {
            return await _context.SessionRecords.FirstOrDefaultAsync(s => s.SessionKey == key) as T;
        }

        return null;
    }
}
