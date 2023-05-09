using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class GeneratePathSistema
    {
        public static string GeneratePath(int number)
        {
            string strConexion = string.Empty;
            switch (number)
            {
                case 1: //GREEN PERU
                    strConexion = "Green-Peru";
                    break;
                case 2: //GALY COMPANY
                    strConexion = "Galy-Company";
                    break;
                case 3: // MOTO TORQUE
                    strConexion = "Moto-Torque";
                    break;
                case 4: //NOVA GLASS
                    strConexion = "Nova-Glass";
                    break;
                case 5: //NOVA FLAT
                    strConexion = "Nova-Flat";
                    break;
                case 6: // NOVA MOTOS
                    strConexion = "Nova-Motos";
                    break;
                case 7: // CALZADOS JAGUAR
                    strConexion = "Calzados-Jaguar";
                    break;
            }
            return strConexion;
        }
    }
}
