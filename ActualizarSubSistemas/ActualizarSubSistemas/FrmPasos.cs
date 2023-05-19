using Common;
using Data;
using Entity;
using Guna.UI2.WinForms;
using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Interop;
using static Guna.UI2.WinForms.Suite.Descriptions;

namespace ActualizarSubSistemas
{
    public partial class FrmPasos : Form
    {
        public Equipo equipo = new Equipo();
        public EquiposSubSistema equiposSubSistema = new EquiposSubSistema();
        public SubSistema subSistema = new SubSistema();
        string idCpu = string.Empty;
        string NombreEquipo = string.Empty;
        private readonly BGeneral bGeneral;
        Guna2MessageDialog msg = new Guna2MessageDialog();
        public string mensaje = string.Empty;
        public string pathCarpetaPrincipal = string.Empty;
        public string pathSistema = string.Empty;
        int indicador = 0;
        int instalado = 1;
        int actualizando = 2;
        string pathArchivoRar = string.Empty;
        private WebClient cliente= new WebClient();
        public bool Actualizacion= false;
        public FrmPasos()
        {
            InitializeComponent();
            bGeneral = new BGeneral();
            ObtenerNombreEquipo();
            idCpu = GetICPU.get(); 
            cliente.DownloadFileCompleted += new AsyncCompletedEventHandler(cargado);
            cliente.DownloadProgressChanged += new DownloadProgressChangedEventHandler(cargando);
        }
        private void cargando(object sender, DownloadProgressChangedEventArgs e)
        {
            if (indicador == instalado)
            {
                barInstall.Value = e.ProgressPercentage;
            }
            if (indicador == actualizando)
            {
                barActualizar.Value = e.ProgressPercentage;
            }

        }

        private void cargado(object sender, AsyncCompletedEventArgs e)
        {
            Ejecutar();
        }
        private String FormatoDoleComilal(string sRuta)
        {
            return Convert.ToChar(34).ToString() + sRuta + Convert.ToChar(34).ToString();
        }

        void Ejecutar()
        {
            try
            {
                Process procesoExtaccion = new Process();
                ProcessStartInfo informacionProcceso = new ProcessStartInfo();
                informacionProcceso.FileName = @"C:\Program Files\WinRAR\WinRAR.exe";
                
                informacionProcceso.Arguments = "x " + FormatoDoleComilal(pathArchivoRar) + " " + FormatoDoleComilal(pathSistema);
                procesoExtaccion.StartInfo = informacionProcceso;
                procesoExtaccion.Start();
                Thread.Sleep(5000);

                //MODIFICAMOS EN LA BASE DE DATOS
                equiposSubSistema.IdSubSistema = subSistema.Id;
                equiposSubSistema.FechaActualizacion = DateTime.Now;
                bGeneral.EquipoSubSistemaModificar(equiposSubSistema);
                if (indicador == instalado)
                {
                        Process.Start(new ProcessStartInfo { FileName = pathSistema + @"\SGE.WindowForms.application", UseShellExecute = true });
                        Application.Exit();
                  
                }
                else
                {
                    this.Hide();
                    Process.Start(new ProcessStartInfo { FileName = pathSistema + @"\SGE.WindowForms.application", UseShellExecute = true });
                    Application.Exit();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }

        public void ObtenerNombreEquipo()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = new IPHostEntry();
            ipEntry = Dns.GetHostEntry(strHostName);
            this.NombreEquipo = Convert.ToString(ipEntry.HostName);
        }
        private void FrmPasos_Load(object sender, EventArgs e)
        {
            if (Actualizacion)
            {
                ObtenerNombreEquipo();
                verificarEquipo();
            }
            if (!Actualizacion)
            {
                if (Constantes.conneciones.Count == 1) // CUANDO EL USUARIO SOLO PERTENESE A UNA EMPRESA
                {
                    Constantes.Connection = Constantes.conneciones.First();
                    SeleccionarSubSistema();
                }
                else// CUANDO EL USUARIO SOLO PERTENESE A MAS DE UNA EMPRESA
                {
                    SeleccionarSistema();
                }
            }
            
        }
        public void SeleccionarSistema()
        {
            //Cargamos Las Empresas
            var list = new List<Empresas>();
            Constantes.conneciones.ForEach(x =>
            {
                if (x == Constantes.ConnGrenPeru)
                { var obj1 = new Empresas(); obj1.Id = Constantes.ConnGrenPeru; obj1.Name = "GREEN PERU"; list.Add(obj1); }
                if (x == Constantes.ConnGalyCompany)
                { var obj2 = new Empresas(); obj2.Id = Constantes.ConnGalyCompany; obj2.Name = "GALY COMPANY"; list.Add(obj2); }
                if (x == Constantes.ConnMotoTorque)
                { var obj3 = new Empresas(); obj3.Id = Constantes.ConnMotoTorque; obj3.Name = "MOTO TORQUE"; list.Add(obj3); }
                if (x == Constantes.ConnNovaGlass)
                { var obj4 = new Empresas(); obj4.Id = Constantes.ConnNovaGlass; obj4.Name = "NOVA GLASS"; list.Add(obj4); }
                if (x == Constantes.ConnNovaFlat)
                { var obj5 = new Empresas(); obj5.Id = Constantes.ConnNovaFlat; obj5.Name = "NOVA FLAT"; list.Add(obj5); }
                if (x == Constantes.ConnNovaMotos)
                { var obj6 = new Empresas(); obj6.Id = Constantes.ConnNovaMotos; obj6.Name = "NOVA MOTOS"; list.Add(obj6); }
                if (x == Constantes.ConnCalzadosJaguar)
                { var obj7 = new Empresas(); obj7.Id = Constantes.ConnCalzadosJaguar; obj7.Name = "CALZADOS JAGUAR"; list.Add(obj7); }
            });

            BSControls.Guna2Combo(lkpEmpresa, list, "Name", "Id", true);
            tabControl.SelectedIndex = Constantes.tabSistema;
        }

        public void SeleccionarSubSistema()
        {
            //Cargamos los Sistemas
            var sistema = bGeneral.SistemaListar();
            BSControls.Guna2Combo(lkpSistema, sistema, "Nombre", "Id", true);
            tabControl.SelectedIndex = Constantes.tabSubsistema;
        }

        private void btnSeleccionarSubSistema_Click(object sender, EventArgs e)
        {
            Constantes.SubSistema = Convert.ToInt32(lkpSistema.SelectedValue);
            verificarEquipo();
        }

        private void btnSeleccionarSistema_Click(object sender, EventArgs e)
        {
            Constantes.Connection = Convert.ToInt32(lkpEmpresa.SelectedValue);
            SeleccionarSubSistema();
        }
        public void verificarEquipo()
        {
            //VERIFICAR SI EL EQUIPO ESTA REGISTRADO

            equiposSubSistema = bGeneral.Equipo_Obtner_Datos(NombreEquipo, idCpu, Constantes.SubSistema);
            if (equiposSubSistema.Acceso == false)
            {
                msg = new Guna2MessageDialog();
                msg.Caption = "Información del Sistema";
                msg.Text = $"Equipo sin permisos, reintentar?";
                msg.Buttons = MessageDialogButtons.OKCancel;
                msg.Style = MessageDialogStyle.Light;
                msg.Icon = MessageDialogIcon.Warning;
                msg.Parent = this;
                if (msg.Show() == DialogResult.OK)
                {
                    if (equiposSubSistema.equipo.Id == 0)
                    {
                        //INSERTAMOS EN LA BASE DE DATOS
                        equiposSubSistema.equipo.Nombre = NombreEquipo;
                        equiposSubSistema.equipo.Cpu = idCpu;
                        equiposSubSistema.IdSubSistema = Constantes.SubSistema;
                        bGeneral.Equipo_Ingresar(NombreEquipo, idCpu);
                    }

                    verificarEquipo();
                    return;
                }
                else
                {
                    if (equiposSubSistema.equipo.Id == 0)
                    {
                        //INSERTAMOS EN LA BASE DE DATOS
                        equiposSubSistema.equipo.Nombre = NombreEquipo;
                        equiposSubSistema.equipo.Cpu = idCpu;
                        equiposSubSistema.IdSubSistema = Constantes.SubSistema;
                        bGeneral.Equipo_Ingresar(NombreEquipo, idCpu);
                    }

                    Application.Exit();
                }
            }
            else
            {

                pathCarpetaPrincipal = @"C:\\Publish-" + GeneratePathSistema.GeneratePath(Constantes.Connection);
                pathSistema = pathCarpetaPrincipal + @"\" + equiposSubSistema.Sistema.Nombre;
                if (!Directory.Exists(pathCarpetaPrincipal))
                    Directory.CreateDirectory(pathCarpetaPrincipal);
                if (!Directory.Exists(pathSistema))
                {
                    Directory.CreateDirectory(pathSistema);
                    InstalarSubSistema();
                }
                else
                {
                    ActualizarSubSistema();
                }
            }
        }
        public void InstalarSubSistema()
        {
            tabControl.SelectedIndex = Constantes.tabInstalar;
            indicador = instalado;
            subSistema = bGeneral.SubSistemaListar().Where(x=>x.IdSistema == Constantes.SubSistema).ToList().OrderByDescending(x => x.Fecha).FirstOrDefault()!;
            pathArchivoRar = pathSistema + @"\" + subSistema.Nombre + ".zip";
            cliente.DownloadFileAsync(new Uri(subSistema.Link), pathArchivoRar);
        }

        public void ActualizarSubSistema()
        {
            tabControl.SelectedIndex = Constantes.tabActualizar;
            indicador = actualizando;
            subSistema = bGeneral.SubSistemaListar().OrderByDescending(x => x.Fecha).FirstOrDefault()!;
            if (subSistema.Id != 0 && subSistema.Id != equiposSubSistema.IdSubSistema)
            {
                //PRIMERO ELIMINAMOS LA VERSION ANTERIOR
                string pathAplicacion = pathSistema + @"\SGE.WindowForms.application";
                string pathFiles = pathSistema + @"\Application Files";
                string pathZip = pathSistema + @"\" + equiposSubSistema.subSistema.Sistema.Nombre + ".zip";
                string pathSetup = pathSistema + @"\setup.exe";
                if (Directory.Exists(pathFiles))
                    Directory.Delete(pathFiles, true);
                if (File.Exists(pathAplicacion))
                    File.Delete(pathAplicacion);
                if (File.Exists(pathZip))
                    File.Delete(pathZip);
                if (File.Exists(pathSetup))
                    File.Delete(pathSetup);
                //DESCARGAMOS DE DROPBOX
                pathArchivoRar = pathSistema + @"\" + subSistema.Nombre + ".zip";
                indicador = actualizando;
                cliente.DownloadFileAsync(new Uri(subSistema.Link), pathArchivoRar);
            }
            else
            {
                mensaje = "No se Encontraron Actualizaciones";
                msg = new Guna2MessageDialog();
                msg.Caption = "Información del Sistema";
                msg.Text = mensaje;
                msg.Buttons = MessageDialogButtons.OK;
                msg.Style = MessageDialogStyle.Light;
                msg.Icon = MessageDialogIcon.Error;
                msg.Parent = this;
                if (msg.Show() == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }
    }
}
