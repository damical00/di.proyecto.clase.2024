using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.MVVM;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace di.proyecto.clase._2024.Frontend.ControlUsuario
{
    /// <summary>
    /// Clase que representa un control de usuario para gestionar usuarios.
    /// </summary>
    public partial class UCUsuario : UserControl
    {
        /// <summary>
        /// Instancia del ViewModel para la gestión de usuarios.
        /// </summary>
        private MVUsuario _mvUsu;

        /// <summary>
        /// Constructor por defecto que inicializa los componentes gráficos.
        /// </summary>
        public UCUsuario()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor que recibe un ViewModel de usuario y lo asocia al DataContext.
        /// </summary>
        /// <param name="mv">ViewModel de usuario</param>
        public UCUsuario(MVUsuario mv)
        {
            InitializeComponent();
            _mvUsu = mv;
            DataContext = _mvUsu;
        }

        /// <summary>
        /// Maneja el evento de clic en la opción de editar un usuario.
        /// Abre un diálogo para modificar los datos del usuario seleccionado.
        /// </summary>
        private void mitemEditar_Click(object sender, RoutedEventArgs e)
        {
            // Asigna el usuario seleccionado en el DataGrid al ViewModel
            _mvUsu.usuario = (Usuario)dgUsuario.SelectedItem;

            // Crea y muestra el diálogo de edición de usuario
            DialogoUsuarioMVVM diag = new DialogoUsuarioMVVM(_mvUsu);
            diag.ShowDialog();

            // Si el usuario confirma la edición, refresca la tabla
            if (diag.DialogResult.Equals(true))
            {
                dgUsuario.Items.Refresh();
            }
        }

        /// <summary>
        /// Maneja el evento de selección de un tipo de usuario en el ComboBox.
        /// </summary>
        /// <param name="sender">Objeto que genera el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void cbGrupos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _mvUsu.Filtrar();
        }

        private void txtNombreUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {
            _mvUsu.Filtrar();
        }
    }
}