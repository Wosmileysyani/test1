using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WorkShop2.Models
{
    public partial class Kruweb1Context : DbContext
    {
        public Kruweb1Context()
        {
        }

        public Kruweb1Context(DbContextOptions<Kruweb1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Initial> Initials { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<Unit> Units { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CatId)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CatNeme).IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustId)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.InitialCode).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.CustTypeNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CustType)
                    .HasConstraintName("FK_Customer_ToTable_1");

                entity.HasOne(d => d.InitialCodeNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.InitialCode)
                    .HasConstraintName("FK_Customer_ToTable");
            });

            modelBuilder.Entity<Initial>(entity =>
            {
                entity.Property(e => e.InitialCode)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.InitialName).IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CatId).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.UnitCode).IsUnicode(false);

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CatId)
                    .HasConstraintName("FK_Product_ToTable_1");

                entity.HasOne(d => d.UnitCodeNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UnitCode)
                    .HasConstraintName("FK_Product_ToTable");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.CustType).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.UnitCode)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
