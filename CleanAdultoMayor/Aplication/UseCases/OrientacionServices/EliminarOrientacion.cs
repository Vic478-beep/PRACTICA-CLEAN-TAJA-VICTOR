using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Aplication.UseCases.OrientacionServices
{
    public class EliminarOrientacion
    {
        private readonly IFichaOrientacion _orientacionRepo;

        public EliminarOrientacion(IFichaOrientacion orientacionRepo)
        {
            _orientacionRepo = orientacionRepo;
        }
        public async Task<string> EjecutarAsync(Guid id)
        {
            await _orientacionRepo.Eliminar(id);
            return "Ficha de orientacion eliminada correctamente";
        }
    }
}
