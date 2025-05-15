using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Repository.IRepository;

namespace MoviesAPI.Controllers
{
    [Route("api/peliculas")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaRepository _peliculaRepo;
        private readonly IMapper _mapper;

        public PeliculasController(IPeliculaRepository peliculaRepo, IMapper mapper)
        {
            this._peliculaRepo = peliculaRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetPeliculas()
        {
            var listaPeliculas = this._peliculaRepo.GetPeliculas();
            var listaPeliculasDto = new List<PeliculaDto>();
            foreach (var lista in listaPeliculas)
            {
                listaPeliculasDto.Add(this._mapper.Map<PeliculaDto>(lista));
            }
            return Ok(listaPeliculasDto);
        }

        [HttpGet("{peliculaId:int}", Name = "GetPelicula")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPelicula(int peliculaId)
        {
            var pelicula = this._peliculaRepo.GetPelicula(peliculaId);
            if (pelicula == null)
            {
                return NotFound();
            }
            var peliculaDto = this._mapper.Map<PeliculaDto>(pelicula);
            return Ok(peliculaDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PeliculaDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearPelicula([FromBody] CrearPeliculaDto crearPeliculaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (crearPeliculaDto == null)
            {
                return BadRequest(ModelState);
            }

            if (this._peliculaRepo.ExistePelicula(crearPeliculaDto.Nombre))
            {
                ModelState.AddModelError("", "La pelicula ya existe.");
                return StatusCode(404, ModelState);
            }

            var pelicula = this._mapper.Map<Pelicula>(crearPeliculaDto);

            if (!this._peliculaRepo.CrearPelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo salio mal al guardar el registro ${pelicula.Nombre}");
                return StatusCode(404, ModelState);
            }
            // crea la pelicula y retorna los datos de esa pelicula.
            return CreatedAtRoute("GetPelicula", new { peliculaId = pelicula.Id }, pelicula);
        }

        [HttpPatch("peliculaId:int", Name = "ActualizarPatchPelicula")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult ActualizarPatchPelicula(int peliculaId, [FromBody] PeliculaDto peliculaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (peliculaDto == null || peliculaId != peliculaDto.Id)
            {
                return BadRequest(ModelState);
            }

            var peliculaExistente = this._peliculaRepo.GetPelicula(peliculaId);
            if (peliculaExistente == null)
            {
                return NotFound($"No se encontro la pelicula con ID {peliculaId}");
            }

            // se hace la conversion.
            var pelicula = this._mapper.Map<Pelicula>(peliculaDto);

            if (!this._peliculaRepo.ActualizarPelicula(pelicula))
            {
                ModelState.AddModelError("", $"No se pudo actualizar el registro {pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{peliculaId:int}", Name = "BorrarPelicula")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BorrarPelicula(int peliculaId)
        {
            if (!this._peliculaRepo.ExistePelicula(peliculaId))
            {
                return NotFound();
            }

            var pelicula = this._peliculaRepo.GetPelicula(peliculaId);

            if (!this._peliculaRepo.BorrarPelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro {pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


    }
}
