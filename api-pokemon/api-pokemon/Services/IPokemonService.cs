using System;
using api_pokemon.DTOs;

namespace api_pokemon.Services;

public interface IPokemonService
{
    public Task<PokemonDTO> Obtener(string nombre);

}
