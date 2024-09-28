namespace Data.Alaska.Models.Base;

public abstract class Entity
{
    public int Id { get; set; }
    public Guid Uuid { get; set; } = Guid.NewGuid();

}