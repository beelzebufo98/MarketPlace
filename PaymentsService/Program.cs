using Microsoft.EntityFrameworkCore;
using PaymentsService.Application.Interfaces;
using PaymentsService.Infrastructure.Data;
using PaymentsService.Application.Services;
using PaymentsService.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("Default");

Console.WriteLine(connectionString);

builder.Services.AddDbContext<PaymentsDbContext>(opts =>
    opts.UseNpgsql(connectionString, o =>
    {
      o.MigrationsAssembly(typeof(PaymentsDbContext).Assembly.FullName);
      o.MigrationsHistoryTable("__EFMigrationsHistory", "PaymentsService");
    })
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()); 
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
  .AddTransient<IPaymentsService, PaymentService>()
  .AddTransient<IPaymentResultRepository, PaymentResultRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payments Service API", Version = "v1" });
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
  var db = scope.ServiceProvider.GetRequiredService<PaymentsDbContext>();
  await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
