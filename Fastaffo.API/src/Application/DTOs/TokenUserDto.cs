namespace fastaffo_api.src.Application.DTOs;
public class TokenUserDto<T>
{
    public T User { get; set; }
    public string Token { get; set; }

    public TokenUserDto(T user, string token)
    {
        User = user;
        Token = token;
    }
}
