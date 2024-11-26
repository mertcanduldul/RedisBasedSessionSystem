namespace RedisBasedSessionSystem.DTOs;

public class SetMenuRequestDto
{
    public string Role { get; set; }
    public List<MenuItemDto> MenuItems { get; set; }
}

public class MenuItemDto
{
    public string Name { get; set; }
    public string Url { get; set; }
}

public class CreateSessionRequestDto
{
    public string SessionKey { get; set; }
    public string SessionData { get; set; }
}

public class LoginRequestDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginResponseDto
{
    public string Token { get; set; }
    public string Message { get; set; }
}

public class GetMenuRequestDto
{
    /// <summary>
    /// Kullanıcının rolü. Örneğin: "Admin"
    /// </summary>
    public string Role { get; set; }
}
public class GetSessionRequestDto
{
    /// <summary>
    /// Oturum anahtarı. Örneğin: "user:123"
    /// </summary>
    public string SessionKey { get; set; }
}
