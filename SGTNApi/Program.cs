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
using SGTNApi.Converters;


var builder = WebApplication.CreateBuilder(args);

// Acceso a la configuración
var configuration = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", p =>
        p.WithOrigins("http://localhost:4200", "http://localhost:4301")
         .AllowAnyHeader()
         .AllowAnyMethod()
    // .AllowCredentials() // sólo si usas cookies / credenciales; no combinar con AllowAnyOrigin

    );

    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new NullableTimeSpanConverter());
        opts.JsonSerializerOptions.Converters.Add(new NullableDateTimeConverter());
    });

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

// Register clients repository and service
builder.Services.AddScoped<IClientRepository, ClientsRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

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

app.UseCors("AllowAngularDev");

var cs = configuration.GetConnectionString("SqlServerConnectionString");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    // temporal en dev: acepta todo
    app.UseCors("AllowAll");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else {
    app.UseCors("AllowAngularDev");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
