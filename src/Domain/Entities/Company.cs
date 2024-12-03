namespace fastaffo_api.src.Domain.Entities;
public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ABN { get; set; } = string.Empty;
    public List<UserAdmin>? Owners { get; set; }
    public List<UserAdmin>? Admins { get; set; }
}