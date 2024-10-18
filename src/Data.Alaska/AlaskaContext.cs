using Data.Alaska.Configuration;
using Data.Alaska.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Alaska;

public class AlaskaContext(ILogger<AlaskaContext> logger, DbContextOptions<AlaskaContext> options) : DbContext(options)
{

    public DbSet<Animal> Animals { get; set; } = default!;
    public DbSet<Extension> Extensions { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.ApplyConfiguration(new AnimalConfiguration());
        modelBuilder.ApplyConfiguration(new ExtensionConfiguration());

        modelBuilder.Entity<Animal>()
            .HasMany(a => a.Extensions)
            .WithMany(e => e.Animals)
            .UsingEntity<Dictionary<string, object>>(
                "AnimalExtension",
                j => j.HasOne<Extension>().WithMany().HasForeignKey("ExtensionId"),
                j => j.HasOne<Animal>().WithMany().HasForeignKey("AnimalId")
            );

        logger.LogInformation("Model creating configurations applied.");

    }

    public override int SaveChanges()
    {

        logger.LogInformation("Saving changes to the database.");
        return base.SaveChanges();

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        logger.LogInformation("Saving changes to the database asynchronously.");
        return await base.SaveChangesAsync(cancellationToken);

    }

}