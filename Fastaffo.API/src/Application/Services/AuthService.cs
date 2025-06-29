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

        admin = new Admin
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

        await _context.AddAsync(admin);
        await _context.SaveChangesAsync();
    }
    
    public async Task<TokenUserDto<AdminDtoRes>> AuthenticateAdminAsync(AuthDtoReq request)
    {
        Admin? admin = await _context.Admins
                                            .Include(a => a.ContactInfo)
                                            .SingleOrDefaultAsync(s => s.Email == request.Email);

        if (admin is null || !BCrypt.Net.BCrypt.Verify(request.Password, admin.Password))
        {
            throw new Exception("User not found or wrong password.");
        }

        string token = _tokenService.CreateToken(admin.Id, admin.Role.ToString());

        AdminDtoRes adminDtoRes = new AdminDtoRes
        {
            Id = admin.Id,
            Name = admin.Name,
            Lastname = admin.Lastname,
            Email = admin.Email,
            Role = admin.Role,
            CompanyId = admin.CompanyId,
            ContactInfo = admin.ContactInfo == null ? null : new ContactInfoDto
            {
                PhotoLogoUrl = admin.ContactInfo.PhotoLogoUrl,
                PhoneNumber = admin.ContactInfo.PhoneNumber,
                PostalCode = admin.ContactInfo.PostalCode,
                State = admin.ContactInfo.State,
                City = admin.ContactInfo.City,
                AddressLine1 = admin.ContactInfo.AddressLine1,
                AddressLine2 = admin.ContactInfo.AddressLine2
            }
        };

        return new TokenUserDto<AdminDtoRes>(adminDtoRes, token);
    }
    
    public async Task RegisterStaffAsync(StaffDtoReq request)
    {
        Staff? staff = await _context.Staffs.SingleOrDefaultAsync(s => s.Email == request.Email);

        if(staff is not null){
            throw new Exception("If you already have an account associated with this email, simply [click here] to reset your password and access your account.");
        }

        string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(request.Password);

        staff = new Staff
        {
            Name = request.Name,
            Lastname = request.Lastname,
            Email = request.Email,
            Password = passwordHash,
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

        await _context.Staffs.AddAsync(staff);
        await _context.SaveChangesAsync();
    }

    public async Task<TokenUserDto<StaffDtoRes>> AuthenticateStaffAsync(AuthDtoReq request)
    {
        Staff? staff = await _context.Staffs
                                            .Include(s => s.ContactInfo)
                                            .SingleOrDefaultAsync(s => s.Email == request.Email);

        if (staff is null || !BCrypt.Net.BCrypt.Verify(request.Password, staff.Password))
        {
            throw new Exception("User not found or wrong password.");
        }

        string token = _tokenService.CreateToken(staff.Id, "staff");

        StaffDtoRes staffDtoRes = new StaffDtoRes
        {
            Id = staff.Id,
            Name = staff.Name,
            Lastname = staff.Lastname,
            Email = staff.Email,
            ContactInfo = staff.ContactInfo == null ? null : new ContactInfoDto
            {
                PhotoLogoUrl = staff.ContactInfo.PhotoLogoUrl,
                PhoneNumber = staff.ContactInfo.PhoneNumber,
                PostalCode = staff.ContactInfo.PostalCode,
                State = staff.ContactInfo.State,
                City = staff.ContactInfo.City,
                AddressLine1 = staff.ContactInfo.AddressLine1,
                AddressLine2 = staff.ContactInfo.AddressLine2
            }
        };

        return new TokenUserDto<StaffDtoRes>(staffDtoRes, token);
    }
    
}