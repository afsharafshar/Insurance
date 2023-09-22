namespace Insurence.Api.Entities;

public class Coverage:Entity
{
    public CoverageType Type { get; set; }
    public double MinInvestment { get; set; }
    public double MaxInvestment { get; set; }
}