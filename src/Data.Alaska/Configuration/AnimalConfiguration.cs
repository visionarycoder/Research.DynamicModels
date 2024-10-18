using Data.Alaska.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Alaska.Configuration;

public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{

    public void Configure(EntityTypeBuilder<Animal> entity)
    {

        entity.ToTable(nameof(AlaskaContext.Animals));

        entity.Property(e => e.Id).ValueGeneratedOnAdd();

        entity.Property(e => e.Name).IsRequired().HasMaxLength(64);

        entity.Property(e => e.AnimalType).IsRequired();

        entity.HasMany(a => a.Extensions).WithOne().OnDelete(DeleteBehavior.Cascade);

    }

}