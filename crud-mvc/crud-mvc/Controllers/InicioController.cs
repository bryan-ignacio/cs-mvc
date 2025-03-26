using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using crud_mvc.Models;
using crud_mvc.Data;
using Microsoft.EntityFrameworkCore;

namespace crud_mvc.Controllers;

public class InicioController : Controller
{
    private readonly ContactosDbContext _contexto;

    public InicioController(ContactosDbContext contexto)
    {
        this._contexto = contexto;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await this._contexto.Contactos.ToListAsync());
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
