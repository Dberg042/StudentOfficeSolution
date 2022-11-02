using Microsoft.EntityFrameworkCore;
using StudentOffice.Models; 

namespace StudentOffice.Data
{
    public class StudentOfficeContext : DbContext
    {
        public StudentOfficeContext(DbContextOptions<StudentOfficeContext> options)
            : base(options)
        {

        }
        //constructor getting here db context option that wil lcontain the connection string
        public DbSet<Team> Teams { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<EmployeeCondition> EmployeeConditions  { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MO");


            //Many to Many Intersection
            modelBuilder.Entity<EmployeeCondition>()
            .HasKey(t => new { t.ConditionID, t.EmployeeID });

            //Prevent Cascade Delete from Team to Employee
            //so we are prevented from deleting a Team with
            //Employees assigned
            modelBuilder.Entity<Team>()
                .HasMany<Employee>(d => d.Employees)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamID)
                .OnDelete(DeleteBehavior.Restrict);

            //Add this so you don't get Cascade Delete
            //Note: Allow Cascade Delete from Employee to EmployeeCondition
            modelBuilder.Entity<EmployeeCondition>()
                .HasOne(pc => pc.Condition)
                .WithMany(c => c.EmployeeConditions)
                .HasForeignKey(pc => pc.ConditionID)
                .OnDelete(DeleteBehavior.Restrict);

            //Add a unique index to the Employee Number
            modelBuilder.Entity<Employee>()
            .HasIndex(p => p.EmployeeNumber)
            .IsUnique();
        }
    }
}
