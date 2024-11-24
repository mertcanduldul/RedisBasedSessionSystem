using System.Text.Json;
using StackExchange.Redis;

namespace RedisBasedAuthSystem.Repositories;


public class UserSessionRepository : IUserSessionRepository
{
    private readonly IDatabase _db;

    public UserSessionRepository(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task SetUserSessionAsync(string sessionId, string userId, string role, TimeSpan expiry)
    {
        var sessionData = new
        {
            UserId = userId,
            Role = role,
            LoginTime = DateTime.UtcNow
        };

        var serializedData = JsonSerializer.Serialize(sessionData);
        await _db.StringSetAsync(sessionId, serializedData, expiry);
    }

    public async Task<string?> GetUserRoleAsync(string sessionId)
    {
        var sessionData = await _db.StringGetAsync(sessionId);
        if (string.IsNullOrEmpty(sessionData)) return null;

        var deserializedData = JsonSerializer.Deserialize<dynamic>(sessionData);
        return deserializedData?.Role;
    }

    public async Task<bool> RemoveSessionAsync(string sessionId)
    {
        return await _db.KeyDeleteAsync(sessionId);
    }
}
