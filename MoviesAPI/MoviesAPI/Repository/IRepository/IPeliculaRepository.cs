using System;
using MoviesAPI.Models;

namespace MoviesAPI.Repository.IRepository;

public interface IPeliculaRepository
{
    ICollection<Pelicula> GetPeliculas();

    ICollection<Pelicula> GetPeliculasEnCategoria(int categoriaId);
    IEnumerable<Pelicula> BuscarPelicula(string nombre);
    Pelicula GetPelicula(int peliculaId);
    bool ExistePelicula(int id);
    bool ExistePelicula(string nombre);
    bool CrearPelicula(Pelicula pelicula);
    bool ActualizarPelicula(Pelicula pelicula);
    bool BorrarPelicula(Pelicula pelicula);
    bool Guardar();
}
