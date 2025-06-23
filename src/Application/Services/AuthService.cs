using System.Security.Claims;
using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Application.Services;
public class AuthService : IAuthService
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

    public (string? Id, List<string>? Roles) GetAuthenticatedUser()
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
    
    public async Task RegisterAdminAsync(AdminDtoReq request)
    {
        Admin? admin = await _context.Admins.SingleOrDefaultAsync(s => s.Email == request.Email);

        if (admin is not null)
        {
            throw new Exception("If you already have an account associated with this email, simply [click here] to reset your password and access your account.");
        }

        string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(request.Password);

        Admin newAdmin = new Admin
        {
            Name = request.Name,
            Lastname = request.Lastname,
            Email = request.Email,
            Password = passwordHash,
            Role = request.Role,
            CompanyId = request.CompanyId,
            ContactInfo = request.ContactInfo == null ? null : new ContactInfo
            {
                PhotoLogoUrl = request.ContactInfo.PhotoLogoUrl,
                PhoneNumber = request.ContactInfo.PhoneNumber,
                PostalCode = request.ContactInfo.PostalCode,
                State = request.ContactInfo.State,
                City = request.ContactInfo.City,
                AddressLine1 = request.ContactInfo.AddressLine1,
                AddressLine2 = request.ContactInfo.AddressLine2
            }
        };

        await _context.AddAsync(newAdmin);
        await _context.SaveChangesAsync();
    }
    public async Task RegisterUserStaffAsync(Staff request)
    {
        Staff? userStaff = await _context.Staffs.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(userStaff is not null){
            throw new Exception("If you already have an account associated with this email, simply [click here] to reset your password and access your account.");
        }

        string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // userStaff = new UserStaff
        // {
        //     FirstName = request.FirstName,
        //     LastName = request.LastName,
        //     Phone = request.Phone,
        //     Email = request.Email,
        //     Password = passwordHash
        // };

        await _context.Staffs.AddAsync(userStaff);
        await _context.SaveChangesAsync();
    }
    public async Task<TokenUserDto<Admin>> AuthenticateAdminAsync(AuthDtoReq request)
    {
        Admin? userAdmin =  await _context.Admins.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(userAdmin is null || !BCrypt.Net.BCrypt.Verify(request.Password, userAdmin.Password)){
            throw new Exception("User not found or wrong password.");
        }

        string token = _tokenService.CreateToken(userAdmin.Id, userAdmin.Role.ToString());
        

        // UserAdminDtoRes userAdminDtoRes = new UserAdminDtoRes{
        //     Id = userAdmin.Id,
        //     FirstName = userAdmin.FirstName,
        //     LastName = userAdmin.LastName,
        //     Email = userAdmin.Email,
        //     Phone = userAdmin.Phone,
        //     IsOwner = userAdmin.IsOwner,
        //     Role = userAdmin.Role
        // };

        return new TokenUserDto<Admin>(userAdmin, token);
    }
    public async Task<TokenUserDto<Staff>> AuthenticateStaffAsync(AuthDtoReq request)
    {
        Staff? userStaff =  await _context.Staffs.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(userStaff is null || !BCrypt.Net.BCrypt.Verify(request.Password, userStaff.Password)){
            throw new Exception("User not found or wrong password.");
        }

        string token = _tokenService.CreateToken(userStaff.Id, "staff");

        // UserStaffDtoRes userStaffDtoRes = new UserStaffDtoRes{
        //     Id = userStaff.Id,
        //     Role = userStaff.Role,    
        //     FirstName = userStaff.FirstName,
        //     LastName = userStaff.LastName,
        //     Phone = userStaff.Phone,
        //     Email = userStaff.Email
        // };

        return new TokenUserDto<Staff>(userStaff, token);
    }
    
}