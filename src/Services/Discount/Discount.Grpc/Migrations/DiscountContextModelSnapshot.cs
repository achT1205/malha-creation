﻿// <auto-generated />
using Discount.Grpc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Discount.Grpc.Migrations
{
    [DbContext(typeof(DiscountContext))]
    partial class DiscountContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("Discount.Grpc.Models.CartCoupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("discountRate")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("CartCoupons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CouponCode = "SAVE10",
                            Description = "Get 10% off on your purchase",
                            discountRate = 10
                        },
                        new
                        {
                            Id = 2,
                            CouponCode = "FREESHIP",
                            Description = "Free shipping on orders over $50",
                            discountRate = 0
                        });
                });

            modelBuilder.Entity("Discount.Grpc.Models.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 5,
                            Description = "Get $5 off on product purchase",
                            ProductId = "019235b3-4946-40ae-8078-710e338b7b4a"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 10,
                            Description = "ISpecial discount of $10 on selected item",
                            ProductId = "019235b3-4946-40ae-8078-710e338b7b4a"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}