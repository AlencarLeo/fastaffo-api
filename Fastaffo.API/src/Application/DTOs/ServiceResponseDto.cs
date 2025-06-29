namespace fastaffo_api.src.Application.DTOs;
public class ServiceResponseDto
{
    public string Message { get; set; }
    public int StatusCode { get; set; }

    public ServiceResponseDto(string message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }
}

public class ServiceResponseDto<T> : ServiceResponseDto
{
    public T? Data { get; set; }

    public ServiceResponseDto(T? data, string message, int statusCode)
        : base(message, statusCode)
    {
        Data = data;
    }
}