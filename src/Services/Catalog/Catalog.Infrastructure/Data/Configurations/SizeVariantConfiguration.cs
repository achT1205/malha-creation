//namespace Catalog.Infrastructure.Data.Configurations;

//public class SizeVariantConfiguration : IEntityTypeConfiguration<SizeVariant>
//{
//    public void Configure(EntityTypeBuilder<SizeVariant> builder)
//    {
//        builder.HasKey(sv => sv.Id);

//                builder.Property(sv => sv.Id)
//                    .ValueGeneratedNever()
//                    .HasConversion(
//                        id => id.Value,
//                        dbId => SizeVariantId.Of(dbId));

//        builder.Property(sv => sv.Size).IsRequired();
//        builder.Property(sv => sv.Price).IsRequired();
//        builder.Property(sv => sv.Quantity).IsRequired();

//        builder.ToTable("SizeVariants");
//    }
//}
