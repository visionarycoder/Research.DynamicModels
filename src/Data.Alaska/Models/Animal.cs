using System.ComponentModel.DataAnnotations;
using Ifx.Data.Models;

namespace Data.Alaska.Models;

public class Animal : Entity
{

    [Required, MaxLength(64)] public string Name { get; set; } = string.Empty;
    [Required, MaxLength(64)] public string AnimalType { get; set; } = string.Empty;

    public virtual ICollection<Extension> Extensions { get; set; } = new List<Extension>();

}