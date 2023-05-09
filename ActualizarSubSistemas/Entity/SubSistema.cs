using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class SubSistema
    {
        public int Id { get; set; }
        public string Link { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public bool Disponible { get; set; }
        public string Comentarios { get; set; } = null!;
        public int IdSistema { get; set; }
        public string Nombre { get; set; } = null!;
        public Sistema Sistema = new Sistema();

    }
}
