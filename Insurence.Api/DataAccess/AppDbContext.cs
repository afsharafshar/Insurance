using Insurence.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Insurence.Api.DataAccess;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }


    public DbSet<Customer> Customers { get; set; }
    public DbSet<CoverageRequest> CoverageRequests { get; set; }
    public DbSet<Coverage> Coverages { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Customer>().Property(x => x.RowVersion).IsRowVersion();
        builder.Entity<CoverageRequest>().Property(x => x.RowVersion).IsRowVersion();
        builder.Entity<Coverage>().Property(x => x.RowVersion).IsRowVersion();
        
        base.OnModelCreating(builder);
    }
}