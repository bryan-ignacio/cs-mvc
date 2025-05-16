using System;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;

namespace MoviesAPI.Repository.IRepository;

public interface IUsuarioRepository
{
    ICollection<Usuario> GetUsuarios();
    Usuario GetUsuario(int usuarioId);
    bool IsUnicoUsuario(string usuario);
    Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
    Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto);
}