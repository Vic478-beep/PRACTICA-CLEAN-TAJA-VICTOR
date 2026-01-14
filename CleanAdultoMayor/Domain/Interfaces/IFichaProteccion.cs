using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFichaProteccion
    {
        /* REGLAS DE NEGOCIO */
        Task<IEnumerable<FichaProteccion>> All();
        Task<FichaProteccion> ObtenerId(Guid id);
        Task Crear(FichaProteccion ficha);
        Task Actualizar(FichaProteccion ficha);
        Task Eliminar(Guid id);
    }
}
