﻿// <auto-generated />
using System;
using Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Catalog.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CollectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsHandmade")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MaterialId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Product", b =>
                {
                    b.OwnsMany("Catalog.Domain.Models.ColorVariant", "ColorVariants", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("ColorVariantId");

                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CreatedBy")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime?>("LastModified")
                                .HasColumnType("datetime2");

                            b1.Property<string>("LastModifiedBy")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Id", "ProductId");

                            b1.HasIndex("ProductId");

                            b1.ToTable("ColorVariants", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ProductId");

                            b1.OwnsMany("Catalog.Domain.ValueObjects.Image", "Images", b2 =>
                                {
                                    b2.Property<Guid>("ColorVariantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("ColorVariantProductId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b2.Property<int>("Id"));

                                    b2.Property<string>("AltText")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("ImageSrc")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("ColorVariantId", "ColorVariantProductId", "Id");

                                    b2.ToTable("ColorVariants_Images");

                                    b2.WithOwner()
                                        .HasForeignKey("ColorVariantId", "ColorVariantProductId");
                                });

                            b1.OwnsOne("Catalog.Domain.ValueObjects.Color", "Color", b2 =>
                                {
                                    b2.Property<Guid>("ColorVariantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("ColorVariantProductId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("ColorVariantId", "ColorVariantProductId");

                                    b2.ToTable("ColorVariants");

                                    b2.WithOwner()
                                        .HasForeignKey("ColorVariantId", "ColorVariantProductId");
                                });

                            b1.OwnsOne("Catalog.Domain.ValueObjects.Price", "Price", b2 =>
                                {
                                    b2.Property<Guid>("ColorVariantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("ColorVariantProductId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<decimal>("Amount")
                                        .HasColumnType("decimal(18,2)");

                                    b2.HasKey("ColorVariantId", "ColorVariantProductId");

                                    b2.ToTable("ColorVariants");

                                    b2.WithOwner()
                                        .HasForeignKey("ColorVariantId", "ColorVariantProductId");

                                    b2.OwnsOne("Catalog.Domain.ValueObjects.Currency", "Currency", b3 =>
                                        {
                                            b3.Property<Guid>("PriceColorVariantId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<Guid>("PriceColorVariantProductId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<string>("Value")
                                                .IsRequired()
                                                .HasMaxLength(100)
                                                .HasColumnType("nvarchar(100)")
                                                .HasColumnName("Currency");

                                            b3.HasKey("PriceColorVariantId", "PriceColorVariantProductId");

                                            b3.ToTable("ColorVariants");

                                            b3.WithOwner()
                                                .HasForeignKey("PriceColorVariantId", "PriceColorVariantProductId");
                                        });

                                    b2.Navigation("Currency")
                                        .IsRequired();
                                });

                            b1.OwnsOne("Catalog.Domain.ValueObjects.Quantity", "Quantity", b2 =>
                                {
                                    b2.Property<Guid>("ColorVariantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("ColorVariantProductId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("Value")
                                        .HasColumnType("int");

                                    b2.HasKey("ColorVariantId", "ColorVariantProductId");

                                    b2.ToTable("ColorVariants");

                                    b2.WithOwner()
                                        .HasForeignKey("ColorVariantId", "ColorVariantProductId");
                                });

                            b1.OwnsOne("Catalog.Domain.ValueObjects.Slug", "Slug", b2 =>
                                {
                                    b2.Property<Guid>("ColorVariantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("ColorVariantProductId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("ColorVariantId", "ColorVariantProductId");

                                    b2.ToTable("ColorVariants");

                                    b2.WithOwner()
                                        .HasForeignKey("ColorVariantId", "ColorVariantProductId");
                                });

                            b1.Navigation("Color")
                                .IsRequired();

                            b1.Navigation("Images");

                            b1.Navigation("Price")
                                .IsRequired();

                            b1.Navigation("Quantity")
                                .IsRequired();

                            b1.Navigation("Slug")
                                .IsRequired();
                        });

                    b.OwnsOne("Catalog.Domain.ValueObjects.AverageRating", "AverageRating", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("TotalRatingsCount")
                                .HasColumnType("int");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsMany("Catalog.Domain.ValueObjects.CategoryId", "CategoryIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("CategoryId");

                            b1.HasKey("Id");

                            b1.HasIndex("ProductId");

                            b1.ToTable("CategoryIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsMany("Catalog.Domain.ValueObjects.OccasionId", "OccasionIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("OccasionId");

                            b1.HasKey("Id");

                            b1.HasIndex("ProductId");

                            b1.ToTable("OccasionIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Catalog.Domain.ValueObjects.ProductDescription", "Description", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Catalog.Domain.ValueObjects.ProductName", "Name", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsMany("Catalog.Domain.ValueObjects.ProductReviewId", "ProductReviewIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("ReviewId");

                            b1.HasKey("Id");

                            b1.HasIndex("ProductId");

                            b1.ToTable("ProductReviewIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Catalog.Domain.ValueObjects.UrlFriendlyName", "UrlFriendlyName", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Catalog.Domain.ValueObjects.Image", "CoverImage", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("AltText")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ImageSrc")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("AverageRating")
                        .IsRequired();

                    b.Navigation("CategoryIds");

                    b.Navigation("ColorVariants");

                    b.Navigation("CoverImage")
                        .IsRequired();

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("OccasionIds");

                    b.Navigation("ProductReviewIds");

                    b.Navigation("UrlFriendlyName")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
