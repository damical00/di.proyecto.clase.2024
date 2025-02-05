using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace di.proyecto.clase._2024.Frontend.Dialogos
{
    

    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private DiinventarioexamenContext contexto;
        private UsuarioServicio usuarioServicio;
        public Login()
        {
            if (ConectarBD())
            {
                InitializeComponent();
                usuarioServicio = new UsuarioServicio(contexto);
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (await usuarioServicio.Login(txtUsername.Text, passClaveAcceso.Password))
            {
                MainWindow ventanaPrincipal = new MainWindow(contexto, usuarioServicio.usuLogin);
                ventanaPrincipal.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("El usuario y/o contraseña no son correctos", "INICIO DE SESION");
            }
        }

        private bool ConectarBD()
        {
            bool correcto = true;
            contexto = new DiinventarioexamenContext();
            try
            {
                contexto.Database.OpenConnection();
            }
            catch (Exception ex)
            {
                correcto = false;
                MessageBox.Show("UPS !!!! No podemos con la BD. Contacte", "CONEXIÓN CON LA BASE DE DATOS");
            }
            return correcto;
        }

       
    }
}
