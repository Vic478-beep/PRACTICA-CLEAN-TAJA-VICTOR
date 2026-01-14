using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Aplication.UseCases.FisioterapiaServices
{
    public class EditarFisioterapia
    {
        private readonly IFichaFisioterapia _fisioterapiaRepo;

        public EditarFisioterapia(IFichaFisioterapia fisioterapiaRepo)
        {
            _fisioterapiaRepo = fisioterapiaRepo;
        }

        public async Task EjecutarAsync(FichaFisioterapia fisioterapia)
        {
            // Validamos los datos antes de enviar a actualizar
            ValidarFisioterapia(fisioterapia);

            // Si pasa la validación, llamamos al repositorio
            await _fisioterapiaRepo.Actualizar(fisioterapia);
        }

        private void ValidarFisioterapia(FichaFisioterapia ficha)
        {
            if (ficha is null)
            {
                throw new ArgumentException("La ficha no puede estar en blanco.", nameof(ficha));
            }

            if (string.IsNullOrWhiteSpace(ficha.FechaProgramacion))
            {
                throw new ArgumentException("La fecha de programación es obligatoria.");
            }


            if (ficha.NumeroSesiones <= 0)
            {
                throw new ArgumentException("El número de sesiones debe ser mayor a 0.");
            }

            if (string.IsNullOrWhiteSpace(ficha.MotivoConsulta))
            {
                throw new ArgumentException("El motivo de la consulta es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(ficha.EquiposEmpleados))
            {
                throw new ArgumentException("Debe especificar los equipos empleados.");
            }

            if (ficha.IdAdulto == Guid.Empty)
            {
                throw new ArgumentException("El identificador del adulto es inválido.");
            }

            // CONSISTENCIA ENTRE NAVEGACION Y FK (SI ADULTO VIENE SETEADO)
            if (ficha.Adulto is not null && ficha.Adulto.Id != ficha.IdAdulto)
            {
                throw new ArgumentException("El adulto de navegación no coincide con el ID foráneo.");
            }
        }
    }
}
