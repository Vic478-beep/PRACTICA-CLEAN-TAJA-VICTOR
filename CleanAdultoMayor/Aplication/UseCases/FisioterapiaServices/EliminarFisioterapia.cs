using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Aplication.UseCases.FisioterapiaServices
{
    public class EliminarFisioterapia
    {
        private readonly IFichaFisioterapia _fisioterapiaRepo;

        public EliminarFisioterapia(IFichaFisioterapia fisioterapiaRepo)
        {
            _fisioterapiaRepo = fisioterapiaRepo;
        }
        public async Task<string> EjecutarAsync(Guid id)
        {
            await _fisioterapiaRepo.Eliminar(id);
            return "Ficha de fisioterapia eliminada correctamente";
        }
    }
}
