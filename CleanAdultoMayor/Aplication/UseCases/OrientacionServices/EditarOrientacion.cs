using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Aplication.UseCases.OrientacionServices
{
    public class EditarOrientacion
    {
        private readonly IFichaOrientacion _orientacionRepo;

        public EditarOrientacion(IFichaOrientacion orientacionRepo)
        {
            _orientacionRepo = orientacionRepo;
        }

        public async Task EjecutarAsync(FichaOrientacion orientacion)
        {
            // Validamos los datos antes de enviar a actualizar
            ValidarOrientacion(orientacion);

            // Si pasa la validación, llamamos al repositorio
            await _orientacionRepo.Actualizar(orientacion);
        }

        private void ValidarOrientacion(FichaOrientacion ficha)
        {
            if (ficha is null)
            {
                throw new ArgumentException(nameof(ficha), "La ficha no puede estar en blanco.");
            }
            if (string.IsNullOrWhiteSpace(ficha.FechaOrientacion))
            {
                throw new ArgumentException("Existe un error en la fecha.");
            }
            if (string.IsNullOrWhiteSpace(ficha.TipoOrientacion))
            {
                throw new ArgumentException("Existe un error en el tipo de orientacion.");
            }
            if (string.IsNullOrWhiteSpace(ficha.Descripcion))
            {
                throw new ArgumentException("Existe un error en la descripcion.");
            }
            if (ficha.IdAdulto == Guid.Empty)
            {
                throw new ArgumentException("Existe un error en el identificador del adulto..");
            }
            //CONSISTENCIA ENTRE NAVEGACION Y FK (SI AUTOR VIENE SETEADO)
            if (ficha.Adulto is not null && ficha.Adulto.Id != ficha.IdAdulto)
            {
                throw new ArgumentException("El adulto de navegacion no...");
            }
        }
    }
}
