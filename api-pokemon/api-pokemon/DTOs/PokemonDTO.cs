using System;
using System.Text.Json.Serialization;

namespace api_pokemon.DTOs;

public class PokemonDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Sprites Sprites { get; set; }
}

public class Sprites
{
    [JsonPropertyName("front_default")]
    public string FrontDefault { get; set; }
}