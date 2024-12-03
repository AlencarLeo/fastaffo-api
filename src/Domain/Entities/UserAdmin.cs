namespace fastaffo_api.src.Domain.Entities;
public class UserAdmin
{
    public Guid Id { get; set; }
    public string Role { get; } = "admin";
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
}