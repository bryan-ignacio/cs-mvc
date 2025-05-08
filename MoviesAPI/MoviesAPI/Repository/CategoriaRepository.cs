using System;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Repository.IRepository;

namespace MoviesAPI.Repository;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _dbContext;
    public CategoriaRepository(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public bool ActualizarCategoria(Categoria categoria)
    {
        categoria.FechaCreacion = DateTime.Now;
        this._dbContext.Categoria.Update(categoria);
        return Guardar();
    }

    public bool BorrarCategoria(Categoria categoria)
    {
        this._dbContext.Categoria.Remove(categoria);
        return Guardar();
    }

    public bool CrearCategoria(Categoria categoria)
    {
        categoria.FechaCreacion = DateTime.Now;
        this._dbContext.Categoria.Add(categoria);
        return Guardar();
    }

    public bool ExisteCategoria(int id)
    {
        return this._dbContext.Categoria.Any(
            c => c.Id == id
        );
    }

    public bool ExixteCategoria(string nombre)
    {
        bool valor = this._dbContext.Categoria.Any(
            c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim()
        );
        return valor;
    }

    public Categoria GetCategoria(int CategoriaId)
    {
        return this._dbContext.Categoria.FirstOrDefault(c => c.Id == CategoriaId);
    }

    public ICollection<Categoria> GetCategorias()
    {
        return this._dbContext.Categoria.OrderBy(c => c.Nombre).ToList();
    }

    // guarda los cambios siempre que se cree un registro.
    public bool Guardar()
    {
        return this._dbContext.SaveChanges() >= 0 ? true : false;
    }
}
