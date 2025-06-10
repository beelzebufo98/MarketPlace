using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrdersService.Application.Interfaces;
using OrdersService.Application.Services;
using OrdersService.Infrastructure.Data;
using OrdersService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("Default");

Console.WriteLine(connectionString);

builder.Services.AddDbContext<OrdersDbContext>(opts =>
    opts.UseNpgsql(connectionString, o =>
    {
      o.MigrationsAssembly(typeof(OrdersDbContext).Assembly.FullName);
      o.MigrationsHistoryTable("__EFMigrationsHistory", "OrdersService");
    })
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
  .AddTransient<IOrderService, OrderService>()
  .AddTransient<IOrderResultRepository, OrderResultRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders Service API", Version = "v1" });
  c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payments Service API v1");
  });
}

using (var scope = app.Services.CreateScope())
{
  var db = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
  await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
