using System;
using System.ComponentModel.DataAnnotations;

namespace crud_mvc.Models;

public class Producto
{
    [Key]
    public int ProductoId { get; set; }
    public string Nombre { get; set; }
    public float Precio { get; set; }
}
