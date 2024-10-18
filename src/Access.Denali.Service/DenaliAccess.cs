using Access.Denali.Contract;
using Access.Denali.Contract.Models;

using Data.Alaska;
using Data.Alaska.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Access.Denali.Service;

public class DenaliAccess(ILogger<DenaliAccess> logger, AlaskaContext ctx) : IDenaliAccess
{

    public async Task<ICollection<Bear>> GetBearsAsync(CancellationToken cancellationToken)
    {
        var query = CreateQuery<Bear>().ToBears();
        return await query.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Bear?> GetBearByIdAsync(int id, CancellationToken cancellationToken)
    {
        var query = CreateQuery<Bear>(id).ToBears();
        var collection = await query.ToListAsync(cancellationToken: cancellationToken);
        return collection.FirstOrDefault();
    }

    public async Task<Bear?> CreateBearAsync(Bear bear, CancellationToken cancellationToken)
    {

        var animal = new Animal
        {
            Name = bear.Name,
            AnimalType = nameof(Bear),
            Extensions = CreateExtensions(typeof(Bear), typeof(Animal), bear)
        };

        var entityEntry = await ctx.Animals.AddAsync(animal, cancellationToken);
        bear.Id = entityEntry.Entity.Id;
        return bear;
    }

    public async Task<Bear?> UpdateBearAsync(Bear bear, CancellationToken cancellationToken)
    {

        var entity = await ctx.Animals
            .Include(e => e.Extensions)
            .FirstOrDefaultAsync(e => e.Id == bear.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        ctx.Entry(entity).CurrentValues.SetValues(new Animal
        {
            Id = bear.Id,
            Name = bear.Name,
            AnimalType = nameof(Bear)
        });

        var extensions = entity.Extensions.ToList();
        foreach (var property in typeof(Bear).GetProperties().Where(pi => typeof(Animal).GetProperty(pi.Name) != null))
        {

            var extension = extensions.FirstOrDefault(e => e.Name == property.Name);
            if (extension == null)
            {
                extension = new Extension
                {
                    Name = property.Name,
                    Value = property.GetValue(bear)?.ToString() ?? string.Empty,
                    ValueType = property.PropertyType.Name
                };
                entity.Extensions.Add(extension);
            }
            else
            {
                extension.Value = property.GetValue(bear)?.ToString() ?? string.Empty;
                extension.ValueType = property.PropertyType.Name;
            }
        }

        await ctx.SaveChangesAsync(cancellationToken);
        return bear;

    }

    private List<Extension> CreateExtensions(Type sourceType, Type targetType, object entity)
    {

        return sourceType
            .GetProperties()
            .Where(pi => targetType.GetProperty(pi.Name) != null)
            .Select(pi => new Extension
            {
                Name = pi.Name,
                Value = pi.GetValue(entity)?.ToString() ?? string.Empty,
                ValueType = pi.PropertyType.Name
            }).ToList();

    }

    public async Task<bool> DeleteBearAsync(int id, CancellationToken cancellationToken)
    {

        var entity = await ctx.Animals.FindAsync(new object[] { id }, cancellationToken);
        if (entity == null)
        {
            return false;
        }
        ctx.Animals.Remove(entity);
        var count = await ctx.SaveChangesAsync(cancellationToken);
        return count > 0;

    }

    private IQueryable<Animal> CreateQuery<T>() where T : class
    {

        var query = ctx.Set<Animal>().Where(e => e.AnimalType == typeof(T).Name);
        return query;

    }

    private IQueryable<Animal> CreateQuery<T>(int id) where T : class
    {
        var query = CreateQuery<T>().Where(e => e.Id == id);
        return query;
    }

}