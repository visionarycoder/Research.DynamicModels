using Data.Alaska.Models.Base;

namespace Data.Alaska.Models
{
    
    public class Widget : DomainResource
    {
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

    }

}
