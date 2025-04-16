using Microsoft.EntityFrameworkCore;
using ShipmentTracker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=ShipmentTrackerDB.db";
builder.Services.AddDbContext<ShipmentTrackerDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddLogging();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ShipmentTrackerDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();