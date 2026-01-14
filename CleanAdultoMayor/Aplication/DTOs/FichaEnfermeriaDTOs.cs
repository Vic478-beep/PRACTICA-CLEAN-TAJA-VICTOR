using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Aplication.DTOs
{
    public class FichaEnfermeriaDTOs
    {
        public Guid CodEnf { get; set; }
        public string Temperatura { get; set; } = string.Empty;
        public string Tratamiento { get; set; } = string.Empty;
        public string PesoTalla { get; set; } = string.Empty;
        public Guid IdAdulto { get; set; } /*Clave foranea*/
        public Adulto? Adulto { get; set; } /*Propiedad de navegacion*/
    }
}
