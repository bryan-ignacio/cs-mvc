using api_pokemon.DTOs;
using api_pokemon.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_pokemon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            this._pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<PokemonDTO> ObtenerPokemon(string nombre)
        {
            return await this._pokemonService.Obtener(nombre);
        }

    }
}
