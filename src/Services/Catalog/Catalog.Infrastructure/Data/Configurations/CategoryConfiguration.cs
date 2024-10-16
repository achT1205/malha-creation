namespace Catalog.Infrastructure.Data.Configurations;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Configuration de la clé primaire
        builder.HasKey(c => c.Id);

        // Mapping de l'ID avec la colonne "CategoryId"
        builder.Property(c => c.Id)
            .HasColumnName("CategoryId")
            .IsRequired();

        // Configuration de la propriété Name
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100); // Limite de longueur sur le nom

        // Index unique sur le nom de la catégorie
        builder.HasIndex(c => c.Name)
            .IsUnique(); // Chaque catégorie doit avoir un nom unique

        // Nom de la table
        builder.ToTable("Categories");
    }
}
