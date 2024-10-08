using Data.Alaska.Models.Base;

namespace Data.Alaska.Models;

public class ModifierExtension : Element
{

    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string ValueType { get; set; } = string.Empty;

    public virtual ICollection<Widget> Widgets { get; set; } = new List<Widget>();

}