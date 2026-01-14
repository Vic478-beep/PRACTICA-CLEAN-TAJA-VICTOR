using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FichaProteccion
    {
        [Key]
        public Guid CodPro { get; set; }
        public string FechaProteccion { get; set; } = string.Empty;
        public string TipoProteccion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public Guid IdAdulto { get; set; } /*Clave foranea*/
        public Adulto? Adulto { get; set; } /*Propiedad de navegacion*/
    }
}
