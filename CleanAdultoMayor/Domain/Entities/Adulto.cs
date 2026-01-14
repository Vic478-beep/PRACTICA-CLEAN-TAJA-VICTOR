using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Adulto
    {
        public Guid Id { get; set; } 
        public string Nombres { get; set; } = string.Empty; 
        public string Apellidos { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string FechaNac { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string EstadoCivil { get; set; } = string.Empty;
        public string Discapacidad { get; set; } = string.Empty;
        public string Domicilio { get; set; } = string.Empty;
        public int Telefono { get; set; }
        public ICollection<Adulto>? Adultos { get; set; } = new List<Adulto>();
    }
}
