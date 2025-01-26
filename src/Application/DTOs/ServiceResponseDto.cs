// public class ServiceResponseDto<T>
public class ServiceResponseDto
{
    // public T? Data { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }

    // public ServiceResponseDto(T? data, string message, int statusCode)
    public ServiceResponseDto(string message, int statusCode)
    {
        // Data = data;
        Message = message;
        StatusCode = statusCode;
    }
}
