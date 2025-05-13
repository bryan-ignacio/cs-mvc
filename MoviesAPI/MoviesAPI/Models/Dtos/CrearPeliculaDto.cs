using System;

namespace MoviesAPI.Models.Dtos;

public class CrearPeliculaDto
{

    public String Nombre { get; set; }
    public String Descripcion { get; set; }
    public int Duracion { get; set; }
    public string RutaImagen { get; set; }
    public enum TipoClasificacion { Siete, Trece, Dieciseis, Dieciocho }
    public TipoClasificacion Clasificacion { get; set; }
    public int CategoriaId { get; set; }
}
