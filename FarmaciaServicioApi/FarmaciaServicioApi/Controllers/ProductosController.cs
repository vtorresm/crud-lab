using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmaciaServicioApi.Data;
using FarmaciaServicioApi.Models;

namespace FarmaciaServicioApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly FarmaciaDbContext _context;

    public ProductosController(FarmaciaDbContext context)
    {
        _context = context;
    }

    // GET: api/Productos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductoFarmaceutico>>> GetProductos()
    {
        return await _context.ProductosFarmaceuticos.ToListAsync();
    }

    // GET: api/Productos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductoFarmaceutico>> GetProducto(int id)
    {
        var producto = await _context.ProductosFarmaceuticos.FindAsync(id);
        if (producto == null) return NotFound();
        return producto;
    }

    // POST: api/Productos
    [HttpPost]
    public async Task<ActionResult<ProductoFarmaceutico>> PostProducto(ProductoFarmaceutico producto)
    {
        _context.ProductosFarmaceuticos.Add(producto);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
    }

    // PUT: api/Productos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProducto(int id, ProductoFarmaceutico producto)
    {
        if (id != producto.Id) return BadRequest();
        _context.Entry(producto).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductoExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    // DELETE: api/Productos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto(int id)
    {
        var producto = await _context.ProductosFarmaceuticos.FindAsync(id);
        if (producto == null) return NotFound();
        _context.ProductosFarmaceuticos.Remove(producto);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ProductoExists(int id)
    {
        return _context.ProductosFarmaceuticos.Any(e => e.Id == id);
    }
}