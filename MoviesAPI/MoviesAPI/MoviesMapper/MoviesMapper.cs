using System;
using AutoMapper;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;

namespace MoviesAPI.MoviesMapper;

public class MoviesMapper : Profile
{
    public MoviesMapper()
    {
        // el reverse se usa para que se comuniquen entre si. -> <-
        CreateMap<Categoria, CategoriaDto>().ReverseMap();
        CreateMap<Categoria, CrearCategoriaDto>().ReverseMap();
    }
}
