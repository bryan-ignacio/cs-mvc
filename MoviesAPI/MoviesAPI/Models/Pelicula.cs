using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models;

public class Pelicula
{
    [Key]
    public int Id { get; set; }
    public String Nombre { get; set; }
    public String Descripcion { get; set; }
    public int Duracion { get; set; }
    public string RutaImagen { get; set; }
    public enum TipoClasificacion
    {
        Siete, Trece, Dieciseis, Dieciocho
    }
    public TipoClasificacion Clasificacion { get; set; }
    public DateTime FechaCreacion { get; set; }

    //Relacion con Categoria, llave foranea.
    public int CategoriaId { get; set; }

    [ForeignKey("CategoriaId")]
    public Categoria Categoria { get; set; }
}
