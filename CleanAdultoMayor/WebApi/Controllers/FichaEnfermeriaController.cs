using Aplication.DTOs;
using Aplication.UseCases.EnfermeriaServices;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichaEnfermeriaController : ControllerBase
    {
        private readonly IFichaEnfermeria _ficha;
        private readonly IMapper _mapper;

        private readonly CrearFichaEnfermeria _crearFicha;
        private readonly EditarEnfermeria _editarFicha;
        private readonly EliminarEnfermeria _eliminarFicha;

        public FichaEnfermeriaController(
            IFichaEnfermeria ficha,
            IMapper mapper,
            CrearFichaEnfermeria crearFicha,
            EditarEnfermeria editarFicha,    
            EliminarEnfermeria eliminarFicha  
        )
        {
            _ficha = ficha;
            _mapper = mapper;
            _crearFicha = crearFicha;
            _editarFicha = editarFicha;
            _eliminarFicha = eliminarFicha;
        }

        [HttpGet("listar_fichas_enfermeria")]
        public async Task<IActionResult> GetAll()
        {
            var fichas = await _ficha.All();

            if (!fichas.Any())
            {
                return NotFound("No hay fichas de enfermeria que listar");
            }

            var fichasDto = _mapper.Map<IEnumerable<FichaEnfermeriaDTOs>>(fichas);
            return Ok(fichasDto);
        }

        [HttpGet("buscar_ficha_enfermeria/{CodEnf}")]
        public async Task<IActionResult> GetById(Guid CodEnf)
        {
            var fichas = await _ficha.ObtenerId(CodEnf);
            if (fichas == null)
            {
                return NotFound(new { mensaje = "La ficha no existe" });
            }
            var fichaDto = _mapper.Map<FichaEnfermeriaDTOs>(fichas);
            return Ok(fichaDto);
        }

        [HttpPost("crear_ficha_enfermeria")]
        public async Task<IActionResult> Create([FromBody] FichaEnfermeriaDTOs fichaDTO)
        {
            try
            {
                var ficha = _mapper.Map<FichaEnfermeria>(fichaDTO);

                await _crearFicha.EjecutarAsync(ficha);

                return CreatedAtAction(nameof(GetById), new { CodEnf = ficha.CodEnf }, fichaDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpPut("editar_ficha_enfermeria/{CodEnf}")]
        public async Task<IActionResult> Update(Guid CodEnf, [FromBody] FichaEnfermeriaDTOs fichaDTO)
        {
            if (CodEnf != fichaDTO.CodEnf) return BadRequest("El ID de la URL no coincide con el del cuerpo.");

            try
            {
                var fichaExistente = await _ficha.ObtenerId(CodEnf);

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

        [HttpDelete("eliminar_ficha_enfermeria/{CodEnf}")]
        public async Task<IActionResult> Delete(Guid CodEnf)
        {
            try
            {
                var fichaExistente = await _ficha.ObtenerId(CodEnf);
                if (fichaExistente == null) return NotFound("La ficha a eliminar no existe.");

                await _eliminarFicha.EjecutarAsync(CodEnf);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
