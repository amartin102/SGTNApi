using Application.Interface;
using Application.Mappings;
using Application.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Context;
using Repository.Interface;
using Repository.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Acceso a la configuración
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SqlDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("SqlServerConnectionString"));
});


// Dependency Injection
builder.Services.AddScoped<IMasterParameterRepository, MasterParameterRepository>();
builder.Services.AddScoped<IMasterParameterService, MasterParameterService>();
builder.Services.AddScoped<IParameterValueRepository, ParameterValueRepository>();
builder.Services.AddScoped<IParameterValueService, ParameterValueService>();

// AutoMapper con configuración explícita
builder.Services.AddAutoMapper((serviceProvider, cfg) =>
{
    cfg.ConstructServicesUsing(serviceProvider.GetService);
    cfg.AddProfile<MasterParameterProfile>();
    cfg.AddProfile<ParameterValueProfile>();
}, typeof(Program).Assembly);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

var cs = configuration.GetConnectionString("SqlServerConnectionString");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
