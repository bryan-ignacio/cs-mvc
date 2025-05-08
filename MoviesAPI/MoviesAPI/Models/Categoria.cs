using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MoviesAPI.Models;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Nombre { get; set; }

    [Required]
    public DateTime FechaCreacion { get; set; }
}