using System;

namespace api_pokemon.DTOs;

public class PokemonDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Sprites { get; set; }
}
