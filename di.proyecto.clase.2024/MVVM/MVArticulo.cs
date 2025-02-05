using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using di.proyecto.clase._2024.MVVM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace di.proyecto.clase._2024.MVVM
{
    // Clase MVArticulo que extiende de MVBaseCRUD para la gestión de artículos
    public partial class MVArticulo : MVBaseCRUD<Articulo>
    {
        // Lista de estados posibles para un artículo
        private List<String> estados = new List<string> { "operativo", "obsoleto", "mantenimiento" };

        // Contexto de base de datos
        private DiinventarioexamenContext contexto;

        // Servicios para interactuar con la base de datos
        private ArticuloServicio articuloServicio;
        private ModeloArticuloServicio modeloArticuloServicio;
        private DptoServicio dptoServicio;
        private UsuarioServicio usuarioServicio;
        private EspacioServicio espacioServicio;

        /// <summary>
        /// Lista auxiliar para obtener los tipos de artículos
        /// </summary>
        private IEnumerable<Tipoarticulo> _listatipos;

        /// <summary>
        /// Lista auxiliar para obtener los espacios de los artículos con soporte para filtrado
        /// </summary>
        private ListCollectionView _listaEspacios;

        /// <summary>
        /// Objeto que guarda el espacio seleccionado en el filtro
        /// </summary>
        private Espacio _espacio;

        /// <summary>
        /// Almacena el nombre ingresado en el filtro de búsqueda
        /// </summary>
        private DateTime _FechaInicial;
        private DateTime _FechaFinal;

        /// <summary>
        /// Lista de criterios de filtrado para la tabla
        /// </summary>
        private List<Predicate<Articulo>> criterios;

        /// <summary>
        /// Criterio de filtrado basado en el grupo del artículo
        /// </summary>
        private Predicate<Articulo> criterioEspacio;

        /// <summary>
        /// Criterio de filtrado basado en la fecha inicial del artículo
        /// </summary>
        private Predicate<Articulo> criterioFechaInicial;

        /// <summary>
        /// Criterio de filtrado basado en la fecha inicial del artículo
        /// </summary>
        private Predicate<Articulo> criterioFechaFinal;

        /// <summary>
        /// Predicado asociado a la propiedad de filtrado de la lista de articulos
        /// </summary>
        private Predicate<object> predicadoFiltro;

        // Variable para almacenar la fecha actual
        private DateTime fecha;


        private ListCollectionView _listaArticulos;


        // Instancia del artículo en edición o creación
        private Articulo _articulo;

        // Constructor que recibe el contexto de la base de datos
        public MVArticulo(DiinventarioexamenContext context)
        {
            contexto = context;
        }

        //******************** GETTERS Y SETTERS ********************

        // Propiedades que obtienen listas de diferentes entidades de la base de datos
        public IEnumerable<Modeloarticulo> listaModelos { get { return Task.Run(modeloArticuloServicio.GetAllAsync).Result; } }
        public IEnumerable<Usuario> listaUsuarios { get { return Task.Run(usuarioServicio.GetAllAsync).Result; } }
        public IEnumerable<Departamento> listaDepartamentos { get { return Task.Run(dptoServicio.GetAllAsync).Result; } }
        public IEnumerable<Espacio> listaEspacios { get { return Task.Run(espacioServicio.GetAllAsync).Result; } }
        public IEnumerable<Articulo> listaArmarios { get { return Task.Run(articuloServicio.GetAllAsync).Result; } }
        public IEnumerable<String> listaEstados { get { return estados; } }
        public DateTime fechaHoy { get { return fecha; } }

        

        public DateTime fechaInicial
        {
            get { return _FechaInicial; }
            set { _FechaInicial = value; OnPropertyChanged(nameof(fechaInicial)); }
        } 
        
        public DateTime fechaFinal
        {
            get { return _FechaFinal; }
            set { _FechaFinal = value; OnPropertyChanged(nameof(fechaFinal)); }
        }


        // Propiedad para manejar el artículo actual
        public Articulo articulo
        {
            get { return _articulo; }
            set { _articulo = value; OnPropertyChanged(nameof(articulo)); }
        }

        /// <summary>
        /// Espacio seleccionado en el filtro
        /// </summary>
        public Espacio espacioSeleccionado
        {
            get => _espacio;
            set
            {
                _espacio = value;
                OnPropertyChanged(nameof(espacioSeleccionado));
            }
        }

        // Lista de artículos obtenidos de la base de datos
        public ListCollectionView listaArticulosFiltro => _listaArticulos ;



        // Método asincrónico para inicializar servicios y valores
        public async Task Inicializa()
        {
            fecha = DateTime.Now; // Se almacena la fecha actual

            // Inicialización de los servicios con el contexto de la base de datos
            articuloServicio = new ArticuloServicio(contexto);
            servicio = articuloServicio;
            modeloArticuloServicio = new ModeloArticuloServicio(contexto);
            usuarioServicio = new UsuarioServicio(contexto);
            dptoServicio = new DptoServicio(contexto);
            espacioServicio = new EspacioServicio(contexto);



            _listaArticulos = new ListCollectionView((await articuloServicio.GetAllAsync()).ToList());

            _articulo = new Articulo(); // Se inicializa la instancia del artículo

            criterios = new List<Predicate<Articulo>>();
            predicadoFiltro = new Predicate<object>(FiltroCriterios);

            InicializaCriterios();
        }

        // Método asincrónico para guardar un artículo en la base de datos
        public async Task<bool> Guarda() { return await Add(articulo); }


        /// <summary>
        /// Aplica los criterios de filtrado a la lista de modelos
        /// </summary>
        public void Filtrar()
        {
           // AddCriterios();
          //  listaArticulosFiltro.Filter = predicadoFiltro;
        }

        /// <summary>
        /// Agrega criterios de filtrado en base al tipo y nombre del modelo
        /// </summary>
        public void AddCriterios()
        {

            if (espacioSeleccionado != null) { criterios.Add(criterioEspacio); }
            if (fechaInicial!= new DateTime()) { criterios.Add(criterioFechaInicial); }
            if (fechaFinal!= new DateTime()) { criterios.Add(criterioFechaFinal); }
        }

        /// <summary>
        /// Método que evalúa si un modelo de artículo cumple con los criterios de filtrado
        /// </summary>
        private bool FiltroCriterios(object item)
        {
            bool correcto = true;
            Articulo modelo = (Articulo)item;
            if (criterios != null)
            {
                correcto = criterios.TrueForAll(x => x(articulo));
            }
            return correcto;
        }

        /// <summary>
        /// Inicializa los criterios de filtrado para el tipo y nombre del artículo
        /// </summary>
        private void InicializaCriterios()
        {
            // ma representa un objeto Modeloarticulo
            criterioEspacio = new Predicate<Articulo>(ma => ma.EspacioNavigation != null && ma.EspacioNavigation.Equals(espacioSeleccionado));
            criterioFechaInicial = new Predicate<Articulo>(ma => ma.Fechaalta != null && ma.Fechaalta>fechaInicial);
            criterioFechaFinal = new Predicate<Articulo>(ma => ma.Fechabaja != null && ma.Fechabaja<fechaFinal);
        }

    }
}

