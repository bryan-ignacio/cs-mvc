using System.ComponentModel.DataAnnotations;
namespace crud_mvc.Models;

public class Contacto
{
    //campos de la tabla en la base de datos.
    [Key] //primary key
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")] //data anotations.
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El telefono es obligatorio.")]
    public string Telefono { get; set; }

    [Required(ErrorMessage = "El celular es obligatorio.")]
    public string Celular { get; set; }

    public DateTime FechaCreacion { get; set; }
}
