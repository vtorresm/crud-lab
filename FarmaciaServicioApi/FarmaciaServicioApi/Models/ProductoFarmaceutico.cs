namespace FarmaciaServicioApi.Models;

public class ProductoFarmaceutico
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string Formula { get; set; }
    public DateTime FechaCaducidad { get; set; }
    public decimal Precio { get; set; }
}