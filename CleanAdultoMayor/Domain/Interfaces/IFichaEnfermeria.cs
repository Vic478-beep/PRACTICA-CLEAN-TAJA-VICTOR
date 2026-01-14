using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFichaEnfermeria
    {
        /* REGLAS DE NEGOCIO */
        Task<IEnumerable<FichaEnfermeria>> All();
        Task<FichaEnfermeria> ObtenerId(Guid CodEnf);
        Task Crear(FichaEnfermeria ficha);
        Task Actualizar(FichaEnfermeria ficha);
        Task Eliminar (Guid CodEnf);
    }
}
