using System;
using System.Collections.Generic;
using ManageEmployee.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ManageEmployee.Models
{
    public partial class QuanLyNhanVienContext : DbContext
    {
        public QuanLyNhanVienContext()
        {
        }

        public QuanLyNhanVienContext(DbContextOptions<QuanLyNhanVienContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Certificate> Certificates { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Experience> Experiences { get; set; } = null!;
        public virtual DbSet<Fresher> Freshers { get; set; } = null!;
        public virtual DbSet<Intern> Interns { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.ToTable("Certificate");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Certificates)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Certificate_Employee");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");
            });

            modelBuilder.Entity<Experience>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("Experience");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeeID");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.Experience)
                    .HasForeignKey<Experience>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Experience_Employee");
            });

            modelBuilder.Entity<Fresher>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("Fresher");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.GraduationDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.Fresher)
                    .HasForeignKey<Fresher>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fresher_Employee");
            });

            modelBuilder.Entity<Intern>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("Intern");

                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmployeeID");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.Intern)
                    .HasForeignKey<Intern>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Intern_Employee");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
