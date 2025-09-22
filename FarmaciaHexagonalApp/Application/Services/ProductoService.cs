using FarmaciaHexagonalApp.Domain.Entities;
using FarmaciaHexagonalApp.Domain.Ports;

namespace FarmaciaHexagonalApp.Application.Services;

public class ProductoService
{
    private readonly IRepositorioProducto _repositorio;

    public ProductoService(IRepositorioProducto repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<IEnumerable<ProductoFarmaceutico>> GetAllProductosAsync()
    {
        return await _repositorio.GetAllAsync();
    }

    public async Task<ProductoFarmaceutico> GetProductoByIdAsync(int id)
    {
        return await _repositorio.GetByIdAsync(id);
    }

    public async Task CreateProductoAsync(ProductoFarmaceutico producto)
    {
        // Aquí puedes agregar lógica de negocio, e.g., validar fecha de caducidad
        if (producto.FechaCaducidad < DateTime.Now)
            throw new ArgumentException("La fecha de caducidad debe ser futura.");

        await _repositorio.CreateAsync(producto);
    }

    public async Task UpdateProductoAsync(ProductoFarmaceutico producto)
    {
        await _repositorio.UpdateAsync(producto);
    }

    public async Task DeleteProductoAsync(int id)
    {
        await _repositorio.DeleteAsync(id);
    }
}