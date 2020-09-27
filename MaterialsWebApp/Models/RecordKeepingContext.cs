using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MaterialsWebApp.Models
{
    public partial class RecordKeepingContext : DbContext
    {
        public RecordKeepingContext()
        {
        }

        public RecordKeepingContext(DbContextOptions<RecordKeepingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<Detail> Details { get; set; }
        public virtual DbSet<IncomeExpenseBook> IncomeExpenseBook { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Unit> Units { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Component>(entity =>
            {
                entity.HasKey(e => e.ComponentId)
                    .HasName("PK__Componen__D79CF02E82EBF152");

                entity.Property(e => e.ComponentId).HasColumnName("ComponentID");

                entity.Property(e => e.DetailId).HasColumnName("DetailID");

                entity.Property(e => e.MaterialId).HasColumnName("MaterialID");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.HasOne(d => d.Detail)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.DetailId)
                    .HasConstraintName("FK_Components_Details");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.MaterialId)
                    .HasConstraintName("FK_Components_Materials");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Components)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_Components_Units");
            });

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.HasKey(e => e.DetailId)
                    .HasName("PK__Details__135C314D50231776");

                entity.Property(e => e.DetailId).HasColumnName("DetailID");
            });

            modelBuilder.Entity<IncomeExpenseBook>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK__IncomeEx__FBDF78C9ACE97779");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.ComponentId).HasColumnName("ComponentID");

                entity.HasOne(d => d.Component)
                    .WithMany(p => p.IncomeExpenseBook)
                    .HasForeignKey(d => d.ComponentId)
                    .HasConstraintName("FK_IncomeExpenseBook_Components");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.MaterialId)
                    .HasName("PK__Material__C506131728B1635F");

                entity.Property(e => e.MaterialId).HasColumnName("MaterialID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasKey(e => e.UnitId)
                    .HasName("PK__Units__44F5EC95EB368EFA");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
