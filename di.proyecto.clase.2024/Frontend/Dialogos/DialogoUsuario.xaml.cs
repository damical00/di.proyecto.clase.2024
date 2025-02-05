using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using di.proyecto.clase.ribbon._2023.Backend.Servicios;
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
    /// Interaction logic for DialogoUsuario.xaml
    /// </summary>
    public partial class DialogoUsuario : Window
    {

        private Usuario usuarioLogin;
        private DiinventarioexamenContext contexto;
        private ServicioGenerico<Tipousuario> tipoUsuServicio;
        private UsuarioServicio usuServicio;
        private RolServicio rolServicio;
        private DptoServicio dptoServicio;
        private GrupoServicio grupoServicio;
        

        public DialogoUsuario(DiinventarioexamenContext context, Usuario usu)
        {
            InitializeComponent();
            contexto = context;
            inicializa();
        }

        public void inicializa() {
        usuServicio = new UsuarioServicio(contexto);
        dptoServicio = new DptoServicio(contexto);
        rolServicio = new RolServicio(contexto);
        grupoServicio = new GrupoServicio(contexto);
        tipoUsuServicio = new ServicioGenerico<Tipousuario>(contexto);
        }

        private Usuario recogerDatos() { 
            Usuario usu = new Usuario();
            usu.Nombre = txtUsername.Text;
            
            
            return usu;
        }

        public async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (await usuServicio.AddAsync(recogerDatos()))
            {
                MessageBox.Show("YA NO LLORA!!!!!");
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("LLORA MUUUCHO!!!!!");
                DialogResult = true;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tipoUsu = ((Tipousuario)tipoUsuario.SelectedItem).Nombre;
            switch (tipoUsu)
            {
                case "Profesor":
                    Departamento.IsEnable = true; grupoServicio.IsEnable = false;
                    break;
                
                case "Alumno":
                    grupoServicio.IsEnable = true; Departamento.IsEnable = false;
                    break;
            }
        }
        public async void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
