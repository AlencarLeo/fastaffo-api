using fastaffo_api.src.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace fastaffo_api.src.Infrastructure.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }
    public DbSet<ExtraRateAmountEntry> ExtraRateAmountEntries { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<RatePolicy> RatePolicies { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<StaffJob> StaffJobs { get; set; }
    public DbSet<StaffJobAllowance> StaffJobAllowances { get; set; }
    public DbSet<StaffTeam> StaffTeams { get; set; }
    public DbSet<Team> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Job>()
            .HasOne(j => j.Company)
            .WithMany()
            .HasForeignKey(j => j.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RatePolicy>()
            .Property(r => r.DayMultiplier)
            .HasPrecision(10, 4);

        modelBuilder.Entity<RatePolicy>()
            .Property(r => r.OvertimeMultiplier)
            .HasPrecision(10, 4);

        modelBuilder.Entity<StaffJob>()
            .Property(sj => sj.Kilometers)
            .HasPrecision(10, 2);
    }
}
