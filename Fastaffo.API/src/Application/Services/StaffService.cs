using fastaffo_api.src.Application.DTOs;
using fastaffo_api.src.Application.Interfaces;
using fastaffo_api.src.Application.Mappers;
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

        var staffDtoRes = StaffMapper.ToDto(staff);

        return new ServiceResponseDto<StaffDtoRes>(staffDtoRes, "Staff member retrieved successfully.", 200);
    }

}
