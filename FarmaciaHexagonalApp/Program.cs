using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FarmaciaHexagonalApp.Application.Services;
using FarmaciaHexagonalApp.Domain.Ports;
using FarmaciaHexagonalApp.Infrastructure.Adapters;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHttpClient();
        services.AddTransient<IRepositorioProducto, ProductoHttpAdapter>();
        services.AddTransient<ProductoService>();
    })
    .Build();

// Ejemplo de uso en consola
var productoService = host.Services.GetRequiredService<ProductoService>();

try
{
    // Crear un producto
    var nuevoProducto = new FarmaciaHexagonalApp.Domain.Entities.ProductoFarmaceutico
    {
        Nombre = "Aspirina",
        Formula = "C9H8O4",
        FechaCaducidad = DateTime.Now.AddYears(2),
        Precio = 10.5m
    };
    await productoService.CreateProductoAsync(nuevoProducto);
    Console.WriteLine("Producto creado.");

    // Obtener todos los productos
    Console.WriteLine("\nLista de productos:");
    var productos = await productoService.GetAllProductosAsync();
    foreach (var p in productos)
    {
        Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Fórmula: {p.Formula}, Precio: {p.Precio}, Caducidad: {p.FechaCaducidad.ToShortDateString()}");
    }

    // Actualizar un producto (suponiendo que el ID 1 existe, ajusta según tu DB)
    if (productos.Any())
    {
        var productoAActualizar = productos.First(); // Tomamos el primer producto para actualizar
        productoAActualizar.Nombre = "Aspirina Actualizada";
        productoAActualizar.Precio = 12.99m;
        await productoService.UpdateProductoAsync(productoAActualizar);
        Console.WriteLine($"\nProducto actualizado: ID {productoAActualizar.Id}, Nuevo Nombre: {productoAActualizar.Nombre}");
    }

    // Obtener el producto actualizado
    if (productos.Any())
    {
        var productoActualizado = await productoService.GetProductoByIdAsync(productos.First().Id);
        Console.WriteLine($"\nProducto recuperado tras actualización: ID: {productoActualizado.Id}, Nombre: {productoActualizado.Nombre}");
    }

    // Eliminar un producto (suponiendo que el ID 1 existe, ajusta según tu DB)
    if (productos.Any())
    {
        var idAEliminar = productos.First().Id;
        await productoService.DeleteProductoAsync(idAEliminar);
        Console.WriteLine($"\nProducto eliminado: ID {idAEliminar}");
    }

    // Verificar lista tras eliminación
    Console.WriteLine("\nLista de productos tras eliminación:");
    productos = await productoService.GetAllProductosAsync();
    foreach (var p in productos)
    {
        Console.WriteLine($"ID: {p.Id}, Nombre: {p.Nombre}, Fórmula: {p.Formula}, Precio: {p.Precio}, Caducidad: {p.FechaCaducidad.ToShortDateString()}");
    }

} catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }

Console.WriteLine("\nPresiona cualquier tecla para salir..."); Console.ReadKey();