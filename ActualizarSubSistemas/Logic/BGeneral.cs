using Common;
using Data;
using Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
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
            var equipo = generalData.EquipoListar().Where(x => x.Nombre == nombreEquipo && x.Cpu == idCpu).FirstOrDefault();
            if (equipo is not null)
            {
                equiposSubSistema = generalData.EquipoSubSistemaListar().Where(x => x.IdEquipo == equipo.Id && x.IdSubSistema == idSubSistema).First();
                equiposSubSistema.equipo = equipo;
                equiposSubSistema.subSistema = generalData.SubSistemaListar().Where(x => x.Id == idSubSistema).FirstOrDefault()!;

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

        public List<Sistema> SistemaListar()
        {
            return generalData.SistemaListar();
        }
    }
}
