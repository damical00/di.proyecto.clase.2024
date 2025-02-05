using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Frontend.ControlUsuario;
using di.proyecto.clase._2024.Frontend.Dialogos;
using di.proyecto.clase._2024.MVVM;
using MahApps.Metro.Controls;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace di.proyecto.clase._2024
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : MetroWindow
    {
        private DiinventarioexamenContext contexto;
        private Usuario usuarioLogin;
        private MVModeloArticulo mvModeloArt;
        private MVArticulo mvArt;
        private MVUsuario mvUsu;
        public MainWindow(DiinventarioexamenContext context, Usuario usu)
        {
            InitializeComponent();
            contexto = context;
            usuarioLogin = usu;
            UserLogged.Text = usu.Username;
            _ = Inicializa();
          
        }

        private async Task Inicializa() {
            mvModeloArt = new MVModeloArticulo(contexto);
            await mvModeloArt.Inicializa();
            mvUsu = new MVUsuario(contexto);
            await mvUsu.Inicializa();
            mvArt = new MVArticulo(contexto);
            await mvArt.Inicializa();
        }


        //----MODELO ARTICULO---------
        private void btnAddModelo_Click(object sender, RoutedEventArgs e)
        {
            DialogoModeloArticuloMVVM diagModulo = new DialogoModeloArticuloMVVM(mvModeloArt);
            diagModulo.ShowDialog();
        }
        private void btnListadoModelos(object sender, RoutedEventArgs e)
        {
            UCModeloArticulo ucMA = new UCModeloArticulo(mvModeloArt);
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(ucMA);
        }


        //----ARTICULO---------
        private void btnAddArticulo_Click(object sender, RoutedEventArgs e)
        {
            AddArticuloMVVM addArticuloMVVM = new AddArticuloMVVM(mvArt);
            addArticuloMVVM.ShowDialog();
        }
        private void btnListadoArticulos(object sender, RoutedEventArgs e)
        {
            UCArticulo ucArt = new UCArticulo(mvArt);
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(ucArt);
        }

        //----USUARIO---------
        private void btnAddUsuario_Click(object sender, RoutedEventArgs e)
        {
            DialogoUsuarioMVVM diagUsuario = new DialogoUsuarioMVVM(mvUsu);
            diagUsuario.ShowDialog();
        }

        private void btnListadoUsuarios(object sender, RoutedEventArgs e)
        {
            UCUsuario ucUsu = new UCUsuario(mvUsu);
            panelPrincipal.Children.Clear();
            panelPrincipal.Children.Add(ucUsu);
        }


        //------------------------------------------------
        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Login inicio = new Login();
            this.Close();
            inicio.Show();
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
                MessageBox.Show("CONEXIÓN CON LA BASE DE DATOS", "UPS !!!! No podemos con la BD. Contacte");
            }
            return correcto;
        } 
    }
}