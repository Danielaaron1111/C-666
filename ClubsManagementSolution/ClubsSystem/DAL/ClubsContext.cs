using System;
using Microsoft.EntityFrameworkCore;
using ClubsSystem.Entities;

namespace ClubsSystem.DAL
{
    /// <summary>
    /// ClubsContext - Database context for the Clubs Management System
    /// This class follows the WestWind pattern for DbContext implementation
    /// Access level is internal as per assignment requirements
    /// </summary>
    internal partial class ClubsContext : DbContext
    {
        /// <summary>
        /// Constructor that accepts DbContextOptions for dependency injection
        /// This pattern allows the connection string to be configured externally
        /// </summary>
        public ClubsContext(DbContextOptions<ClubsContext> options)
            : base(options)
        {
        }

        // DbSet properties for each entity
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Club> Clubs { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Program> Programs { get; set; }

        /// <summary>
        /// OnModelCreating - Configure entity relationships and constraints
        /// This method is called by EF Core to configure the model
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Employee entity
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeID);

                // Configure relationship with Position
                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Positions");

                // Configure relationship with Program
                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ProgramID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Programs");
            });

            // Configure Club entity
            modelBuilder.Entity<Club>(entity =>
            {
                entity.HasKey(e => e.ClubID);

                // ClubName must be unique (business rule from assignment)
                entity.HasIndex(e => e.ClubName)
                    .IsUnique();

                // Default value for Active field
                entity.Property(e => e.Active)
                    .HasDefaultValue(true);

                // Default value for Fee field
                entity.Property(e => e.Fee)
                    .HasDefaultValue(0m);

                // Configure relationship with Employee (nullable)
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Clubs)
                    .HasForeignKey(d => d.EmployeeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clubs_Employees");
            });

            // Configure Position entity
            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.PositionID);
            });

            // Configure Program entity
            modelBuilder.Entity<Program>(entity =>
            {
                entity.HasKey(e => e.ProgramID);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        /// <summary>
        /// Partial method for additional model configuration
        /// This allows extending the model configuration in a partial class
        /// </summary>
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
