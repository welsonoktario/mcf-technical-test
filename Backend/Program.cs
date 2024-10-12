using Backend.Data;
using Backend.Data.Interfaces;
using Backend.Data.Repositories;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

// Add authentication services
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/auth/login";
    });

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", corsPolicyBuilder =>
    {
        var frontendUrl = builder.Environment.IsProduction()
            ? builder.Configuration.GetValue("FrontendUrl", "https://localhost:7273")
            : "*";

        corsPolicyBuilder.WithOrigins(frontendUrl)
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Add repository services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBpkbRepository, BpkbRepository>();
builder.Services.AddScoped<IBaseRepository<StorageLocationModel>, StorageLocationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
