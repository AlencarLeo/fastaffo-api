namespace fastaffo_api.src.Application.DTOs;
public class JobDtoRes
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required Guid CompanyId { get; set; }
    public required string CompanyName { get; set; }
    public required float BaseRate { get; set; }
    public required DateTime StartDateTime { get; set; }
    public required string Location { get; set; }
    public List<Guid>? StaffsId { get; set; }
}