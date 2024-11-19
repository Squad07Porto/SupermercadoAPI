using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using Supermercado.API.Config.Injection;
using Supermercado.API.Config.Mappings;
using Supermercado.API.Infrastructure.Database;
using System.Text;
using Supermercado.API.Services;
using Microsoft.OpenApi.Models;
using Supermercado.API.Services.Interfaces;
using Supermercado.API.Config.Hubs;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddTransient(sp =>
{
    var dbConnectionFactory = sp.GetRequiredService<IDbConnectionFactory>();
    return dbConnectionFactory.CreateConnection();
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!))
    };
});

builder.Services.AddSingleton<JwtService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureServices((context, services) =>
{
    ConfigureRepository.ConfigureDependenciesRepository(services);
    ConfigureService.ConfigureDependenciesService(services);
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new() { Title = "ProdAgri", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Digite o token JWT recebido",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();

MapperConfiguration mapperConfig = new(mc =>
{
    mc.AddProfile(new MappingProfile());
});

builder.Services.AddScoped(sp => mapperConfig.CreateMapper());

builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
        .AllowCredentials());
});

var app = builder.Build();

var rabbitMQService = app.Services.GetRequiredService<IRabbitMQService>();
rabbitMQService.StartListening();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy"); 

app.MapControllers();

app.MapHub<SensorHub>("/sensorhub");

app.Run();
