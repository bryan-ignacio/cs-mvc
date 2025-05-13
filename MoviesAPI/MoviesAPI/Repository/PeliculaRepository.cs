using System;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Repository.IRepository;

namespace MoviesAPI.Repository;

public class PeliculaRepository : IPeliculaRepository
{
    private readonly AppDbContext _context;

    public PeliculaRepository(AppDbContext context)
    {
        this._context = context;
    }

    public bool ActualizarPelicula(Pelicula pelicula)
    {
        pelicula.FechaCreacion = DateTime.Now;
        this._context.Pelicula.Update(pelicula);
        return Guardar();
    }

    public bool BorrarPelicula(Pelicula pelicula)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Pelicula> BuscarPelicula(string nombre)
    {
        throw new NotImplementedException();
    }

    public bool CrearPelicula(Pelicula pelicula)
    {
        throw new NotImplementedException();
    }

    public bool ExistePelicula(int id)
    {
        throw new NotImplementedException();
    }

    public bool ExistePelicula(string nombre)
    {
        throw new NotImplementedException();
    }

    public Pelicula GetPelicula(int peliculaId)
    {
        throw new NotImplementedException();
    }

    public ICollection<Pelicula> GetPeliculas()
    {
        throw new NotImplementedException();
    }

    public ICollection<Pelicula> GetPeliculasEnCategoria(int categoriaId)
    {
        throw new NotImplementedException();
    }

    public bool Guardar()
    {
        throw new NotImplementedException();
    }
}
