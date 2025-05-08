using System;
using MoviesAPI.Models;

namespace MoviesAPI.Repository.IRepository;

public interface ICategoriaRepository
{
    ICollection<Categoria> GetCategorias();
    Categoria GetCategoria(int CategoriaId);
    bool ExisteCategoria(int id);
    bool ExixteCategoria(string nombre);
    bool CrearCategoria(Categoria categoria);
    bool ActualizarCategoria(Categoria categoria);
    bool BorrarCategoria(Categoria categoria);
    bool Guardar();
}
