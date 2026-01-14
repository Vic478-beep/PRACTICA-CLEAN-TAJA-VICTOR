using System;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;

namespace Aplication.UseCases.AdultoServices
{
    public class EditarAdulto
    {
        private readonly IAdulto _adultoRepo;

        public EditarAdulto(IAdulto adultoRepo)
        {
            _adultoRepo = adultoRepo;
        }

        public async Task EjecutarAsync(Adulto adulto)
        {
            ValidarAdulto(adulto);
            await _adultoRepo.Actualizar(adulto);
        }

        private void ValidarAdulto(Adulto adulto)
        {
            if (string.IsNullOrWhiteSpace(adulto.Nombres))
                throw new ArgumentException("El nombre no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(adulto.Apellidos))
                throw new ArgumentException("El apellido no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(adulto.FechaNac))
                throw new ArgumentException("La fecha de nacimiento es obligatoria.");

            if (adulto.Edad < 60 || adulto.Edad > 110)
                throw new ArgumentException("La edad debe estar entre 60 y 110 años.");

            if (string.IsNullOrWhiteSpace(adulto.EstadoCivil))
                throw new ArgumentException("El estado civil es obligatorio.");

            if (string.IsNullOrWhiteSpace(adulto.Domicilio))
                throw new ArgumentException("El domicilio es obligatorio.");

            if (adulto.Telefono <= 0)
                throw new ArgumentException("El número de teléfono no es válido.");
        }
    }
}