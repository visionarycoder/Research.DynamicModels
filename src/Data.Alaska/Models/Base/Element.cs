using Ifx.Data.Models;

namespace Data.Alaska.Models.Base;

public abstract class Element : Entity
{

    public virtual ICollection<Extension> Extensions { get; set; } = new List<Extension>();

}