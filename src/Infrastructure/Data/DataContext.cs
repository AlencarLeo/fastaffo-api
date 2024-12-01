using fastaffo_api.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Infrastructure.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Job> Jobs { get; set; }
    public DbSet<UserStaff> Staffs { get; set; }

}