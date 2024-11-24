using RedisBasedAuthSystem.Repositories;

namespace RedisBasedAuthSystem.Services;

public class MenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task SetMenuAsync(string role, List<object> menuItems)
    {
        await _menuRepository.SetMenuForRoleAsync(role, menuItems);
    }

    public async Task<List<object>> GetMenuAsync(string role)
    {
        return await _menuRepository.GetMenuForRoleAsync(role);
    }
}
