using Common;
using Data;
using Entity;
using Guna.UI2.WinForms;
using Logic;
using System.Net;
using System.Windows.Forms;
using System.Windows.Interop;

namespace ActualizarSubSistemas
{
    public partial class FrmLogin : Form
    {
        public List<Usuario> usuarios = new List<Usuario>();
        private readonly BGeneral bGeneral;
        Guna2MessageDialog msg = new Guna2MessageDialog();
        public string mensaje = string.Empty;
        public FrmLogin()
        {
            InitializeComponent();
            bGeneral = new BGeneral();
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {

            if (!VerificarSiEsUnaActualizacion())
            {
                for (int i = 1; i <= Constantes.TotalConecciones; i++)
                {
                    Constantes.Connection = i;
                    var lista = bGeneral.listarUsuarios();
                    lista.ForEach((data) =>
                    {
                        data.connection = i;

                    });
                    usuarios.AddRange(lista);
                }
            }
            else
            {
                this.Hide();
                FrmPasos frm = new FrmPasos();
                frm.Actualizacion = true;
                frm.ShowDialog();

            }

        }

        public bool VerificarSiEsUnaActualizacion()
        {
            if (File.Exists("C:\\SGIUSER\\userUpdate.txt"))
            {
                string[] valores = LeerDatos("C:\\SGIUSER\\userUpdate.txt");
                Constantes.Connection = Convert.ToInt32(valores[2]);
                Constantes.SubSistema = Convert.ToInt32(valores[3]);
                File.Delete("C:\\SGIUSER\\userUpdate.txt");

                return true;
            }
            return false;
        }

        string[] LeerDatos(string ruta)
        {
            string Linea;
            string[] Valores = null!;
            if (File.Exists(ruta))
            {
                using (StreamReader lector = new StreamReader(ruta))
                {
                    Linea = lector.ReadLine()!;
                    Valores = Linea.Split(",".ToCharArray());
                }
            }
            return Valores;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int resul = bGeneral.User_Verification(txtUsuario.Text, txtContraseña.Text, usuarios);
            if (resul == 2)
            {
                mensaje = "Nombre de usuario no existe";
                msg = new Guna2MessageDialog();
                msg.Caption = "Información del Sistema";
                msg.Text = mensaje;
                msg.Buttons = MessageDialogButtons.OK;
                msg.Style = MessageDialogStyle.Light;
                msg.Icon = MessageDialogIcon.Error;
                msg.Parent = this;
                msg.Show();
                return;
            }
            if (resul == 1)
            {
                mensaje = "Contraseña Incorrecta";
                msg = new Guna2MessageDialog();
                msg.Caption = "Información del Sistema";
                msg.Text = mensaje;
                msg.Buttons = MessageDialogButtons.OK;
                msg.Style = MessageDialogStyle.Light;
                msg.Icon = MessageDialogIcon.Error;
                msg.Parent = this;
                return;
            }

            ObtenerConecciones();
            this.Hide();
            FrmPasos frm = new FrmPasos();
            frm.ShowDialog();
        }
        public void ObtenerConecciones()
        {
            usuarios.Where(x => x.usua_codigo_usuario == txtUsuario.Text && x.usua_password_usuario == CoDec.codec(txtContraseña.Text)).ToList().ForEach(data =>
            {
                Constantes.conneciones.Add(data.connection);
            });

        }



    }
}