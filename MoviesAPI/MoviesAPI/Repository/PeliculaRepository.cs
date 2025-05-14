using System;
using Microsoft.EntityFrameworkCore;
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
        this._context.Pelicula.Remove(pelicula);
        return Guardar();
    }

    public bool CrearPelicula(Pelicula pelicula)
    {
        pelicula.FechaCreacion = DateTime.Now;
        this._context.Pelicula.Add(pelicula);
        return Guardar();
    }

    public IEnumerable<Pelicula> BuscarPelicula(string nombre)
    {
        IQueryable<Pelicula> query = this._context.Pelicula;
        if (!string.IsNullOrEmpty(nombre))
        {
            query = query.Where(
                e => e.Nombre.Contains(nombre) || e.Descripcion.Contains(nombre)
                );
        }
        return query.ToList();
    }

    public bool ExistePelicula(int id)
    {
        return this._context.Pelicula.Any(p => p.Id == id);
    }

    public bool ExistePelicula(string nombre)
    {
        bool valor = this._context.Pelicula.Any(
            p => p.Nombre.ToLower().Trim() == nombre.ToLower().Trim()
        );
        return valor;
    }

    public Pelicula GetPelicula(int peliculaId)
    {
        return this._context.Pelicula.FirstOrDefault(p => p.Id == peliculaId);
    }

    public ICollection<Pelicula> GetPeliculas()
    {
        return this._context.Pelicula.OrderBy(p => p.Nombre).ToList();
    }

    public ICollection<Pelicula> GetPeliculasEnCategoria(int categoriaId)
    {
        return this._context.Pelicula.Include(
            ca => ca.Categoria
        )
        .Where(ca => ca.CategoriaId == categoriaId).ToList();
    }

    public bool Guardar()
    {
        return this._context.SaveChanges() >= 0 ? true : false;
    }
}
