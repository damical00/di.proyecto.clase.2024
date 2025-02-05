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

namespace di.proyecto.clase._2024.MVVM
{
    /// <summary>
    /// Interaction logic for AddArticuloMVVm.xaml
    /// </summary>
    public partial class AddArticuloMVVM : Window
    {
        private DiinventarioexamenContext contexto;
        private Usuario usuarioLogin;
        private ArticuloServicio articuloServicio;
        private MVArticulo mvArticulo;
        private DptoServicio dptoServicio;
        private ModeloArticuloServicio modeloArticuloServicio;
        private EspacioServicio espacioServicio;
        private UsuarioServicio usuarioServicio;
        private List<string> listaEstados = new List<string>() { "Operativo", "Retirado", "Mantenimiento" };

        public AddArticuloMVVM(MVArticulo mv)
        {
            InitializeComponent();
            this.mvArticulo = mv;
            DataContext = mvArticulo;
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvArticulo.OnErrorEvent));
            mvArticulo.btnGuardar = btnGuardar;
           // _ = Inicializa();

        }

        public AddArticuloMVVM(DiinventarioexamenContext context)
        {
            contexto = context;
        }

        /*
        private async Task Inicializa()
        {
            usuarioServicio = new UsuarioServicio(contexto);
            articuloServicio = new ArticuloServicio(contexto);
            modeloArticuloServicio = new ModeloArticuloServicio(contexto);
            espacioServicio = new EspacioServicio(contexto);
            dptoServicio = new DptoServicio(contexto);

            comboDepartamento.ItemsSource = await dptoServicio.GetAllAsync();
            espacioCombo.ItemsSource = await espacioServicio.GetAllAsync();
            modeloCombo.ItemsSource = await modeloArticuloServicio.GetAllAsync();
            usuarioAlta.ItemsSource = await usuarioServicio.GetAllAsync();
            comboArmario.ItemsSource = await articuloServicio.GetAllAsync();
            comboEstado.ItemsSource = listaEstados;

            //Muestra la fecha actual
            fechaAlta.SelectedDate = DateTime.Now;

            usuarioAlta.SelectedItem = usuarioLogin;
        }
        */
       


        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (await articuloServicio.AddAsync(recogerDatosArticulo()))
            {
                MessageBox.Show("GESTION DE ARTICULOS", "EL Articulo SE HA GUARDADO");
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Sergio", "Llora mucho");
            }

        }

        private Articulo recogerDatosArticulo()
        {
            Articulo articulo = new Articulo();

            articulo.Idarticulo = articuloServicio.GetLastId() + 1; //Obtenemos el último ID y le sumamos 1, para que sea autoincrementable
            articulo.Fechaalta = fechaAlta.SelectedDate;
            articulo.ModeloNavigation = (Modeloarticulo)modeloCombo.SelectedItem;
            articulo.DepartamentoNavigation = (Departamento)comboDepartamento.SelectedItem;
            articulo.DentrodeNavigation = (Articulo)comboArmario.SelectedItem;
            articulo.Numserie = numSerie.Text;
            articulo.Observaciones = txtObservaiones.Text;
            articulo.Estado = (string?)comboEstado.SelectedItem;
            articulo.UsuarioaltaNavigation = (Usuario)usuarioAlta.SelectedItem;
            articulo.EspacioNavigation = (Espacio)espacioCombo.SelectedItem;


            return articulo;
        }
        public async void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
