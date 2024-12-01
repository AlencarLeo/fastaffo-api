namespace fastaffo_api.src.Application.DTOs;
public class JobDto
{
    public required string Title { get; set; }
    public required string Company { get; set; }
    public required float BaseRate { get; set; }
    public required DateTime DateTime { get; set; }
    public required string Location { get; set; }
}