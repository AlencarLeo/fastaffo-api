namespace fastaffo_api.src.Application.DTOs;
public class DayJobsDtoRes {
    public required int jobQuantity { get; set; }
    public required int day { get; set; }
    public required List<JobDtoRes> jobs { get; set; }
}