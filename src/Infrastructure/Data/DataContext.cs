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

        // Evita erro de múltiplos caminhos de cascade delete
        modelBuilder.Entity<Job>()
            .HasOne(j => j.Company)
            .WithMany()
            .HasForeignKey(j => j.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        // modelBuilder.Entity<StaffTeam>()
        //     .HasOne(st => st.Staff)
        //     .WithMany()
        //     .HasForeignKey(st => st.StaffId)
        //     .OnDelete(DeleteBehavior.Cascade); // permite cascade aqui

        // modelBuilder.Entity<StaffTeam>()
        //     .HasOne(st => st.Team)
        //     .WithMany()
        //     .HasForeignKey(st => st.TeamId)
        //     .OnDelete(DeleteBehavior.Restrict); // evita conflito no outro lado


        // Corrige possíveis truncamentos de decimal
        modelBuilder.Entity<RatePolicy>()
            .Property(r => r.day_multiplier)
            .HasPrecision(10, 4);

        modelBuilder.Entity<RatePolicy>()
            .Property(r => r.overtime_multiplier)
            .HasPrecision(10, 4);

        modelBuilder.Entity<StaffJob>()
            .Property(sj => sj.Kilometers)
            .HasPrecision(10, 2);
    }
}
