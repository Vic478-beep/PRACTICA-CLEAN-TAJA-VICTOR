using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FichaEnfermeria
    {
        [Key]
        public Guid CodEnf { get; set; }
        public string PresionArterial { get; set; } = string.Empty;
        public int FrecuenciaCardiaca { get; set; }
        public int FrecuenciaRespiratoria { get; set; }
        public float Pulso { get; set; }
        public string Temperatura { get; set; } = string.Empty;
        public string Tratamiento { get; set; } = string.Empty;
        public string PesoTalla { get; set; } = string.Empty;
        public Guid IdAdulto { get; set; } /*Clave foranea*/
        public Adulto? Adulto { get; set; } /*Propiedad de navegacion*/
    }
}
