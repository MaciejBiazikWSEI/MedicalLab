using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MedicalLab.Models;

namespace MedicalLab.Data
{
    public partial class MedicalLabContext : DbContext
    {
        public MedicalLabContext()
        {
        }

        public MedicalLabContext(DbContextOptions<MedicalLabContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Parameter> Parameters { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Result> Results { get; set; } = null!;
        public virtual DbSet<Sample> Samples { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<TestType> TestTypes { get; set; } = null!;
        public virtual DbSet<Tester> Testers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MedicalLab");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.HasOne(d => d.TestTypeNameNavigation)
                    .WithMany(p => p.Parameters)
                    .HasForeignKey(d => d.TestTypeName)
                    .HasConstraintName("FK__Parameter__TestT__2E1BDC42");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Patients__A25C5AA63D960BA2");

                entity.Property(e => e.Pesel).IsFixedLength();
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasKey(e => new { e.TestCode, e.ParameterId })
                    .HasName("PK__Results__648CF3D145854DCD");

                entity.HasOne(d => d.Parameter)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.ParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Results__Paramet__3A81B327");

                entity.HasOne(d => d.TestCodeNavigation)
                    .WithMany(p => p.Results)
                    .HasForeignKey(d => d.TestCode)
                    .HasConstraintName("FK__Results__TestCod__398D8EEE");
            });

            modelBuilder.Entity<Sample>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Samples__A25C5AA6AFC9BB1D");

                entity.HasOne(d => d.PatientCodeNavigation)
                    .WithMany(p => p.Samples)
                    .HasForeignKey(d => d.PatientCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Samples__Patient__30F848ED");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Tests__A25C5AA605BCDABF");

                entity.HasOne(d => d.SampleCodeNavigation)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.SampleCode)
                    .HasConstraintName("FK__Tests__SampleCod__34C8D9D1");

                entity.HasOne(d => d.TestTypeNameNavigation)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TestTypeName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tests__TestTypeN__33D4B598");

                entity.HasOne(d => d.Tester)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tests__TesterId__35BCFE0A");
            });

            modelBuilder.Entity<TestType>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__TestType__737584F739705542");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
