
namespace Catalog.Infrastructure.Data.Configurations;


internal sealed class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    dbId => BrandId.Of(dbId));

        builder.ComplexProperty(
            b => b.Name, nb =>
            {
                nb.Property(n => n.Value)
                    .HasColumnName(nameof(Brand.Name))
                    .HasMaxLength(100)
                    .IsRequired()
                    ;
            }
        );

        builder.Property(b => b.Description).HasMaxLength(500);
        builder.ComplexProperty(
            b => b.WebsiteUrl, nb =>
            {
                nb.Property(n => n.Value)
                    .HasColumnName(nameof(Brand.WebsiteUrl))
                    .HasMaxLength(500)
                    .IsRequired()
                    ;
            }
        );

        builder.ComplexProperty(
              c => c.Logo, bb =>
              {
                  bb.Property(ci => ci.ImageSrc)
                      .HasColumnName(nameof(Product.CoverImage.ImageSrc))
                      .IsRequired();

                  bb.Property(ci => ci.AltText)
                     .HasColumnName(nameof(Product.CoverImage.AltText))
                     .IsRequired();
              }
        );
    }
}