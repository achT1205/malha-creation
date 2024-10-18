
namespace Catalog.Infrastructure.Data.Configurations;


internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    dbId => CategoryId.Of(dbId));


        builder.HasKey(c => c.Id);

        builder.ComplexProperty(
                c => c.Name, cb =>
                {
                    cb.Property(n => n.Value)
                        .HasColumnName(nameof(Category.Name))
                        .HasMaxLength(100)
                        .IsRequired();
                });

    }
}