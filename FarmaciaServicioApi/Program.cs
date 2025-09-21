// Add these using statements at the top of Program.cs
using Microsoft.EntityFrameworkCore;
using FarmaciaServicioApi.Data;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql; // If using MySQL
// using Microsoft.EntityFrameworkCore.SqlServer; // If using SQL Server

// Your existing code with fixes:
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Farmacia API", Version = "v1" });
});

// Fix database context registration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FarmaciaDbContext>(options =>
{
    // For MySQL
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

    // For SQL Server
    // options.UseSqlServer(connectionString);
});

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

app.Run();