
namespace Catalog.Infrastructure.Data.Configurations;


internal sealed class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    dbId => BrandId.Of(dbId));

        builder.ComplexProperty(
            b => b.Name, nb =>
            {
                nb.Property(n => n.Value)
                    .HasColumnName(nameof(Brand.Name))
                    .HasMaxLength(100)
                    .IsRequired()
                    ;
            });
    }
}