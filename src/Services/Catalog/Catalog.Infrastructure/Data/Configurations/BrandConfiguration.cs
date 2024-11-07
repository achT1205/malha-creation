
namespace Catalog.Infrastructure.Data.Configurations;


internal sealed class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    dbId => BrandId.Of(dbId));

        builder.Property(c => c.Name)
            .HasMaxLength(100);
    }
}