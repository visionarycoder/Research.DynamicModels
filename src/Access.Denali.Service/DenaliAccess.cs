using Access.Denali.Contract;
using Access.Denali.Contract.Models;
using Data.Alaska;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Access.Denali.Service
{
    
    public class DenaliAccess(ILogger<DenaliAccess> logger, AlaskaContext ctx) : IDenaliAccess
    {
        public async Task<ICollection<Widget>> GetWidgetsAsync()
        {
            var widgets = new List<Widget>();
            var query = ctx.Widgets
                .Include(w => w.Extensions)
                .Include(w => w.ModifierExtensions)
                .Include(w => w.Properties)
                .AsQueryable();

            var collection = await query.ToListAsync();
            foreach (var entry in collection)
            {
                var widget = new Widget
                {
                    Id = entry.Id,
                    Name = entry.Name,
                };
                var color = entry.Properties.FirstOrDefault(p => p.Name == "Color");
                if (color != null)
                {
                    widget.Color = color.Value;
                }
                var size = entry.Properties.FirstOrDefault(p => p.Name == "Size");
                if (size != null)
                {
                    widget.Size = size.Value;
                }

                var description = entry.Properties.FirstOrDefault(p => p.Name == "Description");
                if (description != null)
                {
                    widget.Description = description.Value;
                }
                widgets.Add(widget);
            }

            return widgets;
        }

        public async Task<Widget> AddAsync(Widget widget)
        {
            return widget;
        }
    }

}
