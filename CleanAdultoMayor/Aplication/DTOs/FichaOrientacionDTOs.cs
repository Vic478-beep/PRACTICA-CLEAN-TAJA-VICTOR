using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Aplication.DTOs
{
    public class FichaOrientacionDTOs
    {
        public Guid CodOri { get; set; }
        public string FechaOrientacion { get; set; } = string.Empty;
        public string TipoOrientacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public Guid IdAdulto { get; set; } /*Clave foranea*/
        public Adulto? Adulto { get; set; } /*Propiedad de navegacion*/
    }
}
