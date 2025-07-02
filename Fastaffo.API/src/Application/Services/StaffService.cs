using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;


namespace fastaffo_api.src.Application.Services;

public class StaffService : IStaffService
{
    private readonly DataContext _context;
    public StaffService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponseDto<StaffDtoRes>> GetStaffByIdAsync(Guid staffId)
    {
        var staff = await _context.Staffs
                                        .Include(s => s.ContactInfo)
                                        .FirstOrDefaultAsync(s => s.Id == staffId);

        if (staff == null)
        {
            return new ServiceResponseDto<StaffDtoRes>(null, "Staff member not found.", 404);
        }

        var staffDtoRes = new StaffDtoRes
        {
            Id = staff.Id,
            Name = staff.Name,
            Lastname = staff.Lastname,
            Email = staff.Email,
            ContactInfo = staff.ContactInfo is not null
                ? new ContactInfoDto
                {
                    PhotoLogoUrl = staff.ContactInfo.PhotoLogoUrl,
                    PhoneNumber = staff.ContactInfo.PhoneNumber,
                    PostalCode = staff.ContactInfo.PostalCode,
                    State = staff.ContactInfo.State,
                    City = staff.ContactInfo.City,
                    AddressLine1 = staff.ContactInfo.AddressLine1,
                    AddressLine2 = staff.ContactInfo.AddressLine2
                }
                : null
        };

        return new ServiceResponseDto<StaffDtoRes>(staffDtoRes, "Staff member retrieved successfully.", 200);
    }

}
