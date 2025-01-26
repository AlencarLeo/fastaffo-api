using System.Security.Claims;
using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Application.Services;
public class AuthService : IAuthservice
{    
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    public AuthService(IHttpContextAccessor httpContextAccessor, DataContext context, ITokenService tokenService)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
        _tokenService = tokenService;
    }

    public (string? Id, List<string>? Roles) GetCurrentUser()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null)
        {
            return (null, null);
        }

        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var roleClaims = user.FindAll(ClaimTypes.Role);
        var roles = roleClaims.Select(c => c.Value).ToList();

        return (id, roles);
    }
    public async Task RegisterUserAdminAsync(UserAdminDtoReq request)
    {
        UserAdmin? userAdmin = await _context.Admins.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(userAdmin is not null){
            throw new Exception("If you already have an account associated with this email, simply [click here] to reset your password and access your account.");
        }

        string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(request.Password);

        userAdmin = new UserAdmin
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Password = passwordHash,
            IsOwner = request.IsOwner,
            CompanyId = null
        };

        await _context.AddAsync(userAdmin);
        await _context.SaveChangesAsync();
    }
    public async Task RegisterUserStaffAsync(UserStaffDtoReq request)
    {
        UserStaff? userStaff = await _context.Staffs.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(userStaff is not null){
            throw new Exception("If you already have an account associated with this email, simply [click here] to reset your password and access your account.");
        }

        string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(request.Password);

        userStaff = new UserStaff
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone,
            Email = request.Email,
            Password = passwordHash
        };

        await _context.Staffs.AddAsync(userStaff);
        await _context.SaveChangesAsync();
    }
    public async Task<TokenUserDto<UserAdminDtoRes>> AuthenticateAdminAsync(AuthDtoReq request)
    {
        UserAdmin? userAdmin =  await _context.Admins.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(userAdmin is null || !BCrypt.Net.BCrypt.Verify(request.Password, userAdmin.Password)){
            throw new Exception("User not found or wrong password.");
        }

        string token = _tokenService.CreateToken(userAdmin.Id, userAdmin.Role);
        

        UserAdminDtoRes userAdminDtoRes = new UserAdminDtoRes{
            Id = userAdmin.Id,
            FirstName = userAdmin.FirstName,
            LastName = userAdmin.LastName,
            Email = userAdmin.Email,
            Phone = userAdmin.Phone,
            IsOwner = userAdmin.IsOwner,
            Role = userAdmin.Role
        };

        return new TokenUserDto<UserAdminDtoRes>(userAdminDtoRes, token);
    }
    public async Task<TokenUserDto<UserStaffDtoRes>> AuthenticateStaffAsync(AuthDtoReq request)
    {
        UserStaff? userStaff =  await _context.Staffs.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(userStaff is null || !BCrypt.Net.BCrypt.Verify(request.Password, userStaff.Password)){
            throw new Exception("User not found or wrong password.");
        }

        string token = _tokenService.CreateToken(userStaff.Id, userStaff.Role);

        UserStaffDtoRes userStaffDtoRes = new UserStaffDtoRes{
            Id = userStaff.Id,
            Role = userStaff.Role,    
            FirstName = userStaff.FirstName,
            LastName = userStaff.LastName,
            Phone = userStaff.Phone,
            Email = userStaff.Email
        };

        return new TokenUserDto<UserStaffDtoRes>(userStaffDtoRes, token);
    }
    
}