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
        throw new NotImplementedException();
    }

    public bool ExisteCategoria(int id)
    {
        throw new NotImplementedException();
    }

    public bool ExixteCategoria(string nombre)
    {
        throw new NotImplementedException();
    }

    public Categoria GetCategoria(int CategoriaId)
    {
        throw new NotImplementedException();
    }

    public ICollection<Categoria> GetCategorias()
    {
        throw new NotImplementedException();
    }

    public bool Guardar()
    {
        throw new NotImplementedException();
    }
}
