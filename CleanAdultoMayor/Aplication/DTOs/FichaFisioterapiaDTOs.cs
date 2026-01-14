using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Aplication.DTOs
{
    public class FichaFisioterapiaDTOs
    {
        public Guid CodFis { get; set; }
        public string FechaProgramacion { get; set; } = string.Empty;
        public int NumeroSesiones { get; set; }
        public string MotivoConsulta { get; set; } = string.Empty;
        public string EquiposEmpleados { get; set; } = string.Empty;
        public Guid IdAdulto { get; set; } /*Clave foranea*/
        public Adulto? Adulto { get; set; } /*Propiedad de navegacion*/
    }
}
