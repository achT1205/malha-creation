namespace Catalog.Infrastructure.Data.Configurations;

public class AccessoryColorVariantConfiguration : IEntityTypeConfiguration<AccessoryColorVariant>
{
    public void Configure(EntityTypeBuilder<AccessoryColorVariant> builder)
    {
        // Utilisation de la table partagée avec ColorVariantBase
        builder.HasBaseType<ColorVariantBase>();

        // Mapping du prix
        builder.Property(a => a.Price)
            .IsRequired()
            .HasConversion(
                price => price.Value,
                value => Price.Of(value)
            );

        // Mapping de la quantité
        builder.Property(a => a.Quantity)
            .IsRequired()
            .HasConversion(
                quantity => quantity.Value,
                value => Quantity.Of(value)
            );

        // Nom de la table
        builder.ToTable("ColorVariants");
    }
}

