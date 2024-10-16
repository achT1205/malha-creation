namespace Catalog.Infrastructure.Data.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        // Configuration de la clé primaire
        builder.HasKey(i => i.Id);

        // Mapping de l'ID avec la colonne "ImageId"
        builder.Property(i => i.Id)
            .HasColumnName("ImageId")
            .IsRequired();

        // Configuration de la propriété ImageSrc
        builder.Property(i => i.ImageSrc)
            .IsRequired()
            .HasMaxLength(500); // Exemple de longueur maximale

        // Configuration de la propriété AltText
        builder.Property(i => i.AltText)
            .HasMaxLength(200); // Limite de longueur pour le texte alternatif

        // Nom de la table
        builder.ToTable("Images");
    }
}

