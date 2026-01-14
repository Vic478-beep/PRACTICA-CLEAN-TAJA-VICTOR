using Aplication.DTOs;
using Aplication.UseCases.OrientacionServices;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichaOrientacionController : ControllerBase
    {
        private readonly IFichaOrientacion _ficha;
        private readonly IMapper _mapper;

        private readonly CrearFichaOrientacion _crearFicha;
        private readonly EditarOrientacion _editarFicha;
        private readonly EliminarOrientacion _eliminarFicha;

        public FichaOrientacionController(
            IFichaOrientacion ficha,
            IMapper mapper,
            CrearFichaOrientacion crearFicha,
            EditarOrientacion editarFicha,     
            EliminarOrientacion eliminarFicha  
        )
        {
            _ficha = ficha;
            _mapper = mapper;
            _crearFicha = crearFicha;
            _editarFicha = editarFicha;
            _eliminarFicha = eliminarFicha;
        }

        [HttpGet("listar_fichas_orientacion")]
        public async Task<IActionResult> GetAll()
        {
            var fichas = await _ficha.All();

            if (!fichas.Any())
            {
                return NotFound("No hay fichas de orientacion que listar");
            }

            var fichasDto = _mapper.Map<IEnumerable<FichaOrientacionDTOs>>(fichas);
            return Ok(fichasDto);
        }

        [HttpGet("buscar_ficha_orientacion/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var fichas = await _ficha.ObtenerId(id);
            if (fichas == null)
            {
                return NotFound(new { mensaje = "La ficha no existe" });
            }
            var fichaDto = _mapper.Map<FichaOrientacionDTOs>(fichas);
            return Ok(fichaDto);
        }

        [HttpPost("crear_ficha_orientacion")]
        public async Task<IActionResult> Create([FromBody] FichaOrientacionDTOs fichaDTO)
        {
            try
            {
                var ficha = _mapper.Map<FichaOrientacion>(fichaDTO);

                await _crearFicha.EjecutarAsync(ficha);

                return CreatedAtAction(nameof(GetById), new { id = ficha.CodOri }, fichaDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpPut("editar_ficha_orientacion/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FichaOrientacionDTOs fichaDTO)
        {
            if (id != fichaDTO.CodOri) return BadRequest("El ID de la URL no coincide con el del cuerpo.");

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

        [HttpDelete("eliminar_ficha_orientacion/{id}")]
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
