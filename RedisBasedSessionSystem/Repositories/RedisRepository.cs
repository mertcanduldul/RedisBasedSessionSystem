using StackExchange.Redis;
using System.Text.Json;
using RedisBasedSessionSystem.Entities;

namespace RedisBasedAuthSystem.Repositories;


public class RedisRepository<T> : IRepository<T> where T : class
{
    private readonly IDatabase _db;

    public RedisRepository(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task AddAsync(T entity)
    {
        if (typeof(T) == typeof(MenuRecord) && entity is MenuRecord menu)
        {
            var menuJson = JsonSerializer.Serialize(menu.MenuJson);
            await _db.StringSetAsync($"menu:{menu.Role}", menuJson);
        }
        else if (typeof(T) == typeof(SessionRecord) && entity is SessionRecord session)
        {
            await _db.StringSetAsync($"session:{session.SessionKey}", session.SessionData, TimeSpan.FromMinutes(60));
        }
    }

    public async Task<T?> GetByKeyAsync(string key)
    {
        if (typeof(T) == typeof(MenuRecord))
        {
            var menuJson = await _db.StringGetAsync($"menu:{key}");
            return menuJson.HasValue
                ? JsonSerializer.Deserialize<MenuRecord>(menuJson.ToString()) as T
                : null;
        }
        else if (typeof(T) == typeof(SessionRecord))
        {
            var sessionData = await _db.StringGetAsync($"session:{key}");
            return sessionData.HasValue
                ? JsonSerializer.Deserialize<SessionRecord>(sessionData.ToString()) as T
                : null;
        }

        return null;
    }
}
