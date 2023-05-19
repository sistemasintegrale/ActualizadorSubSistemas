using Common;
using Data;
using Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class BGeneral
    {
        private readonly GeneralData generalData;

        public BGeneral()
        {
            this.generalData = new GeneralData();
        }

        public EquiposSubSistema Equipo_Obtner_Datos(string nombreEquipo, string idCpu, int idSubSistema)
        {
            EquiposSubSistema equiposSubSistema = new EquiposSubSistema();
            var equipo = new GeneralData().EquipoListar().Where(x => x.Nombre == nombreEquipo && x.Cpu == idCpu).FirstOrDefault();
            if (equipo != null)
            {
                equiposSubSistema = new GeneralData().EquipoSubSistemaListar().Where(x => x.IdEquipo == equipo.Id && x.IdSistema == idSubSistema).FirstOrDefault();
                equiposSubSistema.Sistema = new GeneralData().SistemaListar().Where(x => x.Id == idSubSistema).FirstOrDefault();
                equiposSubSistema.equipo = equipo;
            }

            return equiposSubSistema;
        }

        public void InsertarEquipoSubSistema(EquiposSubSistema equiposSubSistema)
        {
            equiposSubSistema.equipo.Id = EquipoIngresar(equiposSubSistema.equipo);
            equiposSubSistema.IdEquipo = equiposSubSistema.equipo.Id;
            EquipoSubSistemaIngresar(equiposSubSistema);

        }

        private void EquipoSubSistemaIngresar(EquiposSubSistema equiposSubSistema)
        {
            generalData.EquipoSubSistemaIngresar(equiposSubSistema);
        }

        private int EquipoIngresar(Equipo equipo)
        {
            return generalData.EquipoIngresar(equipo);
        }

        public List<Usuario> listarUsuarios()
        {
            return generalData.listarUsuarios();
        }

        public int User_Verification(string usua_usuario, string usua_pass, List<Usuario> usuarios)
        {
            int result;
            // 0 datos correctos
            // 1 password incorrecto
            // 2 usuario incorrecto
            var usua_flag = usuarios.FindIndex(x => x.usua_codigo_usuario == usua_usuario);

            if (usua_flag >= 0)
            {
                if (usuarios[usua_flag].usua_password_usuario == CoDec.codec(usua_pass))
                {

                    result = 0;
                }
                else
                {
                    result = 1;
                }
            }
            else
            {
                result = 2;


            }

            return result;
        }

        public List<SubSistema> SubSistemaListar()
        {
            var lista = generalData.SubSistemaListar();

            return lista;
        }

        public void EquipoSubSistemaModificar(EquiposSubSistema equiposSubSistema)
        {
            generalData.EquipoSubSistemaModificar(equiposSubSistema);
        }

        public void EquipoSubSistemaModificarDarAcceso(EquiposSubSistema equiposSubSistema)
        {
            generalData.EquipoSubSistemaModificarDarAcceso(equiposSubSistema);
        }

        public List<Sistema> SistemaListar()
        {
            return generalData.SistemaListar();
        }

        public List<Equipo> EquipoListar()
        {
            return generalData.EquipoListar();
        }

        public void SistemaIngresar(Sistema sistema)
        {
            generalData.SistemaIngresar(sistema);
        }

        public int SubSistemaIngresar(SubSistema subSistema)
        {
            return generalData.SubSistemaIngresar(subSistema);
        }

        public void SubSistemaModificar(SubSistema subSistema)
        {
            generalData.SubSistemaModificar(subSistema);
        }

        public void SistemaModificar(Sistema sistema)
        {
            generalData.SistemaModificar(sistema);
        }

        public void Equipo_Ingresar(string nombreEquipo, string idCpu)
        {
            var equipo = new Equipo()
            {
                Nombre = nombreEquipo,
                Cpu = idCpu,
                UbicacionActualizador = string.Empty,
                Id = 0,
                NombreUsuario = string.Empty
            };
            equipo.Id = generalData.EquipoIngresar(equipo);
            var equipoSubSistema = new EquiposSubSistema()
            {
                IdEquipo = equipo.Id,
                IdSubSistema = Constantes.SubSistema,//Punto de Venta
                FechaActualizacion = DateTime.Now,
                Acceso = false
            };
            generalData.EquipoSubSistemaIngresar(equipoSubSistema);
        }

        public List<EquiposSubSistema> EquipoSubSistemaListar()
        {            
            var lista = generalData.EquipoSubSistemaListar();
            return lista;
        }

        public void EquipoModificar(Equipo equipo)
        {
            generalData.EquipoModificar(equipo);
        }
    }
}
