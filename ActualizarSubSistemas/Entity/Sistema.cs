using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public  class Sistema
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public List<SubSistema> subSistemas= new List<SubSistema>();
    }
}
