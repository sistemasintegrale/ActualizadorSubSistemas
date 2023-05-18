using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EquiposSubSistema
    {
        public int IdEquipo { get; set; }
        public Equipo equipo = new Equipo();
        public int IdSubSistema { get; set; }
        public SubSistema subSistema = new SubSistema();
        public DateTime FechaActualizacion { get; set; }
        public bool Acceso { get; set; }
        public int IdSistema { get; set; }
        public Sistema Sistema  = new Sistema();
    }
}
