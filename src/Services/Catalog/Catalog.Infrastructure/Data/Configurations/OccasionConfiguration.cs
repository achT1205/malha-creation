namespace Catalog.Infrastructure.Data.Configurations;
public class OccasionConfiguration : IEntityTypeConfiguration<Occasion>
{
    public void Configure(EntityTypeBuilder<Occasion> builder)
    {
        // Configuration de la clé primaire
        builder.HasKey(o => o.Id);

        // Mapping de l'ID avec la colonne "OccasionId"
        builder.Property(o => o.Id)
            .HasColumnName("OccasionId")
            .IsRequired();

        // Configuration de la propriété Name
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(100); // Limite de longueur sur le nom

        // Index unique sur le nom de l'occasion
        builder.HasIndex(o => o.Name)
            .IsUnique(); // Chaque occasion doit avoir un nom unique

        // Nom de la table
        builder.ToTable("Occasions");
    }
}
