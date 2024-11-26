using System.Text.Json;
using StackExchange.Redis;

namespace RedisBasedAuthSystem.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly IDatabase _db;

    public MenuRepository(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task SetMenuForRoleAsync(string role, List<object> menuItems)
    {
        var serializedMenu = JsonSerializer.Serialize(menuItems);
        await _db.StringSetAsync($"menu:{role}", serializedMenu);
    }

    public async Task<List<object>> GetMenuForRoleAsync(string role)
    {
        var menuData = await _db.StringGetAsync($"menu:{role}");
        if (string.IsNullOrEmpty(menuData)) return new List<object>();

        return JsonSerializer.Deserialize<List<object>>(menuData!) ?? new List<object>();
    }
}