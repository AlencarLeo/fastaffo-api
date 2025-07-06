using System.Security.Claims;

using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Application.Mappers;
using fastaffo_api.src.Domain.Entities;
using fastaffo_api.src.Infrastructure.Data;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Application.Services;
public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    private readonly IValidatorService _validatorService;
    private readonly IValidator<AdminDtoReq> _adminDtoReqValidator;
    private readonly IValidator<StaffDtoReq> _staffDtoReqValidator;
    private readonly IValidator<AuthDtoReq> _authDtoReqValidator;
    public AuthService(
        IHttpContextAccessor httpContextAccessor,
        DataContext context,
        ITokenService tokenService,
        IValidatorService validatorService,
        IValidator<AdminDtoReq> adminDtoReqValidator,
        IValidator<AuthDtoReq> authDtoReqValidator,
        IValidator<StaffDtoReq> staffDtoReqValidator
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
        _tokenService = tokenService;
        _validatorService = validatorService;
        _adminDtoReqValidator = adminDtoReqValidator;
        _staffDtoReqValidator = staffDtoReqValidator;
        _authDtoReqValidator = authDtoReqValidator;
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
        await _validatorService.ValidateAsync(_adminDtoReqValidator, request);

        Admin? admin = await _context.Admins.SingleOrDefaultAsync(s => s.Email == request.Email);

        if (admin is not null)
        {
            throw new Exception("If you already have an account associated with this email, simply [click here] to reset your password and access your account.");
        }

        string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(request.Password);

        admin = AdminMapper.ToEntity(request);

        await _context.AddAsync(admin);
        await _context.SaveChangesAsync();
    }

    public async Task<TokenUserDto<AdminDtoRes>> AuthenticateAdminAsync(AuthDtoReq request)
    {
        await _validatorService.ValidateAsync(_authDtoReqValidator, request);

        Admin? admin = await _context.Admins
                                            .Include(a => a.ContactInfo)
                                            .SingleOrDefaultAsync(s => s.Email == request.Email);

        if (admin is null || !BCrypt.Net.BCrypt.Verify(request.Password, admin.Password))
        {
            throw new Exception("User not found or wrong password.");
        }

        string token = _tokenService.CreateToken(admin.Id, admin.Role.ToString());

        AdminDtoRes adminDtoRes = AdminMapper.ToDto(admin);

        return new TokenUserDto<AdminDtoRes>(adminDtoRes, token);
    }

    public async Task RegisterStaffAsync(StaffDtoReq request)
    {
        await _validatorService.ValidateAsync(_staffDtoReqValidator, request);

        Staff? staff = await _context.Staffs.SingleOrDefaultAsync(s => s.Email == request.Email);

        if (staff is not null)
        {
            throw new Exception("If you already have an account associated with this email, simply [click here] to reset your password and access your account.");
        }

        string passwordHash
            = BCrypt.Net.BCrypt.HashPassword(request.Password);

        staff = StaffMapper.ToEntity(request);

        await _context.Staffs.AddAsync(staff);
        await _context.SaveChangesAsync();
    }

    public async Task<TokenUserDto<StaffDtoRes>> AuthenticateStaffAsync(AuthDtoReq request)
    {
        await _validatorService.ValidateAsync(_authDtoReqValidator, request);

        Staff? staff = await _context.Staffs
                                            .Include(s => s.ContactInfo)
                                            .SingleOrDefaultAsync(s => s.Email == request.Email);

        if (staff is null || !BCrypt.Net.BCrypt.Verify(request.Password, staff.Password))
        {
            throw new Exception("User not found or wrong password.");
        }

        string token = _tokenService.CreateToken(staff.Id, "staff");

        StaffDtoRes staffDtoRes =  StaffMapper.ToDto(staff);

        return new TokenUserDto<StaffDtoRes>(staffDtoRes, token);
    }

}
