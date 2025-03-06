using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pr4_Partners.Models;

public partial class Pr2PartnersContext : DbContext
{
    public Pr2PartnersContext()
    {
    }

    public Pr2PartnersContext(DbContextOptions<Pr2PartnersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnersProduct> PartnersProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<TypesOfPartner> TypesOfPartners { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Pr2_Partners;Username=postgres;Password=1111");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Partners_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Email).HasColumnType("character varying");
            entity.Property(e => e.LegalAdress).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.NameOfDirector).HasColumnType("character varying");
            entity.Property(e => e.PhoneNumber).HasMaxLength(19);
            entity.Property(e => e.Tin)
                .HasMaxLength(15)
                .HasColumnName("TIN");

            entity.HasOne(d => d.IdTypeOfPartnerNavigation).WithMany(p => p.Partners)
                .HasForeignKey(d => d.IdTypeOfPartner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Partners_TypesOfPartner");
        });

        modelBuilder.Entity<PartnersProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Partners_Product_pkey");

            entity.ToTable("Partners_Product");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.IdPartnerNavigation).WithMany(p => p.PartnersProducts)
                .HasForeignKey(d => d.IdPartner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Partners_PP");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.PartnersProducts)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Product_PP");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Products_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.ArticleNumber)
                .HasColumnType("character varying")
                .HasColumnName("articleNumber");
            entity.Property(e => e.MinPriceForPartner).HasColumnType("money");
            entity.Property(e => e.Name).HasColumnType("character varying");

            entity.HasOne(d => d.IdProductTypeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdProductType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Products_ProductTypes");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductTypes_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.TypeOfProduct).HasColumnType("character varying");
        });

        modelBuilder.Entity<TypesOfPartner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TypesOfPartner_pkey");

            entity.ToTable("TypesOfPartner");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.TypeOfPartner).HasColumnType("character varying");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
