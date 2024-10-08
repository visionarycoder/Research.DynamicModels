using Data.Alaska.Models.Base;

namespace Data.Alaska.Models;

public class Property : Element
{

    public string Name { get; set; } = default!;
    public string Value { get; set; } = default!;
    public string ValueType { get; set; } = default!;

    public virtual ICollection<Widget> Widgets { get; set; } = new List<Widget>();

}