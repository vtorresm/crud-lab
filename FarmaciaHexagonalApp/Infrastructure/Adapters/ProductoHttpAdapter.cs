using System.Net.Http.Json;
using FarmaciaHexagonalApp.Domain.Entities;
using FarmaciaHexagonalApp.Domain.Ports;

namespace FarmaciaHexagonalApp.Infrastructure.Adapters;

public class ProductoHttpAdapter : IRepositorioProducto
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://localhost:7145/api/Productos"; // Ajusta al URL de tu API

    public ProductoHttpAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductoFarmaceutico>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ProductoFarmaceutico>>(BaseUrl) ?? new List<ProductoFarmaceutico>();
    }

    public async Task<ProductoFarmaceutico> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProductoFarmaceutico>($"{BaseUrl}/{id}") ?? throw new Exception("Producto no encontrado");
    }

    public async Task CreateAsync(ProductoFarmaceutico producto)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, producto);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateAsync(ProductoFarmaceutico producto)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{producto.Id}", producto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        response.EnsureSuccessStatusCode();
    }
}