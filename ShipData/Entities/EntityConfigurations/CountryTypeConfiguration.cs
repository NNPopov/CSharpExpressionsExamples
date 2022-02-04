using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShipData.Entities.EntityConfigurations;

public class CountryTypeConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("producers");

        builder
            .Property(c => c.Id)
            .HasColumnName("id")
            .IsRequired();

        builder
            .HasKey(c => c.Id)
            .HasName("pk_producers");

        builder
            .Property(c => c.Name)
            .HasColumnName("name")
            .IsRequired();

        builder
            .HasIndex(u => u.Name)
            .HasDatabaseName("ix_producers_name")
            .IsUnique();
    }
}