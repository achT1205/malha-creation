namespace Catalog.Infrastructure.Data.Configurations;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Configuration de la clé primaire
        builder.HasKey(p => p.Id);

        // Configuration des propriétés basiques
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200); // Exemple de contrainte de longueur

        builder.Property(p => p.UrlFriendlyName)
            .IsRequired()
            .HasMaxLength(300); // Exemple de longueur maximale pour un slug

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(1000); // Exemple de longueur maximale pour la description

        builder.Property(p => p.IsHandmade)
            .IsRequired();

        // Relations avec des entités simples
        builder.HasOne(p => p.ProductType)
            .WithMany()
            .HasForeignKey("ProductTypeId")
            .IsRequired();

        builder.HasOne(p => p.Material)
            .WithMany()
            .HasForeignKey("MaterialId")
            .IsRequired();

        builder.HasOne(p => p.Collection)
            .WithMany()
            .HasForeignKey("CollectionId")
            .IsRequired();

        builder.HasOne(p => p.CoverImage)
            .WithMany()
            .HasForeignKey("CoverImageId")
            .IsRequired();

        // Relations avec les catégories (Many-to-Many)
        builder.HasMany(p => p.Categories)
            .WithMany("Products") // Assuming 'Products' is the navigation property on Category
            .UsingEntity(j => j.ToTable("ProductCategories"));

        // Relations avec les occasions (Many-to-Many)
        builder.HasMany(p => p.Occasions)
            .WithMany("Products") // Assuming 'Products' is the navigation property on Occasion
            .UsingEntity(j => j.ToTable("ProductOccasions"));

        // Relations avec ColorVariants (One-to-Many)
        builder.HasMany(p => p.ColorVariants)
            .WithOne()
            .HasForeignKey("ProductId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete if a product is deleted

        // Indices pour optimiser les recherches
        builder.HasIndex(p => p.UrlFriendlyName)
            .IsUnique(); // Ensure that the slug is unique

        // Table name
        builder.ToTable("Products");
    }
}
