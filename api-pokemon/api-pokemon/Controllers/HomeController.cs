using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using api_pokemon.Models;
using api_pokemon.Services;

namespace api_pokemon.Controllers;

public class HomeController : Controller
{
    IPokemonService _pokemonService;

    public HomeController(IPokemonService pokemonService)
    {
        this._pokemonService = pokemonService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
