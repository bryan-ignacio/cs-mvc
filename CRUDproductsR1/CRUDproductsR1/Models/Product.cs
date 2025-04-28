using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRUDproductsR1.Models;

public class Product
{
    [Display(Name = "Id")]
    [Key]
    public int Id { get; set; }

    [Display(Name = "Codigo del Producto")]
    [Required(ErrorMessage = "El Codigo es obligatorio")]
    public string Codigo { get; set; }

    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "El Nombre es obligatorio")]
    public string Nombre { get; set; }

    [Display(Name = "Precio Unitario")]
    [Required(ErrorMessage = "El Precio Unitario es obligatorio")]
    public decimal PrecioUnitario { get; set; }

    [Display(Name = "Precio Fardo")]
    [Required(ErrorMessage = "EL Precio Fardo es obligatorio")]
    public decimal PrecioFardo { get; set; }

}
