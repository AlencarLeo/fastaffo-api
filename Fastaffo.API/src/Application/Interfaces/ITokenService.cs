namespace fastaffo_api.src.Application.Interfaces;
public interface ITokenService
{
    string CreateToken(Guid id, string role);
}
