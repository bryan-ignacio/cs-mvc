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

    [HttpGet]
    public IActionResult Editar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var contacto = this._contexto.Contactos.Find(id);
        if (contacto == null)
        {
            return NotFound();
        }
        return View(contacto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Contacto contacto)
    {
        // valida las validaciones de los campos del modelo contacto.
        if (ModelState.IsValid)
        {
            this._contexto.Update(contacto);
            await this._contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [HttpGet]
    public IActionResult Detalle(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var contacto = this._contexto.Contactos.Find(id);
        if (contacto == null)
        {
            return NotFound();
        }
        return View(contacto);
    }



    [HttpGet]
    public IActionResult Borrar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var contacto = this._contexto.Contactos.Find(id);
        if (contacto == null)
        {
            return NotFound();
        }
        return View(contacto);
    }

    // el actionName se utiliza solo si el metodo que queremos tenga otro nombre.
    [HttpPost, ActionName("Borrar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BorrarContacto(int? id)
    {
        var contacto = await this._contexto.Contactos.FindAsync(id);
        if (contacto == null)
        {
            // lo mandamos a la misma vista.
            return View();
        }
        //borrado
        this._contexto.Remove(contacto);
        await this._contexto.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
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
//ActionResult<TipoDato> : recibe un tipo de dato y retorna una respuesta se usa en GET 

// IActionResult : se utiliza cuando no queremos retornar nada en la respuesta. se usa en PUT, DELETE, UPDATE