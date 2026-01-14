using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Aplication.UseCases.EnfermeriaServices
{
    public class EditarEnfermeria
    {
        private readonly IFichaEnfermeria _enfermeriaRepo;

        public EditarEnfermeria(IFichaEnfermeria enfermeriaRepo)
        {
            _enfermeriaRepo = enfermeriaRepo;
        }

        public async Task EjecutarAsync(FichaEnfermeria enfermeria)
        {
            // Validamos los datos antes de enviar a actualizar
            ValidarEnfermeria(enfermeria);

            // Si pasa la validación, llamamos al repositorio
            await _enfermeriaRepo.Actualizar(enfermeria);
        }

        private void ValidarEnfermeria(FichaEnfermeria ficha)
        {
            if (ficha is null)
            {
                throw new ArgumentException(nameof(ficha), "La ficha no puede estar en blanco.");
            }
            if (string.IsNullOrWhiteSpace(ficha.Temperatura))
            {
                throw new ArgumentException("Existe un error en la temperatura");
            }
            if (string.IsNullOrWhiteSpace(ficha.Tratamiento))
            {
                throw new ArgumentException("Existe un error en el tratamiento.");
            }
            if (string.IsNullOrWhiteSpace(ficha.PesoTalla))
            {
                throw new ArgumentException("Existe un error en el peso y talla");
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
