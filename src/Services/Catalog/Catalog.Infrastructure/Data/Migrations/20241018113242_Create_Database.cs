using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlFriendlyName_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AverageRating_Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AverageRating_TotalRatingsCount = table.Column<int>(type: "int", nullable: false),
                    CoverImage_ImageSrc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverImage_AltText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHandmade = table.Column<bool>(type: "bit", nullable: false),
                    ProductTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryIds_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColorVariants",
                columns: table => new
                {
                    ColorVariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity_Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorVariants", x => new { x.ColorVariantId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ColorVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OccasionIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OccasionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccasionIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OccasionIds_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviewIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviewIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReviewIds_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ColorVariants_Images",
                columns: table => new
                {
                    ColorVariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorVariantProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageSrc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorVariants_Images", x => new { x.ColorVariantId, x.ColorVariantProductId, x.Id });
                    table.ForeignKey(
                        name: "FK_ColorVariants_Images_ColorVariants_ColorVariantId_ColorVariantProductId",
                        columns: x => new { x.ColorVariantId, x.ColorVariantProductId },
                        principalTable: "ColorVariants",
                        principalColumns: new[] { "ColorVariantId", "ProductId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryIds_ProductId",
                table: "CategoryIds",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ColorVariants_ProductId",
                table: "ColorVariants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OccasionIds_ProductId",
                table: "OccasionIds",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviewIds_ProductId",
                table: "ProductReviewIds",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryIds");

            migrationBuilder.DropTable(
                name: "ColorVariants_Images");

            migrationBuilder.DropTable(
                name: "OccasionIds");

            migrationBuilder.DropTable(
                name: "ProductReviewIds");

            migrationBuilder.DropTable(
                name: "ColorVariants");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
