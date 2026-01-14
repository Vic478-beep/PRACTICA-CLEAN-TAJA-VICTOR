using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class AdultoDTOs
    {
        public Guid Id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string FechaNac { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string EstadoCivil { get; set; } = string.Empty;
        public string Domicilio { get; set; } = string.Empty;
        public int Telefono { get; set; }
    }
}
