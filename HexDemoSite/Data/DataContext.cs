using HexDemoSite.Models;
using Microsoft.EntityFrameworkCore;

namespace HexDemoSite.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Role>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<DepartmentPosition>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<DepartmentPosition>()
            .HasOne(x => x.Role);
        modelBuilder.Entity<DepartmentPosition>()
            .HasOne(x => x.Employee);

        modelBuilder.Entity<OpenPosition>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<OpenPosition>()
            .HasOne(x => x.Role);
        
        modelBuilder.Entity<CandidateForm>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<CandidateForm>()
            .HasOne(x => x.OpenPosition);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<HexDemoSite.Models.DepartmentPosition> Position { get; set; }

    public DbSet<HexDemoSite.Models.CandidateForm> CandidateForm { get; set; }
}