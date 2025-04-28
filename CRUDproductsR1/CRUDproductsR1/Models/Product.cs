using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDproductsR1.Models;

public class Product
{

    [Key]
    public int Id { get; set; }


    [Required(ErrorMessage = "El Codigo es obligatorio")]
    public string Codigo { get; set; }


    [Required(ErrorMessage = "El Nombre es obligatorio")]
    public string Nombre { get; set; }


    [Required(ErrorMessage = "El Precio Unitario es obligatorio")]
    public decimal PrecioUnitario { get; set; }


    [Required(ErrorMessage = "EL Precio Fardo es obligatorio")]
    public decimal PrecioFardo { get; set; }

}
