using Microsoft.EntityFrameworkCore;
using FarmaciaServicioApi.Models;

namespace FarmaciaServicioApi.Data;

public class FarmaciaDbContext : DbContext
{
    public FarmaciaDbContext(DbContextOptions<FarmaciaDbContext> options) : base(options) { }

    public DbSet<ProductoFarmaceutico> ProductosFarmaceuticos { get; set; }
}