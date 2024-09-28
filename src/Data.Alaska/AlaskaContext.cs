using Data.Alaska.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Alaska
{
    public class AlaskaContext(ILogger<AlaskaContext> logger, DbContextOptions<AlaskaContext> options)
                        : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Extension> Extensions { get; set; } = default!;
        public DbSet<Inventory> Inventories { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Supplier> Suppliers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Extensions)
                .WithOne()
                .HasForeignKey(e => e.TargetId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Extensions)
                .WithOne()
                .HasForeignKey(e => e.TargetId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Extension>()
                .HasDiscriminator<string>("TargetType");

            modelBuilder.Entity<Inventory>()
                .HasMany(i => i.Extensions)
                .WithOne()
                .HasForeignKey(e => e.TargetId)
                .HasPrincipalKey(i => i.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Extensions)
                .WithOne()
                .HasForeignKey(e => e.TargetId)
                .HasPrincipalKey(o => o.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasMany(oi => oi.Extensions)
                .WithOne()
                .HasForeignKey(e => e.TargetId)
                .HasPrincipalKey(oi => oi.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Extensions)
                .WithOne()
                .HasForeignKey(e => e.TargetId)
                .HasPrincipalKey(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Extensions)
                .WithOne()
                .HasForeignKey(e => e.TargetId)
                .HasPrincipalKey(s => s.Id)
                .OnDelete(DeleteBehavior.Cascade);

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
}

