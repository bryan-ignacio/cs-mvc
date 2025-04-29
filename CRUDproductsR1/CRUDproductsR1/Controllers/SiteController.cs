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

        List<Product> productos = new List<Product>();

        using (var stream = new StreamReader(archivo.OpenReadStream(), Encoding.UTF8))
        {
            string? linea;
            while ((linea = await stream.ReadLineAsync()) != null)
            {
                var columnas = linea.Split(',');

                if (columnas.Length != 4)
                {
                    continue; // saltar líneas mal formateadas
                }
                try
                {
                    // se crea el producto con los datos del archivo.
                    var producto = new Product
                    {
                        Codigo = columnas[0].Trim(),
                        Nombre = columnas[1].Trim(),
                        PrecioUnitario = decimal.Parse(columnas[2].Trim(), CultureInfo.InvariantCulture),
                        PrecioFardo = decimal.Parse(columnas[3].Trim(), CultureInfo.InvariantCulture)
                    };
                    // ahora validamos si este producto con este codigo ya existe?

                    productos.Add(producto);
                }
                catch (Exception)
                {
                    // Opcional: manejar errores de parseo
                    continue;
                }
            }
        }
        if (productos.Count > 0)
        {
            await _contexto.Product.AddRangeAsync(productos);
            await _contexto.SaveChangesAsync();
        }

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
        if (codigo == null)
        {
            return NotFound();
        }
        // este sirve si codigo es llave primaria por lo que no sirve para esto.
        // var product = await this._contexto.Product.FindAsync(codigo);
        var product = await _contexto.Product.FirstOrDefaultAsync(p => p.Codigo == codigo);

        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
