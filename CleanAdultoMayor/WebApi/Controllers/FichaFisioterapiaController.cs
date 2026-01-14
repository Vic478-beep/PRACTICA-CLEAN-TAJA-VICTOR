using Aplication.DTOs;
using Aplication.UseCases.EnfermeriaServices;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aplication.UseCases.FisioterapiaServices;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichaFisioterapiaController : ControllerBase
    {
        private readonly IFichaFisioterapia _ficha;
        private readonly IMapper _mapper;

        private readonly CrearFichaFisioterapia _crearFicha;
        private readonly EditarFisioterapia _editarFicha;
        private readonly EliminarFisioterapia _eliminarFicha;

        public FichaFisioterapiaController(
            IFichaFisioterapia ficha,
            IMapper mapper,
            CrearFichaFisioterapia crearFicha,
            EditarFisioterapia editarFicha,
            EliminarFisioterapia eliminarFicha
        )
        {
            _ficha = ficha;
            _mapper = mapper;
            _crearFicha = crearFicha;
            _editarFicha = editarFicha;
            _eliminarFicha = eliminarFicha;
        }

        [HttpGet("listar_fichas_fisioterapia")]
        public async Task<IActionResult> GetAll()
        {
            var fichas = await _ficha.All();

            if (!fichas.Any())
            {
                return NotFound("No hay fichas de fisioterapia que listar");
            }

            var fichasDto = _mapper.Map<IEnumerable<FichaFisioterapiaDTOs>>(fichas);
            return Ok(fichasDto);
        }

        [HttpGet("buscar_ficha_fisioterapia/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var fichas = await _ficha.ObtenerId(id);
            if (fichas == null)
            {
                return NotFound(new { mensaje = "La ficha no existe" });
            }
            var fichaDto = _mapper.Map<FichaFisioterapiaDTOs>(fichas);
            return Ok(fichaDto);
        }

        [HttpPost("crear_ficha_fisioterapia")]
        public async Task<IActionResult> Create([FromBody] FichaFisioterapiaDTOs fichaDTO)
        {
            try
            {
                var ficha = _mapper.Map<FichaFisioterapia>(fichaDTO);

                await _crearFicha.EjecutarAsync(ficha);

                return CreatedAtAction(nameof(GetById), new { id = ficha.CodFis }, fichaDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpPut("editar_ficha_fisioterapia/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FichaFisioterapiaDTOs fichaDTO)
        {
            if (id != fichaDTO.CodFis) return BadRequest("El ID de la URL no coincide con el del cuerpo.");

            try
            {
                var fichaExistente = await _ficha.ObtenerId(id);

                if (fichaExistente == null)
                    return NotFound("El adulto a editar no existe.");
                _mapper.Map(fichaDTO, fichaExistente);
                await _editarFicha.EjecutarAsync(fichaExistente);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("eliminar_ficha_fisioterapia/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var fichaExistente = await _ficha.ObtenerId(id);
                if (fichaExistente == null) return NotFound("La ficha a eliminar no existe.");

                await _eliminarFicha.EjecutarAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
