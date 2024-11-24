using RedisBasedAuthSystem.Repositories;

namespace RedisBasedAuthSystem.Services;

public class UserSessionService
{
    private readonly IUserSessionRepository _userSessionRepository;

    public UserSessionService(IUserSessionRepository userSessionRepository)
    {
        _userSessionRepository = userSessionRepository;
    }

    public async Task SetSessionAsync(string sessionId, string userId, string role, TimeSpan expiry)
    {
        await _userSessionRepository.SetUserSessionAsync(sessionId, userId, role, expiry);
    }

    public async Task<string?> GetRoleAsync(string sessionId)
    {
        return await _userSessionRepository.GetUserRoleAsync(sessionId);
    }

    public async Task<bool> RemoveSessionAsync(string sessionId)
    {
        return await _userSessionRepository.RemoveSessionAsync(sessionId);
    }
}
