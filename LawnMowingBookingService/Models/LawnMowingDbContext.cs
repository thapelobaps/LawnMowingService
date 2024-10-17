using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LawnMowingBookingService.Models;

public partial class LawnMowingDbContext : DbContext 
{
    public LawnMowingDbContext()
    {
    }

    public LawnMowingDbContext(DbContextOptions<LawnMowingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingConflict> BookingConflicts { get; set; }

    public virtual DbSet<ConflictManager> ConflictManagers { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Machine> Machines { get; set; }

    public virtual DbSet<Operator> Operators { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=THAPELOBAPS;Database=LawnMowingDB;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bookings__3214EC07C66ADC72");

            entity.Property(e => e.IsAcknowledged).HasDefaultValue(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Bookings__Custom__52593CB8");

            entity.HasOne(d => d.Machine).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MachineId)
                .HasConstraintName("FK__Bookings__Machin__534D60F1");
        });

        modelBuilder.Entity<BookingConflict>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingC__3214EC07E9E97646");

            entity.HasOne(d => d.ConflictManager).WithMany(p => p.BookingConflicts)
                .HasForeignKey(d => d.ConflictManagerId)
                .HasConstraintName("FK__BookingCo__Confl__5AEE82B9");

            entity.HasOne(d => d.OriginalBooking).WithMany(p => p.BookingConflicts)
                .HasForeignKey(d => d.OriginalBookingId)
                .HasConstraintName("FK__BookingCo__Origi__59063A47");

            entity.HasOne(d => d.ReassignedMachine).WithMany(p => p.BookingConflicts)
                .HasForeignKey(d => d.ReassignedMachineId)
                .HasConstraintName("FK__BookingCo__Reass__59FA5E80");
        });

        modelBuilder.Entity<ConflictManager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Conflict__3214EC07431B47CA");

            entity.HasIndex(e => e.Email, "UQ__Conflict__A9D10534FFC1D875").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07B47FC462");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D1053432A457D4").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Machines__3214EC0716FE5394");

            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Operator).WithMany(p => p.Machines)
                .HasForeignKey(d => d.OperatorId)
                .HasConstraintName("FK__Machines__Operat__4D94879B");
        });

        modelBuilder.Entity<Operator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Operator__3214EC077F09BF7C");

            entity.HasIndex(e => e.Email, "UQ__Operator__A9D105341BD3108B").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.HasOne(d => d.AssignedMachine).WithMany(p => p.Operators)
                .HasForeignKey(d => d.AssignedMachineId)
                .HasConstraintName("FK_Operators_Machines");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
