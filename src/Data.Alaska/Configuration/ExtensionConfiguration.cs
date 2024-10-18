using Data.Alaska.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Alaska.Configuration;

public class ExtensionConfiguration : IEntityTypeConfiguration<Extension>
{

    public void Configure(EntityTypeBuilder<Extension> builder)
    {

        // Specify the table name
        builder.ToTable(nameof(AlaskaContext.Extensions));
        
        // Configure inherited properties from Entity
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        // Configure properties specific to Extension
        builder.Property(e => e.Name).IsRequired().HasMaxLength(64);

        builder.Property(e => e.Value).IsRequired().HasMaxLength(64);

        builder.Property(e => e.ValueType).IsRequired().HasMaxLength(64);
    
    }

}