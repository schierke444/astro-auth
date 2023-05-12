namespace server.Models;

public class AuthDetailsDto
{
    public Guid Id { get; set; }    
    public string Username { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
}