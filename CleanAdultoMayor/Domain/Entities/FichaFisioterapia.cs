using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FichaFisioterapia
    {
        [Key]
        public Guid CodFis { get; set; }
        public string FechaProgramacion { get; set; } = string.Empty;
        public int NumeroSesiones { get; set; }
        public string MotivoConsulta { get; set; } = string.Empty;
        public string EquiposEmpleados { get; set; } = string.Empty;
        public Guid IdAdulto { get; set; } /*Clave foranea*/
        public Adulto? Adulto { get; set; } /*Propiedad de navegacion*/
    }
}
