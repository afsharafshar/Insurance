using Insurence.Api.DataAccess;
using Insurence.Api.Infrasturcture;
using Insurence.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Insurence.db"));

builder.Services.AddScoped<RequestService>();
var app = builder.Build();

async Task InitializeDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;
        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        await new DataBaseInitializer(dbContext).InitalizeFacade();
    }
}

await InitializeDatabase();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandling();
app.MapControllers();

app.Run();