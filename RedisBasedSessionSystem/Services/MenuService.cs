using System.Text.Json;
using RedisBasedAuthSystem.Repositories;
using RedisBasedSessionSystem.DTOs;
using RedisBasedSessionSystem.Entities;

namespace RedisBasedAuthSystem.Services;
public class MenuService
{
    private readonly IRepository<MenuRecord> _redisRepository;
    private readonly IRepository<MenuRecord> _sqlRepository;

    public MenuService(IRepository<MenuRecord> redisRepository, IRepository<MenuRecord> sqlRepository)
    {
        _redisRepository = redisRepository;
        _sqlRepository = sqlRepository;
    }

    public async Task SaveMenuAsync(string role, List<MenuItemDto> menuItems)
    {
        var menuJson = JsonSerializer.Serialize(menuItems);
        var menuRecord = new MenuRecord { Role = role, MenuJson = menuJson, CreatedAt = DateTime.UtcNow };

        await _redisRepository.AddAsync(menuRecord);
        await _sqlRepository.AddAsync(menuRecord);
    }

    public async Task<List<MenuItemDto>?> GetMenuAsync(string role)
    {
        var menuJson = await _redisRepository.GetByKeyAsync($"menu:{role}");
        if (menuJson != null)
        {
            return JsonSerializer.Deserialize<List<MenuItemDto>>(menuJson.MenuJson);
        }

        var sqlMenu = await _sqlRepository.GetByKeyAsync(role);
        if (sqlMenu != null)
        {
            var menuItems = JsonSerializer.Deserialize<List<MenuItemDto>>(sqlMenu.MenuJson);
            await _redisRepository.AddAsync(sqlMenu);
            return menuItems;
        }

        return null;
    }
}
