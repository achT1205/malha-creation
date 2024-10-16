namespace Catalog.Infrastructure.Data.Configurations;

public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        // Configuration de la clé primaire
        builder.HasKey(c => c.Id);

        // Mapping de l'ID avec la colonne "CollectionId"
        builder.Property(c => c.Id)
            .HasColumnName("CollectionId")
            .IsRequired();

        // Configuration de la propriété Name
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100); // Limite de longueur pour le nom

        // Relation One-to-One avec Image
        builder.HasOne(c => c.Image)
            .WithMany() // Pas de relation inverse
            .HasForeignKey("ImageId") // La clé étrangère vers Image
            .IsRequired();

        // Nom de la table
        builder.ToTable("Collections");
    }
}
