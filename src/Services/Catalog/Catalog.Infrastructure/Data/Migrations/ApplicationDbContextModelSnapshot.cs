﻿// <auto-generated />
using System;
using System.Collections.Generic;
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

            modelBuilder.Entity("Catalog.Domain.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "Catalog.Domain.Models.Category.Name#CategoryName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Name");
                        });

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Catalog.Domain.Models.Collection", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.ComplexProperty<Dictionary<string, object>>("Image", "Catalog.Domain.Models.Collection.Image#Image", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("AltText")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("AltText");

                            b1.Property<string>("ImageSrc")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ImageSrc");
                        });

                    b.HasKey("Id");

                    b.ToTable("Collections", (string)null);
                });

            modelBuilder.Entity("Catalog.Domain.Models.Material", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Materials", (string)null);
                });

            modelBuilder.Entity("Catalog.Domain.Models.Occasion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "Catalog.Domain.Models.Occasion.Name#OccasionName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Name");
                        });

                    b.HasKey("Id");

                    b.ToTable("Occasions", (string)null);
                });

            modelBuilder.Entity("Catalog.Domain.Models.ProductType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes", (string)null);
                });

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

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UrlFriendlyName_Value")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("UrlFriendlyName");

                    b.ComplexProperty<Dictionary<string, object>>("AverageRating", "Product.AverageRating#AverageRating", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("TotalRatingsCount")
                                .HasColumnType("int")
                                .HasColumnName("TotalRatingsCount");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("AverageRating");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("CoverImage", "Product.CoverImage#Image", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("AltText")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("AltText");

                            b1.Property<string>("ImageSrc")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ImageSrc");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "Product.Description#ProductDescription", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "Product.Name#ProductName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Name");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("UrlFriendlyName", "Product.UrlFriendlyName#UrlFriendlyName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("UrlFriendlyName");
                        });

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("UrlFriendlyName_Value")
                        .IsUnique();

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Product", b =>
                {
                    b.HasOne("Catalog.Domain.Models.Collection", null)
                        .WithMany()
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Catalog.Domain.Models.Material", null)
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Catalog.Domain.Models.ProductType", null)
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

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

                            b1.Property<string>("Slug_Value")
                                .IsRequired()
                                .ValueGeneratedOnUpdateSometimes()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Slug");

                            b1.HasKey("Id", "ProductId");

                            b1.HasIndex("ProductId");

                            b1.HasIndex("Slug_Value")
                                .IsUnique();

                            b1.ToTable("ColorVariants", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ProductId");

                            b1.OwnsOne("Catalog.Domain.ValueObjects.Color", "Color", b2 =>
                                {
                                    b2.Property<Guid>("ColorVariantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("ColorVariantProductId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("nvarchar(50)")
                                        .HasColumnName("Color");

                                    b2.HasKey("ColorVariantId", "ColorVariantProductId");

                                    b2.ToTable("ColorVariants");

                                    b2.WithOwner()
                                        .HasForeignKey("ColorVariantId", "ColorVariantProductId");
                                });

                            b1.OwnsOne("Catalog.Domain.ValueObjects.ColorVariantPrice", "Price", b2 =>
                                {
                                    b2.Property<Guid>("ColorVariantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("ColorVariantProductId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<decimal?>("Amount")
                                        .HasColumnType("decimal(18,2)")
                                        .HasColumnName("Price");

                                    b2.Property<string>("Currency")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("Currency");

                                    b2.HasKey("ColorVariantId", "ColorVariantProductId");

                                    b2.ToTable("ColorVariants");

                                    b2.WithOwner()
                                        .HasForeignKey("ColorVariantId", "ColorVariantProductId");
                                });

                            b1.OwnsOne("Catalog.Domain.ValueObjects.ColorVariantQuantity", "Quantity", b2 =>
                                {
                                    b2.Property<Guid>("ColorVariantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("ColorVariantProductId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int?>("Value")
                                        .HasColumnType("int")
                                        .HasColumnName("Quantity");

                                    b2.HasKey("ColorVariantId", "ColorVariantProductId");

                                    b2.ToTable("ColorVariants");

                                    b2.WithOwner()
                                        .HasForeignKey("ColorVariantId", "ColorVariantProductId");
                                });

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

                                    b2.ToTable("ColorVariantImages", (string)null);

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
                                        .ValueGeneratedOnUpdateSometimes()
                                        .HasMaxLength(200)
                                        .HasColumnType("nvarchar(200)")
                                        .HasColumnName("Slug");

                                    b2.HasKey("ColorVariantId", "ColorVariantProductId");

                                    b2.ToTable("ColorVariants");

                                    b2.WithOwner()
                                        .HasForeignKey("ColorVariantId", "ColorVariantProductId");
                                });

                            b1.OwnsMany("Catalog.Domain.Models.SizeVariant", "SizeVariants", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .HasColumnType("uniqueidentifier")
                                        .HasColumnName("SizeVariantId");

                                    b2.Property<Guid>("ColorVariantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("ProductId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<DateTime?>("CreatedAt")
                                        .HasColumnType("datetime2");

                                    b2.Property<string>("CreatedBy")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<DateTime?>("LastModified")
                                        .HasColumnType("datetime2");

                                    b2.Property<string>("LastModifiedBy")
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("Id", "ColorVariantId", "ProductId");

                                    b2.HasIndex("ColorVariantId", "ProductId");

                                    b2.ToTable("SizeVariants", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("ColorVariantId", "ProductId");

                                    b2.OwnsOne("Catalog.Domain.ValueObjects.Price", "Price", b3 =>
                                        {
                                            b3.Property<Guid>("SizeVariantId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<Guid>("SizeVariantColorVariantId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<Guid>("SizeVariantProductId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<decimal>("Amount")
                                                .HasColumnType("decimal(18,2)")
                                                .HasColumnName("Price");

                                            b3.Property<string>("Currency")
                                                .IsRequired()
                                                .HasColumnType("nvarchar(max)")
                                                .HasColumnName("Currency");

                                            b3.HasKey("SizeVariantId", "SizeVariantColorVariantId", "SizeVariantProductId");

                                            b3.ToTable("SizeVariants");

                                            b3.WithOwner()
                                                .HasForeignKey("SizeVariantId", "SizeVariantColorVariantId", "SizeVariantProductId");
                                        });

                                    b2.OwnsOne("Catalog.Domain.ValueObjects.Quantity", "Quantity", b3 =>
                                        {
                                            b3.Property<Guid>("SizeVariantId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<Guid>("SizeVariantColorVariantId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<Guid>("SizeVariantProductId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<int>("Value")
                                                .HasColumnType("int")
                                                .HasColumnName("Quantity");

                                            b3.HasKey("SizeVariantId", "SizeVariantColorVariantId", "SizeVariantProductId");

                                            b3.ToTable("SizeVariants");

                                            b3.WithOwner()
                                                .HasForeignKey("SizeVariantId", "SizeVariantColorVariantId", "SizeVariantProductId");
                                        });

                                    b2.OwnsOne("Catalog.Domain.ValueObjects.Size", "Size", b3 =>
                                        {
                                            b3.Property<Guid>("SizeVariantId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<Guid>("SizeVariantColorVariantId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<Guid>("SizeVariantProductId")
                                                .HasColumnType("uniqueidentifier");

                                            b3.Property<string>("Value")
                                                .IsRequired()
                                                .HasMaxLength(5)
                                                .HasColumnType("nvarchar(5)")
                                                .HasColumnName("Size");

                                            b3.HasKey("SizeVariantId", "SizeVariantColorVariantId", "SizeVariantProductId");

                                            b3.ToTable("SizeVariants");

                                            b3.WithOwner()
                                                .HasForeignKey("SizeVariantId", "SizeVariantColorVariantId", "SizeVariantProductId");
                                        });

                                    b2.Navigation("Price")
                                        .IsRequired();

                                    b2.Navigation("Quantity")
                                        .IsRequired();

                                    b2.Navigation("Size")
                                        .IsRequired();
                                });

                            b1.Navigation("Color")
                                .IsRequired();

                            b1.Navigation("Images");

                            b1.Navigation("Price")
                                .IsRequired();

                            b1.Navigation("Quantity")
                                .IsRequired();

                            b1.Navigation("SizeVariants");

                            b1.Navigation("Slug")
                                .IsRequired();
                        });

                    b.OwnsMany("Catalog.Domain.Models.Review", "Reviews", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Comment")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Comment");

                            b1.Property<DateTime?>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CreatedBy")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime>("DatePosted")
                                .HasColumnType("datetime2")
                                .HasColumnName("DatePosted");

                            b1.Property<DateTime?>("LastModified")
                                .HasColumnType("datetime2");

                            b1.Property<string>("LastModifiedBy")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ReviewerId")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("ReviewerId");

                            b1.HasKey("Id");

                            b1.HasIndex("ProductId");

                            b1.ToTable("Reviews", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ProductId");

                            b1.OwnsOne("Catalog.Domain.ValueObjects.Rating", "Rating", b2 =>
                                {
                                    b2.Property<Guid>("ReviewId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("Value")
                                        .HasColumnType("int")
                                        .HasColumnName("Rating");

                                    b2.HasKey("ReviewId");

                                    b2.ToTable("Reviews");

                                    b2.WithOwner()
                                        .HasForeignKey("ReviewId");
                                });

                            b1.Navigation("Rating")
                                .IsRequired();
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

                            b1.ToTable("ProductCategory", (string)null);

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

                            b1.ToTable("ProductOccasion", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("CategoryIds");

                    b.Navigation("ColorVariants");

                    b.Navigation("OccasionIds");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
