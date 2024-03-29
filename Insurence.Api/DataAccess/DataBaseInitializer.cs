using Insurence.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Insurence.Api.DataAccess;

public class DataBaseInitializer
{
    private readonly AppDbContext _context;

    public DataBaseInitializer(AppDbContext context)
    {
        _context = context;
    }

    public async Task InitalizeFacade()
    {
       await _context.Database.EnsureCreatedAsync();
       await AddCoverage();
    }

    public async Task AddCoverage()
    {
        var hasCoverage =await _context.Coverages.AnyAsync();
        if (hasCoverage)
        {
            return;
        }

        var coverages = new List<Coverage>()
        { 
            new Coverage() {Type = CoverageType.Surgery,MinInvestment =5000 ,MaxInvestment =500_000_000,PaymentFee = 0.0052},
            new Coverage(){Type = CoverageType.Dental,MinInvestment =4000 ,MaxInvestment =400_000_000,PaymentFee = 0.0042},
            new Coverage(){Type = CoverageType.Hospitalization,MinInvestment =2000 ,MaxInvestment =200_000_000,PaymentFee = 0.005},
        };
        
        _context.Coverages.AddRange(coverages);
        await _context.SaveChangesAsync();
    }
}