using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using di.proyecto.clase._2024.MVVM;
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
    /// Interaction logic for DialogoModeloArticuloMVC.xaml
    /// </summary>
    public partial class DialogoModeloArticuloMVC : Window
    {

        private DiinventarioexamenContext contexto;
        private ModeloArticuloServicio modeloarticuloServicio;
        private TipoArticuloServicio tipoArticuloServicio;

        public DialogoModeloArticuloMVC(MVModeloArticulo mv)
        {
            InitializeComponent();
           
        }

        

        public DialogoModeloArticuloMVC(DiinventarioexamenContext context)
        {
            InitializeComponent();
            contexto = context;
            Inicializa();
        }

        private async void Inicializa()
        {
            modeloarticuloServicio = new ModeloArticuloServicio(contexto);
            tipoArticuloServicio = new TipoArticuloServicio(contexto);
            cbTipoArticulo.ItemsSource=await tipoArticuloServicio.GetAllAsync();
        }

        private Modeloarticulo recogerDatos()
        {
            Modeloarticulo modelo = new Modeloarticulo();

            modelo.Nombre = txtNombreModelo.Text;
            modelo.Modelo = txtModelo.Text;
            modelo.Marca = txtmarcaModelo.Text;
            modelo.Descripcion = txtDescipcionModelo.Text;

            modelo.TipoNavigation = (Tipoarticulo) cbTipoArticulo.SelectedItem;

           //modelo.Tipo = ((Tipoarticulo)cbTipoArticulo.SelectedItem).Idtipoarticulo;

            return modelo;
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e) {
            if (await modeloarticuloServicio.AddAsync(recogerDatos()))

            {
                MessageBox.Show("GESTION DE ARTICULOS", "EL MODELO SE HA GUARDADO CORRECTAMENTE");
                DialogResult = true;
            }
                else
                {
                    MessageBox.Show("GESTION DE ARTICULOS", "ERROR!!! PROBLEMAS AL INSERTAR EL OBJETO");
                DialogResult = false;
                }
            
        }

    }
}
