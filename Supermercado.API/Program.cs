using System.Data;
using AutoMapper;
using Supermercado.API.Config.Injection;
using Supermercado.API.Config.Mappings;
using Supermercado.API.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var dbConnectionFactory = sp.GetRequiredService<IDbConnectionFactory>();
    return dbConnectionFactory.CreateConnection();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureServices((context, services) =>
{
    ConfigureRepository.ConfigureDependenciesRepository(services);
    ConfigureService.ConfigureDependenciesService(services);
});

builder.Services.AddControllers();

MapperConfiguration mapperConfig = new (mc =>
{
    mc.AddProfile(new MappingProfile());
});

builder.Services.AddScoped(sp => mapperConfig.CreateMapper());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
