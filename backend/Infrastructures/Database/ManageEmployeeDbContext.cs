using System;
using System.Collections.Generic;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructures.Database;

public partial class ManageEmployeeDbContext : DbContext
{
    public ManageEmployeeDbContext() { }

    public ManageEmployeeDbContext(DbContextOptions<ManageEmployeeDbContext> options)
        : base(options) { }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Leaverequest> Leaverequests { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        =>
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=postgres;User Id=postgres;Password=example;"
        );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("attendance_pkey");

            entity.ToTable("attendance");

            entity.Property(e => e.AttendanceId).HasColumnName("attendance_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity
                .HasOne(d => d.Employee)
                .WithMany(p => p.Attendances)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("attendance_employee_id_fkey");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("department_pkey");

            entity.ToTable("department");

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Address).HasMaxLength(255).HasColumnName("address");
            entity.Property(e => e.Description).HasMaxLength(255).HasColumnName("description");
            entity.Property(e => e.Name).HasMaxLength(255).HasColumnName("name");

            entity
                .HasMany(d => d.Employees)
                .WithMany(p => p.Departments)
                .UsingEntity<Dictionary<string, object>>(
                    "Departmentemployee",
                    r =>
                        r.HasOne<Employee>()
                            .WithMany()
                            .HasForeignKey("EmployeeId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("departmentemployee_employee_id_fkey"),
                    l =>
                        l.HasOne<Department>()
                            .WithMany()
                            .HasForeignKey("DepartmentId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("departmentemployee_department_id_fkey"),
                    j =>
                    {
                        j.HasKey("DepartmentId", "EmployeeId").HasName("departmentemployee_pkey");
                        j.ToTable("departmentemployee");
                        j.IndexerProperty<int>("DepartmentId").HasColumnName("department_id");
                        j.IndexerProperty<int>("EmployeeId").HasColumnName("employee_id");
                    }
                );
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email");
            entity.Property(e => e.FirstName).HasMaxLength(255).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasMaxLength(255).HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20).HasColumnName("phone_number");
            entity.Property(e => e.Position).HasColumnName("position");
        });

        modelBuilder.Entity<Leaverequest>(entity =>
        {
            entity.HasKey(e => e.LeaveRequestId).HasName("leaverequest_pkey");

            entity.ToTable("leaverequest");

            entity.Property(e => e.LeaveRequestId).HasColumnName("leave_request_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.RequestDate).HasColumnName("request_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity
                .HasOne(d => d.Employee)
                .WithMany(p => p.Leaverequests)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("leaverequest_employee_id_fkey");

            entity
                .HasOne(d => d.Status)
                .WithMany(p => p.Leaverequests)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("leaverequest_status_id_fkey");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("status_pkey");

            entity.ToTable("status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName).HasMaxLength(255).HasColumnName("status_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
