namespace Insurence.Api.Entities;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }=DateTime.Now;
    public DateTime? Updated { get; set; }
    public byte[]? RowVersion { get; set; }
}