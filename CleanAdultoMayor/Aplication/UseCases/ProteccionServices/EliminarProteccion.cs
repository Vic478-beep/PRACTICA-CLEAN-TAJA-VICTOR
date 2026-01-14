using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Aplication.UseCases.ProteccionServices
{
    public class EliminarProteccion
    {
        private readonly IFichaProteccion _proteccionRepo;

        public EliminarProteccion(IFichaProteccion proteccionRepo)
        {
            _proteccionRepo = proteccionRepo;
        }
        public async Task<string> EjecutarAsync(Guid id)
        {
            await _proteccionRepo.Eliminar(id);
            return "Ficha de proteccion eliminada correctamente";
        }
    }
}
