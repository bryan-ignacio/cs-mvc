using System;
using System.Text.Json;
using api_pokemon.DTOs;

namespace api_pokemon.Services;

public class PokemonService : IPokemonService
{
    private HttpClient _httpClient;

    public PokemonService(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public async Task<PokemonDTO> Obtener(string nombre)
    {
        // string url = $"https://pokeapi.co/api/v2/pokemon/{nombre}";
        var result = await this._httpClient.GetAsync(this._httpClient.BaseAddress + nombre);
        var body = await result.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var pokemonObj = JsonSerializer.Deserialize<PokemonDTO>(body, options);
        return pokemonObj;
    }
}
