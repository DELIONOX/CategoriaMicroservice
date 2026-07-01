using Microsoft.AspNetCore.Mvc;
using CategoriaMicroservice.Models;
using CategoriaMicroservice.Services;

namespace CategoriaMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;

        public CategoriasController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoriaService.ObtenerTodas());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var categoria = _categoriaService.ObtenerPorId(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Categoria nuevaCategoria)
        {
            if (string.IsNullOrWhiteSpace(nuevaCategoria.Nombre))
            {
                return BadRequest("El nombre de la categoría es obligatorio.");
            }

            var categoriaCreada = _categoriaService.Agregar(nuevaCategoria);
            return CreatedAtAction(nameof(GetById), new { id = categoriaCreada.Id }, categoriaCreada);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Categoria categoriaActualizada)
        {
            if (string.IsNullOrWhiteSpace(categoriaActualizada.Nombre))
            {
                return BadRequest("El nombre de la categoría es obligatorio.");
            }

            var resultado = _categoriaService.Actualizar(id, categoriaActualizada);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var eliminado = _categoriaService.Eliminar(id);

            if (!eliminado)
            {
                return NotFound("Categoria no encontrada");
            }

            return Ok("Categoria eliminada");
        }
    }
}