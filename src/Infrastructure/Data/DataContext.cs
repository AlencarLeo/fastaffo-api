using fastaffo_api.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Infrastructure.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobRequest> JobRequests { get; set; }
    public DbSet<JobStaff> JobStaffs { get; set; }
    public DbSet<UserAdmin> Admins { get; set; }
    public DbSet<UserStaff> Staffs { get; set; }

}