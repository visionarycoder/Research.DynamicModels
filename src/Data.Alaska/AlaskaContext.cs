using Data.Alaska.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Alaska
{
    public class AlaskaContext(ILogger<AlaskaContext> logger, DbContextOptions<AlaskaContext> options) : DbContext(options)
    {
        
        public DbSet<Extension> Extensions { get; set; } = default!;
        public DbSet<ModifierExtension> ModifierExtensions { get; set; } = default!;
        public DbSet<Property> Properties { get; set; } = default!;
        public DbSet<Widget> Widgets { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            logger.LogInformation("Model creating configurations applied.");

            // Configure relationships and constraints

            // Extension configuration
            modelBuilder.Entity<Extension>()
                .HasMany(e => e.Widgets)
                .WithMany(w => w.Extensions)
                .UsingEntity(j => j.ToTable("WidgetExtensions"));

            // ModifierExtension configuration
            modelBuilder.Entity<ModifierExtension>()
                .HasMany(me => me.Widgets)
                .WithMany(w => w.ModifierExtensions)
                .UsingEntity(j => j.ToTable("WidgetModifierExtensions"));

            // Property configuration
            modelBuilder.Entity<Property>()
                .HasMany(p => p.Widgets)
                .WithMany(w => w.Properties)
                .UsingEntity(j => j.ToTable("WidgetProperties"));

            // Widget configuration
            modelBuilder.Entity<Widget>()
                .HasMany(w => w.Extensions)
                .WithMany(e => e.Widgets)
                .UsingEntity(j => j.ToTable("WidgetExtensions"));

            modelBuilder.Entity<Widget>()
                .HasMany(w => w.ModifierExtensions)
                .WithMany(me => me.Widgets)
                .UsingEntity(j => j.ToTable("WidgetModifierExtensions"));

            modelBuilder.Entity<Widget>()
                .HasMany(w => w.Properties)
                .WithMany(p => p.Widgets)
                .UsingEntity(j => j.ToTable("WidgetProperties"));
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

}