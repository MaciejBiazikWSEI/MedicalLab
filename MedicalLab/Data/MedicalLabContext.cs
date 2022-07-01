using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MedicalLab.Models;

namespace MedicalLab.Data
{
    /// <summary>
    /// Medical Lab application database context
    /// </summary>
    public partial class MedicalLabContext : DbContext
    {
        public MedicalLabContext()
        {
        }

        public MedicalLabContext(DbContextOptions<MedicalLabContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Sample> Samples { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<Tester> Testers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MedicalLab");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Patients__A25C5AA63D960BA2");
            });

            modelBuilder.Entity<Sample>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Samples__A25C5AA6AFC9BB1D");

                entity.HasOne(d => d.PatientCodeNavigation)
                    .WithMany(p => p.Samples)
                    .HasForeignKey(d => d.PatientCode)
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

                entity.HasOne(d => d.Tester)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tests__TesterId__35BCFE0A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
