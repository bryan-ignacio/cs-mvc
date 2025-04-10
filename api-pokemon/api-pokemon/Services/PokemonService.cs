using System;
using System.Text.Json;
using api_pokemon.DTOs;

namespace api_pokemon.Services;

public class PokemonService : IPokemonService
{
    private HttpClient _httpClient;

    public PokemonService()
    {
        this._httpClient = new HttpClient();
    }

    public async Task<PokemonDTO> Obtener(string nombre)
    {
        string url = $"https://pokeapi.co/api/v2/pokemon/{nombre}";
        var result = await this._httpClient.GetAsync(url);
        var body = await result.Content.ReadAsStringAsync();
        var pokemonObj = JsonSerializer.Deserialize<PokemonDTO>(body);
        return pokemonObj;
    }

}
