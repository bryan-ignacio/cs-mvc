using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models.Dtos;

public class CrearCategoriaDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100, ErrorMessage = "El numero maximo de caracteres es de 100")]
    public string Nombre { get; set; }
}
