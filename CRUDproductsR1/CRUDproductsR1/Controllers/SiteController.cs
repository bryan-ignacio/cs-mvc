using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDproductsR1.Models;
using CRUDproductsR1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace CRUDproductsR1.Controllers;

public class SiteController : Controller
{

    private readonly ProductsDbContext _contexto;

    public SiteController(ProductsDbContext context)
    {
        this._contexto = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Product> products = await this._contexto.Product.ToListAsync();
        return View(products);
    }

    [HttpGet]
    public IActionResult Editar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var product = this._contexto.Product.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Product product)
    {
        if (ModelState.IsValid)
        {
            this._contexto.Update(product);
            await this._contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [HttpGet]
    public IActionResult Info(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = this._contexto.Product.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpGet]
    public IActionResult Borrar(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var product = this._contexto.Product.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Borrar(Product product)
    {
        this._contexto.Remove(product);
        await this._contexto.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Agregar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Agregar(Product product)
    {
        if (ModelState.IsValid)
        {
            var exist = await _contexto.Product.AnyAsync(p => p.Codigo == product.Codigo);
            if (exist)
            {
                ModelState.AddModelError("Codigo", "Ya existe un producto con este código.");
                return View();
            }
            this._contexto.Product.Add(product);
            await this._contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [HttpGet]
    public IActionResult Subir()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Subir(IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
        {
            ModelState.AddModelError("archivo", "Por favor seleccione un archivo válido.");
            return View();
        }

        using var stream = new StreamReader(archivo.OpenReadStream(), Encoding.UTF8);
        string? linea;
        var productosNuevos = new List<Product>();
        var productosActualizar = new List<Product>();

        while ((linea = await stream.ReadLineAsync()) != null)
        {
            var columnas = linea.Split(',');

            if (columnas.Length != 4) continue;

            try
            {
                string codigo = columnas[0].Trim();
                string nombre = columnas[1].Trim();
                decimal precioUnitario = decimal.Parse(columnas[2].Trim(), CultureInfo.InvariantCulture);
                decimal precioFardo = decimal.Parse(columnas[3].Trim(), CultureInfo.InvariantCulture);

                var existente = await _contexto.Product.FirstOrDefaultAsync(p => p.Codigo == codigo);

                if (existente != null)
                {
                    // actualizar valores
                    existente.Nombre = nombre;
                    existente.PrecioUnitario = precioUnitario;
                    existente.PrecioFardo = precioFardo;
                    productosActualizar.Add(existente);
                }
                else
                {
                    productosNuevos.Add(new Product
                    {
                        Codigo = codigo,
                        Nombre = nombre,
                        PrecioUnitario = precioUnitario,
                        PrecioFardo = precioFardo
                    });
                }
            }
            catch
            {
                continue; // saltar línea malformada
            }
        }

        if (productosNuevos.Any())
        {
            await _contexto.Product.AddRangeAsync(productosNuevos);
        }

        if (productosActualizar.Any())
        {
            _contexto.Product.UpdateRange(productosActualizar);
        }

        await _contexto.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Mostrar()
    {
        List<Product> products = await this._contexto.Product.ToListAsync();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Buscar(string? codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
        {
            TempData["Mensaje"] = "Debe ingresar un código.";
            return RedirectToAction(nameof(Mostrar));
        }

        var product = await _contexto.Product.FirstOrDefaultAsync(p => p.Codigo == codigo);

        if (product == null)
        {
            TempData["Mensaje"] = $"No se encontró ningún producto con el código '{codigo}'.";
            return RedirectToAction(nameof(Mostrar));
        }
        return View("Buscar", product);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
