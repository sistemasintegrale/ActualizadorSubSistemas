using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class EquipoSubSistemaDTO
    {
        public int IdEquipo { get; set; }       
        public int IdSubSistema { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public bool Acceso { get; set; }
    }
}
