namespace fastaffo_api.src.Application.DTOs;
public class MonthJobsDtoRes {
    public required int year { get; set; }
    public required int month { get; set; }
    public required List<DayJobsDtoRes> days { get; set; }
}