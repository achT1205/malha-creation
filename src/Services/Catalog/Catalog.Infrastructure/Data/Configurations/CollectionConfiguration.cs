
namespace Catalog.Infrastructure.Data.Configurations;


internal sealed class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder.ToTable("Collections");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    dbId => CollectionId.Of(dbId));

        builder.Property(c => c.Name)
            .HasMaxLength(100);

        builder.Property(c => c.Description).HasMaxLength(500);

        builder.ComplexProperty(
              c => c.CoverImage, cb =>
              {
                  cb.Property(ci => ci.ImageSrc)
                      .HasColumnName(nameof(Product.CoverImage.ImageSrc))
                      .IsRequired();

                  cb.Property(ci => ci.AltText)
                     .HasColumnName(nameof(Product.CoverImage.AltText))
                     .IsRequired();
              });
    }
}