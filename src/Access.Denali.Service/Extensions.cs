using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Access.Denali.Contract.Models;

using Data.Alaska.Models;

namespace Access.Denali.Service
{
    internal static class Extensions
    {
        public static IQueryable<Bear> ToBears(this IQueryable<Animal> query)
        {
            var bears = query.Select(e => new Bear
            {
                Id = e.Id,
                Name = e.Name,
                Gender = GetPropertyValue<Gender>(e.Extensions, nameof(Bear.Gender)),
                Habitat = GetPropertyValue<string>(e.Extensions, nameof(Bear.Habitat)),
                HibernationMonths = GetPropertyValue<int>(e.Extensions, nameof(Bear.HibernationMonths))
            }).Cast<Bear>().AsQueryable();
            return bears;
        }

        private static T GetPropertyValue<T>(ICollection<Extension> extensions, string propertyName)
        {
            var extension = extensions.FirstOrDefault(x => x.Name == propertyName);
            if (extension != null && TryParse<T>(extension.Value, out var value))
            {
                return value;
            }
            return default!;
        }

        private static bool TryParse<T>(string value, out T result)
        {
            var type = typeof(T);
            if (type.IsEnum)
            {
                if (Enum.TryParse(type, value, out var enumResult))
                {
                    result = (T)enumResult;
                    return true;
                }
            }
            else
            {
                try
                {
                    result = (T)Convert.ChangeType(value, typeof(T));
                    return true;
                }
                catch
                {
                    // Ignore exception and fall through to return false
                }
            }
            result = default!;
            return false;
        }

    }
}
