namespace RedisBasedAuthSystem.Repositories;

public interface IMenuRepository
{
    Task SetMenuForRoleAsync(string role, List<object> menuItems);
    Task<List<object>> GetMenuForRoleAsync(string role);
}
