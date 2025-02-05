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
    /// Interaction logic for UCArticulo.xaml
    /// </summary>
    public partial class UCArticulo : UserControl
    {
        private MVArticulo _mvArt;
        private List<Predicate<Articulo>> criterios;
        private Predicate<Articulo> criteriosTipo;
        public UCArticulo()
        {
            InitializeComponent();
        }
        
        public UCArticulo(MVArticulo mv)
        {
         
            _mvArt = mv;
            DataContext = _mvArt;
            InitializeComponent();

        }
        private void mitemEditar_Click(object sender, RoutedEventArgs e)
        {
            _mvArt.articulo = (Articulo)dgArticulo.SelectedItem;
            AddArticuloMVVM diag = new AddArticuloMVVM(_mvArt);
            diag.ShowDialog();
            if (diag.DialogResult.Equals(true))
            {
                dgArticulo.Items.Refresh();
            }
        }

       

        private void botonFiltrar_Click(object sender, RoutedEventArgs e)
        {
            _mvArt.Filtrar();
        }
    }
}
