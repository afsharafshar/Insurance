namespace Insurence.Api.Entities;

public class CoverageRequest:Entity
{
    public CoverageType Type { get; set; }
    public double Investment { get; set; }
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
}