using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShipData.Entities.EntityConfigurations;

public class ShipTypeConfiguration : IEntityTypeConfiguration<Ship>
{
    public void Configure(EntityTypeBuilder<Ship> builder)
    {
        builder.ToTable("ships");

        builder
            .Property(c => c.Id)
            .HasColumnName("id")
            .IsRequired();

        builder
            .HasKey(c => c.Id)
            .HasName("pk_ships");

        builder
            .Property(c => c.Name)
            .HasColumnName("name")
            .IsRequired();

        builder
            .Property(c => c.UniqueId)
            .HasColumnName("unique_id")
            .IsRequired();

        builder
            .Property(c => c.Description)
            .HasColumnName("description");


        builder
            .Property(c => c.LaunchedDate)
            .HasColumnName("launched_date");

        builder
            .Property(c => c.CountryId)
            .HasColumnName("country_id");

        builder
            .Property(c => c.NormalDisplacement)
            .HasColumnName("normal_displacement");

        builder
            .Property(c => c.NumberGuns)
            .HasColumnName("number_guns");

        builder
            .Property(c => c.Bore)
            .HasColumnName("bore");

        builder
            .HasIndex(u => u.Name)
            .HasDatabaseName("ix_ship_name")
            .IsUnique();

        builder
            .HasIndex(u => u.UniqueId)
            .HasDatabaseName("ix_unique_id")
            .IsUnique();

        builder.HasOne(t => t.Country)
            .WithMany(t => t.Ships)
            .HasForeignKey(t => t.CountryId)
            ;
    }
}