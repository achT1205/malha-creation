namespace Catalog.Infrastructure.Data.Configurations;

public class ClothingColorVariantConfiguration : IEntityTypeConfiguration<ClothingColorVariant>
{
    public void Configure(EntityTypeBuilder<ClothingColorVariant> builder)
    {
        // Configure the base class (ColorVariantBase)
        builder.HasBaseType<ColorVariantBase>();

        // Configure the table name for ClothingColorVariants
        builder.ToTable("ClothingColorVariants");

        // Configuration des propriétés spécifiques à ClothingColorVariant
        builder.Property(ccv => ccv.Color)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(ccv => ccv.Slug)
               .IsRequired()
               .HasMaxLength(100);

        // Configuration des images (relation 1:n)
        builder.HasMany(ccv => ccv.Images)
               .WithOne()
               .HasForeignKey("ClothingColorVariantId")
               .OnDelete(DeleteBehavior.Cascade);

        // Configuration des SizeVariants (ValueObject - propriété possédée)
        builder.OwnsMany(ccv => ccv.SizeVariants, sb =>
        {
            sb.ToTable("SizeVariants");
            sb.WithOwner().HasForeignKey("ClothingColorVariantId");

            // Configuration des propriétés du ValueObject SizeVariant
            sb.Property(sv => sv.Size)
              .IsRequired()
              .HasColumnName("Size")
              .HasMaxLength(10);

            sb.Property(sv => sv.Price)
              .IsRequired()
              .HasColumnName("Price")
              .HasPrecision(18, 2);

            sb.Property(sv => sv.Quantity)
              .IsRequired()
              .HasColumnName("Quantity");

            // Définir la clé composite pour SizeVariant
            sb.HasKey("ClothingColorVariantId", "Size"); // Assuming Size as part of unique key
        });
    }
}
