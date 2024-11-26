namespace RedisBasedSessionSystem.Entities;

public class MenuRecord
{
    public int Id { get; set; }
    public string Role { get; set; }
    public string MenuJson { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class SessionRecord
{
    public int Id { get; set; }
    public string SessionKey { get; set; }
    public string SessionData { get; set; }
    public DateTime CreatedAt { get; set; }
}
