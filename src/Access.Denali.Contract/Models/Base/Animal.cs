using Ifx.Models;

namespace Access.Denali.Contract.Models.Base;

public abstract class Animal : Dto
{

    public string Name { get; set; } = string.Empty;
    public Gender Gender { get; set; } = default!;
    public string Habitat { get; set; } = string.Empty;

}