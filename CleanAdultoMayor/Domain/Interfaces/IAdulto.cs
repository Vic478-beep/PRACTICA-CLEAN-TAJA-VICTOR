using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAdulto
    {
        Task<IEnumerable<Adulto>> All();
        Task<Adulto> ObtenerId(Guid id);
        Task Crear(Adulto adulto);
        Task Actualizar(Adulto adulto);
        Task Eliminar (Guid id);
    }
}
