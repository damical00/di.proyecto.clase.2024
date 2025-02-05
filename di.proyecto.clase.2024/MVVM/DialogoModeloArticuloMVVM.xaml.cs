using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using di.proyecto.clase._2024.Backend.Utiles;
using di.proyecto.clase._2024.Frontend.ControlUsuario;
using di.proyecto.clase._2024.MVVM.Base;
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

namespace di.proyecto.clase._2024.MVVM
{
    /// <summary>
    /// Interaction logic for DialogoModeloArticuloMVVM.xaml
    /// </summary>
    public partial class DialogoModeloArticuloMVVM : Window
    {

        private DiinventarioexamenContext contexto;
        private ModeloArticuloServicio modeloServicio;
        private TipoArticuloServicio tipoArticuloServicio;
        private Modeloarticulo modeloarticulo;
        private MVModeloArticulo mvModelo;
        private Modeloarticulo modeloAux;

        public DialogoModeloArticuloMVVM(MVModeloArticulo mv)
        {
            InitializeComponent();
            this.mvModelo = mv;
            DataContext = mvModelo;
            modeloAux = new Modeloarticulo();
            PropertyCopier<Modeloarticulo>.CopyProperties(mvModelo.modeloArticulo, modeloAux);
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvModelo.OnErrorEvent));
            mvModelo.btnGuardar = btnGuardar;
        }

        public DialogoModeloArticuloMVVM(DiinventarioexamenContext context)
        {
            context = contexto;
        }

        public async void btnGuardar_Click (object sender, RoutedEventArgs e)
        {
            if (mvModelo.IsValid(this))
            {
                if (await mvModelo.Guarda())
                {
                    MessageBox.Show("GESTION MODELO ARTICULO", "EL MODELO ARTICULO" + mvModelo.modeloArticulo.Nombre+ " SE HA GUARDADO CORRECTAMENTE");
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("GESTION MODELO ARTICULO", "ERROR PROBLEMAS CON LA BASE DE DATOS, LLAME A ALGUIEN QUE SEPA");
                }
            }
            else { 
                MessageBox.Show("DA", "Error al insertar. Faltan campos por rellenar"); 
            }
        }

        public void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            PropertyCopier<Modeloarticulo>.CopyProperties(modeloAux,mvModelo.modeloArticulo);
            DialogResult = false;
        }

    }
}
