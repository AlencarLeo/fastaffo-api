namespace fastaffo_api.src.Domain.Entities;
public class User
{
    public string Role { get; } = "admin";
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}