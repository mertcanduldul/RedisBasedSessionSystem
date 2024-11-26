using RedisBasedAuthSystem.Repositories;
using RedisBasedSessionSystem.Entities;

namespace RedisBasedAuthSystem.Services;

public class SessionService
{
    private readonly IRepository<SessionRecord> _redisRepository;
    private readonly IRepository<SessionRecord> _sqlRepository;

    public SessionService(IRepository<SessionRecord> redisRepository, IRepository<SessionRecord> sqlRepository)
    {
        _redisRepository = redisRepository;
        _sqlRepository = sqlRepository;
    }

    public async Task CreateSessionAsync(string sessionKey, string sessionData)
    {
        var sessionRecord = new SessionRecord { SessionKey = sessionKey, SessionData = sessionData, CreatedAt = DateTime.UtcNow };

        await _redisRepository.AddAsync(sessionRecord);
        await _sqlRepository.AddAsync(sessionRecord);
    }

    public async Task<string?> GetSessionAsync(string sessionKey)
    {
        var sessionData = await _redisRepository.GetByKeyAsync($"session:{sessionKey}");
        if (sessionData != null)
        {
            return sessionData.SessionData;
        }

        var sqlSession = await _sqlRepository.GetByKeyAsync(sessionKey);
        if (sqlSession != null)
        {
            await _redisRepository.AddAsync(sqlSession);
            return sqlSession.SessionData;
        }

        return null;
    }
}
