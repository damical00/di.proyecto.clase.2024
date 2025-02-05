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
    /// Interaction logic for UCModeloArticulo.xaml
    /// </summary>
    public partial class UCModeloArticulo : UserControl
    {
        private MVModeloArticulo _mvModeloArticulo;
        private List<Predicate<Modeloarticulo>> criterios;
        private Predicate<Modeloarticulo> criterioTipo;
        private Tipoarticulo _tipoaticulo;

        public UCModeloArticulo()
        {
            InitializeComponent();
        } 
        
        public UCModeloArticulo(MVModeloArticulo mv)
        {
            InitializeComponent();
            _mvModeloArticulo = mv;
            DataContext = _mvModeloArticulo;
        }

        private void mitemEditar_Click(object sender, RoutedEventArgs e)
        {
            _mvModeloArticulo.modeloArticulo = (Modeloarticulo)dgModeloArticulo.SelectedItem;
            DialogoModeloArticuloMVVM diag = new DialogoModeloArticuloMVVM(_mvModeloArticulo);
            diag.ShowDialog();
            if (diag.DialogResult.Equals(true))
            {
                dgModeloArticulo.Items.Refresh();
            }
        }

     
       

        private void cbTipoArticulo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _mvModeloArticulo.Filtrar();
        }

        private void txtNombreModelo_TextChanged(object sender, TextChangedEventArgs e)
        {


            _mvModeloArticulo.Filtrar();
        }

        private void checkCantidad_Checked(object sender, RoutedEventArgs e)
        {
            // Activar el filtro por cantidad
            //_mvModeloArticulo.FiltrarPorCantidad = true;
        }

        private void checkCantidad_Unchecked(object sender, RoutedEventArgs e)
        {
            // Desactivar el filtro por cantidad
            //_mvModeloArticulo.FiltrarPorCantidad = false;
        }
    }
}
