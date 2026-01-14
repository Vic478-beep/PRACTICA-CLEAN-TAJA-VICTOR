using Aplication.DTOs;
using Aplication.UseCases.ProteccionServices;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichaProteccionController : ControllerBase
    {
        private readonly IFichaProteccion _ficha;
        private readonly IMapper _mapper;
        
        private readonly CrearFichaProteccion _crearFicha;
        private readonly EditarProteccion _editarFicha;
        private readonly EliminarProteccion _eliminarFicha;

        public FichaProteccionController(
            IFichaProteccion ficha,
            IMapper mapper,
            CrearFichaProteccion crearFicha,
            EditarProteccion editarFicha,     
            EliminarProteccion eliminarFicha  
        )
        {
            _ficha = ficha;
            _mapper = mapper;
            _crearFicha = crearFicha;
            _editarFicha = editarFicha;
            _eliminarFicha = eliminarFicha;
        }

        [HttpGet("listar_fichas_proteccion")]
        public async Task<IActionResult> GetAll()
        {
            var fichas = await _ficha.All();

            if (!fichas.Any())
            {
                return NotFound("No hay fichas de proteccion que listar");
            }

            var fichasDto = _mapper.Map<IEnumerable<FichaProteccionDTOs>>(fichas);
            return Ok(fichasDto);
        }

        [HttpGet("buscar_ficha_proteccion/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var fichas = await _ficha.ObtenerId(id);
            if (fichas == null)
            {
                return NotFound(new { mensaje = "La ficha no existe" });
            }
            var fichaDto = _mapper.Map<FichaProteccionDTOs>(fichas);
            return Ok(fichaDto);
        }

        [HttpPost("crear_ficha_proteccion")]
        public async Task<IActionResult> Create([FromBody] FichaProteccionDTOs fichaDTO)
        {
            try
            {
                var ficha = _mapper.Map<FichaProteccion>(fichaDTO);

                await _crearFicha.EjecutarAsync(ficha);

                return CreatedAtAction(nameof(GetById), new { id = ficha.CodPro }, fichaDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpPut("editar_ficha_proteccion/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FichaProteccionDTOs fichaDTO)
        {
            if (id != fichaDTO.CodPro) return BadRequest("El ID de la URL no coincide con el del cuerpo.");

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

        [HttpDelete("eliminar_ficha_proteccion/{id}")]
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
