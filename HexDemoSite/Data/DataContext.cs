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
        modelBuilder.Entity<DepartmentPosition>()
            .HasOne(x => x.OpenPosition)
            .WithOne(x => x.DepartmentPosition)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<OpenPosition>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<Candidate>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Candidate>()
            .HasOne(x => x.OpenPosition);

        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<DepartmentPosition> DepartmentPositions { get; set; }
    public DbSet<OpenPosition> OpenPositions { get; set; }
    public DbSet<Candidate> CandidateForms { get; set; }

    public static void MigrateAndSeed(DataContext context)
    {
        context.Database.Migrate();
        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                new Role { Id = 1, Name = "HR" },
                new Role { Id = 2, Name = "Manager" },
                new Role { Id = 3, Name = "Janitor" },
                new Role { Id = 4, Name = "Driver" },
                new Role { Id = 5, Name = "Air Traffic Controller" },
                new Role { Id = 6, Name = "Intern" }
            );
            context.SaveChanges();
        }

        if (!context.Employees.Any())
        {
            context.Employees.AddRange(
                new Employee { Id = 1, Name = "John Doe" },
                new Employee { Id = 2, Name = "Jane Doe" }
            );
        }
        
        if (!context.DepartmentPositions.Any())
        {
            context.DepartmentPositions.AddRange(
                new DepartmentPosition { Id = 1, EmployeeId = 1, RoleId = 1, JobTitle = "HR", Department = "Operations", Location = "LITTLE ROCK", JobDescription = "Handle HR duties within the department" },
                new DepartmentPosition { Id = 2, EmployeeId = 2, RoleId = 2, JobTitle = "Manager", Department = "Operations", Location = "LAREDO", JobDescription = "Manage employees within the operations department" },
                new DepartmentPosition { Id = 3, RoleId = 3, JobTitle = "Janitor", Department = "Operations", Location = "LITTLE ROCK", JobDescription = "Ensure the workspace is kept to a certain cleanliness level" }
            );
        }

        context.SaveChanges();
    }
}