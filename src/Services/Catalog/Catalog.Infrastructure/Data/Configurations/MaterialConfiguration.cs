
namespace Catalog.Infrastructure.Data.Configurations;


internal sealed class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.ToTable("Materials");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
        .ValueGeneratedNever()
        .HasConversion(
            id => id.Value,
            dbId => MaterialId.Of(dbId));


        builder.Property(m => m.Name)
            .HasMaxLength(100);

        builder.Property(c => c.Description).HasMaxLength(500);

    }
}