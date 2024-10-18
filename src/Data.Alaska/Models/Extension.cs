using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ifx.Data.Models;

namespace Data.Alaska.Models;

public class Extension : Entity
{

    [Required, MaxLength(64)] public string Name { get; set; } = default!;
    [Required, MaxLength(64)] public string Value { get; set; } = default!;
    [Required, MaxLength(64)] public string ValueType { get; set; } = default!;

    public ICollection<Animal> Animals { get; set; } = new List<Animal>();

}