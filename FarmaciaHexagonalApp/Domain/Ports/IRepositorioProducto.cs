using FarmaciaHexagonalApp.Domain.Entities;

namespace FarmaciaHexagonalApp.Domain.Ports;

public interface IRepositorioProducto
{
    Task<IEnumerable<ProductoFarmaceutico>> GetAllAsync();
    Task<ProductoFarmaceutico> GetByIdAsync(int id);
    Task CreateAsync(ProductoFarmaceutico producto);
    Task UpdateAsync(ProductoFarmaceutico producto);
    Task DeleteAsync(int id);
}