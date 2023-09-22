namespace Insurence.Api.Entities;

public class Customer:Entity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    public List<CoverageRequest> Requests { get; set; }
}