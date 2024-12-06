namespace fastaffo_api.src.Domain.Entities;
public class UserAdmin
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; }
    public bool IsOwner { get; set; }
    public string Role { get; } = "admin";
}
