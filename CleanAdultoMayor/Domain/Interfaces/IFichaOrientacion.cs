using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFichaOrientacion
    {
        /* REGLAS DE NEGOCIO */
        Task<IEnumerable<FichaOrientacion>> All();
        Task<FichaOrientacion> ObtenerId(Guid id);
        Task Crear(FichaOrientacion ficha);
        Task Actualizar(FichaOrientacion ficha);
        Task Eliminar (Guid id);
    }
}
