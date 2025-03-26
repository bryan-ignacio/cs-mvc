using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using crud_mvc.Models;
using crud_mvc.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Contacto contacto)
    {
        // valida las validaciones de los campos del modelo contacto.
        if (ModelState.IsValid)
        {
            //agregar fecha actual
            contacto.FechaCreacion = DateTime.Now;
            this._contexto.Contactos.Add(contacto);
            await this._contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
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
