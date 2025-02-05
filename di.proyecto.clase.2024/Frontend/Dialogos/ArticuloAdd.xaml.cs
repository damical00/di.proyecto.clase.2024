using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
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
    /// Interaction logic for ArticuloAdd.xaml
    /// </summary>
    public partial class ArticuloAdd : Window
    {
        private Usuario usuarioLogin;
        private DiinventarioexamenContext contexto;
        private ModeloArticuloServicio modeloarticuloServicio;
        private ArticuloServicio articuloServicio;
        private UsuarioServicio usuarioServicio;
        private EspacioServicio espacioServicio;
        private DptoServicio dptoServicio;
        private List<String> listaEstados = new List<String>() { "Operativo","Retirado","Mantenimiento"};

        public ArticuloAdd(Backend.Modelo.DiinventarioexamenContext context, Usuario usu)
        {
            InitializeComponent();
            contexto = context;
            usuarioLogin = usu;
            _ = Inicializa();
        }

        public async void btnGuarar_Click(object sender, RoutedEventArgs e)
        {
            if (await articuloServicio.AddAsync(recogerDatosArticulo())) {
                MessageBox.Show("YA NO LLORA!!!!!");
                DialogResult = true;
            }
            else
            {
                 MessageBox.Show("LLORA MUUUCHO!!!!!");
                DialogResult = true;
            }
        }
        
        public async void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }



        private async Task Inicializa()
        {
            modeloarticuloServicio = new ModeloArticuloServicio(contexto);
            articuloServicio = new ArticuloServicio(contexto);
            usuarioServicio = new UsuarioServicio(contexto);
            espacioServicio= new EspacioServicio(contexto);
            dptoServicio = new DptoServicio(contexto);

            //Asignamos los valores de las tablas de la base de datos en los comboBox
            comboDepartamento.ItemsSource = await dptoServicio.GetAllAsync();
            espacio.ItemsSource=await espacioServicio.GetAllAsync();
            modelo.ItemsSource = await modeloarticuloServicio.GetAllAsync();
            usuarioAlta.ItemsSource = await usuarioServicio.GetAllAsync();
            comboArmario.ItemsSource = await articuloServicio.GetAllAsync();
            comboEstado.ItemsSource = listaEstados;
            fechaAlta.SelectedDate = DateTime.Now;
            usuarioAlta.SelectedItem = usuarioLogin;

        }
        private Articulo recogerDatosArticulo()
        {
            Articulo articulo = new Articulo();

            articulo.Idarticulo = articuloServicio.GetLastId() + 1; //Obtenemos el último ID y le sumamos 1, para que sea autoincrementable
            articulo.Fechaalta = fechaAlta.SelectedDate;
            articulo.ModeloNavigation = (Modeloarticulo)modelo.SelectedItem;
            articulo.DepartamentoNavigation = (Departamento)comboDepartamento.SelectedItem;
            articulo.DentrodeNavigation = (Articulo)comboArmario.SelectedItem;
            articulo.Numserie = numSerie.Text;
            articulo.Observaciones = txtObservaiones.Text;
            articulo.Estado = (string?)comboEstado.SelectedItem;
            articulo.UsuarioaltaNavigation =(Usuario) usuarioAlta.SelectedItem;
            articulo.EspacioNavigation = (Espacio)espacio.SelectedItem;


            return articulo;
        }
    }
}
