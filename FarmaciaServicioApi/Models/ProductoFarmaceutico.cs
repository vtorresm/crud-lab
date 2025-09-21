using System.ComponentModel.DataAnnotations;

namespace FarmaciaServicioApi.Models
{

    public class ProductoFarmaceutico
    {
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Formula { get; set; }
    public DateTime FechaCaducidad { get; set; }
    public decimal Precio { get; set; }
    }
}