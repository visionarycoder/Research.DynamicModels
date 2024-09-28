using Data.Alaska.Models.Base;

namespace Data.Alaska.Models;

public class Product : Entity
{
    public virtual ICollection<Extension> Extensions { get; set; } = new List<Extension>();
}