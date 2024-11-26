namespace RedisBasedAuthSystem.Repositories;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<T> GetByKeyAsync(string key);
}