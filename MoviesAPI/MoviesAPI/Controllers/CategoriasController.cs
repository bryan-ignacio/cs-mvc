using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Repository.IRepository;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _ctRepo;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository ctRepo, IMapper mapper)
        {
            this._ctRepo = ctRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategorias()
        {
            var ListaCategorias = this._ctRepo.GetCategorias();
            var listaCategoriasDto = new List<CategoriaDto>();

            foreach (var lista in ListaCategorias)
            {
                listaCategoriasDto.Add(this._mapper.Map<CategoriaDto>(lista));
            }
            return Ok(listaCategoriasDto);
        }


        // otro endpoint.
        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoria(int categoriaId)
        {
            var itemCategoria = this._ctRepo.GetCategoria(categoriaId);

            if (itemCategoria == null)
            {
                return NotFound();
            }
            //se hace la conversion de categoria a categoriaDto.
            var itemCategoriaDto = this._mapper.Map<CategoriaDto>(itemCategoria);

            return Ok(itemCategoriaDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CrearCategoria([FromBody] CrearCategoriaDto crearCategoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearCategoriaDto == null)
            {
                return BadRequest(ModelState);
            }

            if (this._ctRepo.ExixteCategoria(crearCategoriaDto.Nombre))
            {
                ModelState.AddModelError("", $"La Categoria ya existe.");
                return StatusCode(404, ModelState);
            }
            // de dto a categoria.
            var categoria = this._mapper.Map<Categoria>(crearCategoriaDto);

            if (!this._ctRepo.CrearCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salio mal al guardar el registro ${categoria.Nombre}.");
                return StatusCode(404, ModelState);
            }
            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id }, categoria);
        }
    }
}
