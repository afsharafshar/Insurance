using Insurence.Api.DataAccess;
using Insurence.Api.Entities;
using Insurence.Api.Infrasturcture;
using Microsoft.EntityFrameworkCore;

namespace Insurence.Api.Services;

public class RequestService
{
    private readonly AppDbContext _context;

    public RequestService(AppDbContext context)
    {
        _context = context;
    }


    public async Task InsertRequest(InsertRequestDto requestDto,CancellationToken stoppingToken)
    {
        var coverages = await _context.Coverages.ToListAsync(stoppingToken);

        var customer = new Customer()
        {
            Email = requestDto.Email!,
            Name = requestDto.Name!,
            Requests = new List<CoverageRequest>()
        };

        foreach (var request in requestDto.RequestDtos)
        {
            var coverage = coverages.First(x => x.Type == request.Type);

            if (request.Investment>= coverage.MinInvestment && request.Investment<=coverage.MaxInvestment)
            {
                var requestCoverage = new CoverageRequest()
                {
                    Type = request.Type,
                    Investment = request.Investment,
                };
                customer.Requests.Add(requestCoverage);
                
            }
            else
            {
                throw new LogicException("Investment not in range");
            }
        }

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(stoppingToken);
    }


    public async Task<InsuranceCoverageForUserDto> ShowInsurance(string email, CancellationToken stoppingToken)
    {
        var customer = await _context.Customers
            .Include(x => x.Requests)
            .FirstOrDefaultAsync(x => x.Email == email,stoppingToken);

        if (customer is null)
        {
            throw new LogicException("customer not found");
        }
        
        var coverages = await _context.Coverages.ToListAsync(stoppingToken);

        var showCoverage = new InsuranceCoverageForUserDto();

        foreach (var request in customer.Requests)
        {
            var coverage = coverages.First(x => x.Type == request.Type);
            var fee = request.Investment * coverage.PaymentFee;
            showCoverage.All += fee;
        }

        return showCoverage;
    }
}