using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class EquipoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Cpu { get; set; } = null!;
        public string? UbicacionActualizador { get; set; }
        public string? NombreUsuario { get; set; }
    }
}
