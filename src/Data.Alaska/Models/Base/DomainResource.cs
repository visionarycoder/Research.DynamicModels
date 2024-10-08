namespace Data.Alaska.Models.Base;

public abstract class DomainResource : Element
{

    public virtual ICollection<ModifierExtension> ModifierExtensions { get; set; } = new List<ModifierExtension>();

}