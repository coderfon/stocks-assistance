using StocksAssistance.Common.Helpers;
using StocksAssistance.EF.Context;
using Microsoft.EntityFrameworkCore;
using StocksAssistance.Business.Services;
using StocksAssistance.EF.Repositories;
using StocksAssistance.EF.Models;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("StocksAssistanceApiDatabase");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add EF Core stuff
builder.Services.AddDbContext<StocksAssistanceDbContext>(opts => opts.UseLazyLoadingProxies()
                                                                     .UseSqlServer(connectionString));
builder.Services.AddScoped<CompanyRepository, CompanyRepository>();
builder.Services.AddScoped<CompanyTagRepository, CompanyTagRepository>();
builder.Services.AddScoped<CompanyService, CompanyService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Ensure database is created
using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<StocksAssistanceDbContext>();
    context?.Database.EnsureCreated();
}

app.Run();
