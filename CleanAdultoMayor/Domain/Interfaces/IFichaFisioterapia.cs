using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFichaFisioterapia
    {
        /* REGLAS DE NEGOCIO */
        Task<IEnumerable<FichaFisioterapia>> All();
        Task<FichaFisioterapia> ObtenerId(Guid id);
        Task Crear(FichaFisioterapia ficha);
        Task Actualizar(FichaFisioterapia ficha);
        Task Eliminar(Guid id);
    }
}
