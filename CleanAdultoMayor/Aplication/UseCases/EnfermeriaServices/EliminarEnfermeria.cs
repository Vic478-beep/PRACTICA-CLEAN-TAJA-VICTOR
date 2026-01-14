using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Aplication.UseCases.EnfermeriaServices
{
    public class EliminarEnfermeria
    {
        private readonly IFichaEnfermeria _enfermeriaRepo;

        public EliminarEnfermeria(IFichaEnfermeria enfermeriaRepo)
        {
            _enfermeriaRepo = enfermeriaRepo;
        }
        public async Task<string> EjecutarAsync(Guid id)
        {
            await _enfermeriaRepo.Eliminar(id);
            return "Ficha de enfermeria eliminada correctamente";
        }
    }
}
