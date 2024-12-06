namespace fastaffo_api.src.Application.DTOs;
public class CompanyDtoRes
{
    public required string Name { get; set; }
    public required string ABN { get; set; }
    public List<UserAdminDtoRes> OwnersAndAdmins { get; set; } = new List<UserAdminDtoRes>();
}