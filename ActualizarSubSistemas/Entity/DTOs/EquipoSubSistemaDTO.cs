using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class EquipoSubSistemaDTO
    {
  
        public int IdSistema { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public bool Acceso { get; set; }
        public string nombreSistema { get; set; } = null!;
    }
}
