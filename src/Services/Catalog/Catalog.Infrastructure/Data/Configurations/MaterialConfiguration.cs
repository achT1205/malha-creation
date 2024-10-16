namespace Catalog.Infrastructure.Data.Configurations;

public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        // Configuration de la clé primaire
        builder.HasKey(m => m.Id);

        // Mapping de l'ID avec la colonne "MaterialId"
        builder.Property(m => m.Id)
            .HasColumnName("MaterialId")
            .IsRequired();

        // Configuration de la propriété Name
        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100); // Limite de longueur

        // Index unique sur le nom du matériau
        builder.HasIndex(m => m.Name)
            .IsUnique(); // Chaque matériau doit avoir un nom unique

        // Nom de la table
        builder.ToTable("Materials");
    }
}
