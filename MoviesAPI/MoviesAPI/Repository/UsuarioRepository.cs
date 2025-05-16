using System;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Repository.IRepository;
using System.Security.Cryptography;

namespace MoviesAPI.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _dbcontext;

    public UsuarioRepository(AppDbContext dbContext)
    {
        this._dbcontext = dbContext;
    }

    public Usuario GetUsuario(int usuarioId)
    {
        return this._dbcontext.Usuario.FirstOrDefault(u => u.Id == usuarioId);
    }

    public ICollection<Usuario> GetUsuarios()
    {
        return this._dbcontext.Usuario.OrderBy(u => u.NombreUsuario).ToList();
    }

    public bool IsUnicoUsuario(string usuario)
    {
        var usuarioBd = this._dbcontext.Usuario.FirstOrDefault(
            u => u.NombreUsuario == usuario
        );
        if (usuarioBd == null)
        {
            return true;
        }
        return false;
    }

    public Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
    {
        var passwordEncriptado = obtenermd5(usuarioLoginDto.Password);
        var usuario = this._dbcontext.Usuario.FirstOrDefault(u => u.NombreUsuario.ToLower() == usuarioLoginDto.NombreUsuario);
    }

    public async Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto)
    {
        var passwordEncriptado = obtenermd5(usuarioRegistroDto.Password);

        Usuario usuario = new Usuario()
        {
            NombreUsuario = usuarioRegistroDto.NombreUsuario,
            Password = passwordEncriptado,
            Nombre = usuarioRegistroDto.Nombre,
        };

        this._dbcontext.Usuario.Add(usuario);
        await this._dbcontext.SaveChangesAsync();
        usuario.Password = passwordEncriptado;
        return usuario;
    }

    // metodo para encriptar password con md5
    // se una tanto en el Acceso como en el Registro.
    public static string obtenermd5(string valor)
    {
        MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
        byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
        data = x.ComputeHash(data);
        string resp = "";
        for (int i = 0; i < data.Length; i++)
        {
            resp += data[i].ToString("x2").ToLower();
        }
        // encripta el password como md5.
        return resp;
    }

}
