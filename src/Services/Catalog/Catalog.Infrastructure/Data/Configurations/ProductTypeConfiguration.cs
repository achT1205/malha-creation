
namespace Catalog.Infrastructure.Data.Configurations;


internal sealed class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.ToTable("ProductTypes");

        builder.Property(p => p.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    dbId => ProductTypeId.Of(dbId));

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name)
            .HasMaxLength(100);
    }
}