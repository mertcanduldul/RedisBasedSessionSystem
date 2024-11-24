namespace RedisBasedAuthSystem.Repositories;

public interface IUserSessionRepository
{
    Task SetUserSessionAsync(string sessionId, string userId, string role, TimeSpan expiry);
    Task<string?> GetUserRoleAsync(string sessionId);
    Task<bool> RemoveSessionAsync(string sessionId);
}