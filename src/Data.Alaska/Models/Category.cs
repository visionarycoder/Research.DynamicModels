using System.Data;
using Data.Alaska.Models.Base;

namespace Data.Alaska.Models;

public class List : Entity
{
    public virtual ICollection<Extension> Extensions { get; set; } = new List<Extension>();

}

public class ListEntry : Entity
{

    public int ReferenceId { get; set; }
    public virtual Reference Target { get; set; } = default!;

}

public class Reference : Entity
{
    public int ItemId { get; set; }
    public string ItemType { get; set; } = default!;
}

public class CatalogEntry : Entity
{

}

public class ProductDefinition : Entity
{

    public ICollection<Property> Properties { get; set; } = new List<Property>();
    public Uri OnlineInformation { get; set; } = default!;

}

public class ServiceDefinition : Entity
{

}

public class WarrantyDefinition : Entity
{

}

public class Property : Entity
{
    public string Name { get; set; } = default!;
    public string Value { get; set; } = default!;
    public string ValueType { get; set; } = default!;
}

public class Part
{
 
    public int ReferenceId { get; set; }
    public int Count { get; set; }

}

public class Image : Entity
{

    public string AltText { get; set; }
    public string Url { get; set; }
    public int ReferenceId { get; set; }

}

public class Extension : Element
{
    public string Url { get; set; }
    public string Value { get; set; }
    public string ValueType { get; set; }

}

public abstract class Element : Entity, IElement
{
    public virtual ICollection<Extension> Extensions { get; set; } = new List<Extension>();

}
public interface IElement
{
    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public ICollection<Extension> Extensions { get; set; }
}

public class Address : Element
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    
}

public class ContactPoint : Element
{
    public int ListId { get; set; }
    public int ListEntryId { get; set; }
    public string Use { get; set; } = string.Empty;
    public string Rank { get; set; } = string.Empty;
    public Period Period { get; set; } = default!;
}

public class Period : Element
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}


public class Quantity : Element
{
    public decimal Value { get; set; }
    public string Comparator { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public Guid Rule { get; set; } = default!;
}

public class Distance : Quantity
{

}