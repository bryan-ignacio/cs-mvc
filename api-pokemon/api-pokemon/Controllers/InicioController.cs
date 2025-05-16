using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using api_pokemon.Models;
using api_pokemon.Services;
using System.Threading.Tasks;

namespace api_pokemon.Controllers;

public class InicioController : Controller
{
    IPokemonService _pokemonService;

    public InicioController(IPokemonService pokemonService)
    {
        this._pokemonService = pokemonService;
    }

    public async Task<IActionResult> Index(string nombre = "pikachu")
    {
        try
        {
            var pokemon = await this._pokemonService.Obtener(nombre);
            return View(pokemon);
        }
        catch (Exception e)
        {
            ViewBag.Error = $"No se encontro el Pokemon: {nombre}";
            return View(null);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
