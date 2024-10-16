namespace Catalog.Infrastructure.Data.Configurations;
public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        // Configuration de la clé primaire
        builder.HasKey(pt => pt.Id);

        // Mapping de l'ID avec la colonne "ProductTypeId"
        builder.Property(pt => pt.Id)
            .HasColumnName("ProductTypeId")
            .IsRequired();

        // Configuration de la propriété Name
        builder.Property(pt => pt.Name)
            .IsRequired()
            .HasMaxLength(100); // Longueur maximale de 100 caractères

        // Index sur le nom du type de produit pour optimiser les recherches
        builder.HasIndex(pt => pt.Name)
            .IsUnique(); // Le nom du type de produit doit être unique

        // Nom de la table
        builder.ToTable("ProductTypes");
    }
}
