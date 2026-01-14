using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Aplication.DTOs;
using Domain.Entities;
using Aplication.UseCases.AdultoServices;
using Aplication.UseCases;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdultoController : ControllerBase
    {
        private readonly IAdulto _adulto;
        private readonly IMapper _mapper;
        // Casos de Uso
        private readonly CrearAdulto _crearAdulto;
        private readonly EditarAdulto _editarAdulto;
        private readonly EliminarAdulto _eliminarAdulto;

        public AdultoController(
            IAdulto adulto,
            IMapper mapper,
            CrearAdulto crearAdulto,
            EditarAdulto editarAdulto,     
            EliminarAdulto eliminarAdulto  
        )
        {
            _adulto = adulto;
            _mapper = mapper;
            _crearAdulto = crearAdulto;
            _editarAdulto = editarAdulto;
            _eliminarAdulto = eliminarAdulto;
        }
        [HttpGet("listar_adultos")]
        public async Task<IActionResult> GetAll()
        {
            var adultos = await _adulto.All();

            if (!adultos.Any())
            {
                return NotFound("No hay adultos que listar");
            }

            var adultosDto = _mapper.Map<IEnumerable<AdultoDTOs>>(adultos);
            return Ok(adultosDto);
        }

        [HttpGet("buscar_adulto/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var adultoEncontrado = await _adulto.ObtenerId(id);

            if (adultoEncontrado == null)
            {
                return NotFound(new { mensaje = "El adulto no existe" });
            }

            var adultoDto = _mapper.Map<AdultoDTOs>(adultoEncontrado);

            return Ok(adultoDto);
        }

        [HttpPost("crear_adulto")]
        public async Task<IActionResult> Create([FromBody] AdultoDTOs adultoDTO)
        {
            try
            {
                var adulto = _mapper.Map<Adulto>(adultoDTO);

                await _crearAdulto.EjecutarAsync(adulto);

                return CreatedAtAction(nameof(GetById), new { id = adulto.Id }, adultoDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpPut("editar_adulto/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AdultoDTOs adultoDTO)
        {
            if (id != adultoDTO.Id) return BadRequest("El ID de la URL no coincide con el del cuerpo.");

            try
            {
                var adultoExistente = await _adulto.ObtenerId(id);

                if (adultoExistente == null)
                    return NotFound("El adulto a editar no existe.");

                
                _mapper.Map(adultoDTO, adultoExistente);

                
                await _editarAdulto.EjecutarAsync(adultoExistente);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("eliminar_adulto/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var adultoExistente = await _adulto.ObtenerId(id);
                if (adultoExistente == null) return NotFound("El adulto a eliminar no existe.");

                await _eliminarAdulto.EjecutarAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
