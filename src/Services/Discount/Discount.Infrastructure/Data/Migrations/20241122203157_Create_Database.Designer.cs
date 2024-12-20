﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Discount.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Discount.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241122203157_Create_Database")]
    partial class Create_Database
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Discount.Domain.Models.Coupon", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("DateTime")
                        .HasColumnName("EndDate");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFirstTimeOrderOnly")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaxUses")
                        .HasColumnType("int");

                    b.Property<int?>("MaxUsesPerCustomer")
                        .HasColumnType("int");

                    b.Property<decimal?>("MinimumOrderValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("DateTime")
                        .HasColumnName("StartDate");

                    b.Property<int?>("TotalRedemptions")
                        .HasColumnType("int");

                    b.ComplexProperty<Dictionary<string, object>>("Code", "Discount.Domain.Models.Coupon.Code#CouponCode", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("nvarchar(15)")
                                .HasColumnName("Code");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Discountable", "Discount.Domain.Models.Coupon.Discountable#Discountable", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal?>("FlatAmount")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("FlatAmount");

                            b1.Property<int?>("Percentage")
                                .HasColumnType("int")
                                .HasColumnName("Percentage");
                        });

                    b.HasKey("Id");

                    b.ToTable("Coupon", (string)null);
                });

            modelBuilder.Entity("Discount.Domain.Models.Coupon", b =>
                {
                    b.OwnsMany("Discount.Domain.ValueObjects.CustomerId", "AllowedCustomerIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("CouponId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("CustomerId");

                            b1.HasKey("Id");

                            b1.HasIndex("CouponId");

                            b1.ToTable("CouponCustomer", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CouponId");
                        });

                    b.OwnsMany("Discount.Domain.ValueObjects.ProductId", "ProductIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("CouponId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("ProductId");

                            b1.HasKey("Id");

                            b1.HasIndex("CouponId");

                            b1.ToTable("CouponProduct", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CouponId");
                        });

                    b.Navigation("AllowedCustomerIds");

                    b.Navigation("ProductIds");
                });
#pragma warning restore 612, 618
        }
    }
}
