
namespace Catalog.Infrastructure.Data.Configurations;


internal sealed class OccasionConfiguration : IEntityTypeConfiguration<Occasion>
{
    public void Configure(EntityTypeBuilder<Occasion> builder)
    {
        builder.ToTable("Occasions");

        builder.Property(o => o.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    dbId => OccasionId.Of(dbId));


        builder.HasKey(o => o.Id);

        builder.ComplexProperty(
                o => o.Name, ob =>
                {
                    ob.Property(n => n.Value)
                        .HasColumnName(nameof(Occasion.Name))
                        .HasMaxLength(100)
                        .IsRequired();
                }
        );

        builder.Property(c => c.Description).HasMaxLength(500);
    }
}