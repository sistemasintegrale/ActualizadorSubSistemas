﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public  class Conneccion
    {
        public static string GetConexion()
        {
            string strConexion = string.Empty;
            int number = (Int32)Constantes.Connection;
            switch (number)
            {
                case 1: //GREEN PERU
                    strConexion = "Data Source=tcp:95.217.194.247,1433;Initial Catalog=sistemasintegrales_com_GP;Persist Security Info=True;User ID=sistemasintegrales_com_de;Password=rogola2012;MultipleActiveResultSets = True;TrustServerCertificate = False;Encrypt = False";
                    break;
                case 2: //GALY COMPANY
                    strConexion = "Data Source=tcp:95.217.194.247,1433;Initial Catalog=telaslima_com_GC;Persist Security Info=True;User ID=sistemasintegrales_com_de;Password=rogola2012;MultipleActiveResultSets = True; TrustServerCertificate = False; Encrypt = False";
                    break;
                case 3: // MOTO TORQUE
                    strConexion = "Data Source=tcp:95.217.194.247,1433;Initial Catalog=sistemasintegrales_com_MT;Persist Security Info=True;User ID=sistemasintegrales_com_de;Password=rogola2012;MultipleActiveResultSets = True; TrustServerCertificate = False; Encrypt = False";
                    break;
                case 4: //NOVA GLASS
                    strConexion = "Server=novaglass.database.windows.net,1433;Initial Catalog=SGE_NOVAGLASS;Persist Security Info=False;User ID=adminnova;Password = Novaazure$9; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30; ";
                    break;
                case 5: //NOVA FLAT
                    strConexion = "Server=novaglass.database.windows.net,1433;Initial Catalog=SGE_NOVAFLAT;Persist Security Info=False;User ID=adminnova;Password = Novaazure$9; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30; ";
                    break;
                case 6: // NOVA MOTOS
                    strConexion = "Server=novamotorssql.database.windows.net,1433;Initial Catalog=SGE_NOVAMOTOS;Persist Security Info=False;User ID=adminsql;Password = novamotors$9; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30; ";
                    break;
                case 7: //CALZADOS JAGUAR
                    strConexion = "Data Source=tcp:95.217.194.247,1433;Initial Catalog=jaguar_com_pe_CJ;Persist Security Info=True;User ID=jaguar_com_pe_CJ;Password=eY-68j#0D;MultipleActiveResultSets = True; TrustServerCertificate = False; Encrypt = False";

                    break;
            }
            return strConexion;
        }
    }
}
