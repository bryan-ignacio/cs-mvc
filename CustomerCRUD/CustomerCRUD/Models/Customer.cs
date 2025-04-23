using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace CustomerCRUD.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio")]
    public string Apellido { get; set; }

    [Required(ErrorMessage = "El Nit es obligatorio")]
    public string Nit { get; set; }

    [Required(ErrorMessage = "El Email es obligatorio")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El Telefono es obligatorio")]
    public string Telefono { get; set; }

    [Required(ErrorMessage = "La Direccion es obligatorio")]
    public string Direccion { get; set; }

    public DateTime FechaCreacion { get; set; }
}
