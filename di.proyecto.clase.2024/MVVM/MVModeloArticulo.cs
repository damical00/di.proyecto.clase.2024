using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using di.proyecto.clase._2024.MVVM.Base;
using System.Windows.Data;

namespace di.proyecto.clase._2024.MVVM
{
    public partial class MVModeloArticulo : MVBaseCRUD<Modeloarticulo>
    {
        // VARIABLES PRIVADAS ***********************************************
       /// <summary>
       /// Contexto de Entity Framework para interactuar con la base de datos
       /// </summary>
        private DiinventarioexamenContext contexto;

        /// <summary>
        /// Servicio para acceder a la tabla ModeloArticulo
        /// </summary>
        private ModeloArticuloServicio modeloServicio;


        /// <summary>
        /// Servicio para acceder a la tabla TipoArticulo
        /// </summary>
        private TipoArticuloServicio tipoArticuloServicio;

        /// <summary>
        /// Variable auxiliar para almacenar los valores del diálogo Insertar/Editar
        /// </summary>
        private Modeloarticulo modelo;

        /// <summary>
        /// Lista auxiliar para obtener los tipos de artículos
        /// </summary>
        private IEnumerable<Tipoarticulo> _listatipos;

        /// <summary>
        /// Lista auxiliar para obtener los modelos de los artículos con soporte para filtrado
        /// </summary>
        private ListCollectionView _listaModelos;

        /// <summary>
        /// Objeto que guarda el tipo de artículo seleccionado en el filtro
        /// </summary>
        private Tipoarticulo _tipoaticulo;

        /// <summary>
        /// Variable que almacena si el chack está activo o no
        /// </summary>
        private bool checkAtivo;


        /// <summary>
        /// Almacena el nombre ingresado en el filtro de búsqueda
        /// </summary>
        private string _txtNombreModelo;

        /// <summary>
        /// Lista de criterios de filtrado para la tabla
        /// </summary>
        private List<Predicate<Modeloarticulo>> criterios;
  
        /// <summary>
        /// Criterio de filtrado basado en el tipo de artículo
        /// </summary>
        private Predicate<Modeloarticulo> criterioTipo;
        
        /// <summary>
        /// Criterio de filtrado basado en el nombre del artículo
        /// </summary>
        private Predicate<Modeloarticulo> criterioNombre;
        
        /// <summary>
        /// Predicado asociado a la propiedad de filtrado de la lista de modelos
        /// </summary>
        private Predicate<object> predicadoFiltro;

        // SETTERS Y GETTERS PUBLICOS ***************************************
        
        /// <summary>
        /// Modelo de artículo actual
        /// </summary>
        public Modeloarticulo modeloArticulo
        {
            get { return modelo; }
            set { modelo = value; OnPropertyChanged(nameof(modeloArticulo)); }
        }

        /// <summary>
        /// Lista de tipos de artículos obtenida de la base de datos
        /// </summary>
        public IEnumerable<Tipoarticulo> listaTipos { get { return Task.Run(tipoArticuloServicio.GetAllAsync).Result; } }

        /// <summary>
        /// Lista de modelos filtrados
        /// </summary>
        public ListCollectionView listaModelosFiltro => _listaModelos;

        /// <summary>
        /// Lista completa de modelos de artículos obtenida de la base de datos
        /// </summary>
        public IEnumerable<Modeloarticulo> listaModelos => Task.Run(() => modeloServicio.GetAllAsync()).Result;

        /// <summary>
        /// Tipo de artículo seleccionado en el filtro
        /// </summary>
        public Tipoarticulo tipoSeleccionado
        {
            get => _tipoaticulo;
            set 
            { 
                _tipoaticulo = value;
                OnPropertyChanged(nameof(tipoSeleccionado));
            }
        }

        /// <summary>
        /// Nombre del modelo de artículo introducido en el filtro
        /// </summary>
        public string txtNombreModelo
        {
            get => _txtNombreModelo;
            set
            {
                _txtNombreModelo = value;
                OnPropertyChanged(nameof(txtNombreModelo));
            }
        }

        /// <summary>
        /// Guarda los cambios en el modelo de artículo
        /// </summary>
        public async Task<bool> Guarda() 
        {
            return await Update(modeloArticulo);
        }




        /// <summary>
        /// Constructor de la clase que inicializa el contexto
        /// </summary>
        public MVModeloArticulo(DiinventarioexamenContext context)
        {
            contexto = context;
        }


        /// <summary>
        /// Inicializa los servicios, listas y criterios de filtrado
        /// </summary>
        public async Task Inicializa()
        {
            modeloServicio = new ModeloArticuloServicio(contexto);
            servicio = modeloServicio;
            tipoArticuloServicio = new TipoArticuloServicio(contexto);
            modelo = new Modeloarticulo();

            _listaModelos = new ListCollectionView((await modeloServicio.GetAllAsync()).ToList());

            criterios = new List<Predicate<Modeloarticulo>>();
            predicadoFiltro = new Predicate<object>(FiltroCriterios);

            InicializaCriterios();
        }



        /// <summary>
        /// Aplica los criterios de filtrado a la lista de modelos
        /// </summary>
        public void Filtrar() 
        {
            AddCriterios();
            listaModelosFiltro.Filter = predicadoFiltro; 
        }

        

        /// <summary>
        /// Agrega criterios de filtrado en base al tipo y nombre del modelo
        /// </summary>
        public void AddCriterios()
        {
            criterios.Clear();
            if (tipoSeleccionado != null) { criterios.Add(criterioTipo); }
            if (txtNombreModelo != null) { criterios.Add(criterioNombre); }
        }

        /// <summary>
        /// Método que evalúa si un modelo de artículo cumple con los criterios de filtrado
        /// </summary>
        private bool FiltroCriterios(object item)
        {
            bool correcto = true;
            Modeloarticulo modelo = (Modeloarticulo)item;
            if (criterios != null)
            {
                correcto = criterios.TrueForAll(x => x(modelo));
            }
            return correcto;
        }

        /// <summary>
        /// Inicializa los criterios de filtrado para el tipo y nombre del artículo
        /// </summary>
        private void InicializaCriterios()
        {
            // ma representa un objeto Modeloarticulo
            criterioTipo = new Predicate<Modeloarticulo>(ma => ma.TipoNavigation != null && ma.TipoNavigation.Equals(tipoSeleccionado));
            criterioNombre = new Predicate<Modeloarticulo>(ma => ma.Nombre != null && ma.Nombre.ToLower().StartsWith(txtNombreModelo.ToLower()));
        }
    }
}
