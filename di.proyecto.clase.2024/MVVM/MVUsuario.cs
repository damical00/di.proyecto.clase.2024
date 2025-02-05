using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using di.proyecto.clase._2024.Frontend.ControlUsuario;
using di.proyecto.clase._2024.MVVM.Base;
using di.proyecto.clase.ribbon._2023.Backend.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace di.proyecto.clase._2024.MVVM
{
    public partial class MVUsuario: MVBaseCRUD<Usuario>
    {

        private DiinventarioexamenContext contexto;
        private UsuarioServicio usuarioServicio;
        private DptoServicio dptoServicio;
        private Usuario _usuario;
        private RolServicio rolServicio;
        private ListCollectionView _listaUsuarios;

        /// <summary>
        /// Objeto que guarda el grupo de artículo seleccionado en el filtro
        /// </summary>
        private GrupoServicio _grupoServicio;

        /// <summary>
        /// Almacena el nombre ingresado en el filtro de búsqueda
        /// </summary>
        private string _txtNombreUsuario;

        /// <summary>
        /// Lista de criterios de filtrado para la tabla
        /// </summary>
        private List<Predicate<Usuario>> criterios;

        /// <summary>
        /// Criterio de filtrado basado en el grupo del usuario
        /// </summary>
        private Predicate<Usuario> criterioGrupo;

        /// <summary>
        /// Lista auxiliar para obtener los grupos de los usuarios con soporte para filtrado
        /// </summary>
        private ListCollectionView _listaGrupos;

        /// <summary>
        /// Objeto que guarda el grupo seleccionado en el filtro
        /// </summary>
        private Grupo _grupoSelected;

        /// <summary>
        /// Criterio de filtrado basado en el nombre del Usuario
        /// </summary>
        private Predicate<Usuario> criterioNombre;

        /// <summary>
        /// Predicado asociado a la propiedad de filtrado de la lista de grupos
        /// </summary>
        private Predicate<object> predicadoFiltro;
        public MVUsuario(DiinventarioexamenContext context)
        {
            contexto = context;
            Inicializa();
        }


        public async Task Inicializa()
        {
            usuarioServicio = new UsuarioServicio(contexto);
            dptoServicio = new DptoServicio(contexto);
            rolServicio = new RolServicio(contexto);
            _usuario = new Usuario();

            _listaUsuarios = new ListCollectionView((await usuarioServicio.GetAllAsync()).ToList());

            _grupoServicio = new GrupoServicio(contexto);  // Agregar esta línea

            criterios = new List<Predicate<Usuario>>();  // Agregar la lista de criterios
            predicadoFiltro = new Predicate<object>(FiltroCriterios);  // Implementar el predicado

            InicializaCriterios();
        }


        public async Task<bool> Guarda() { return await Add(usuario); }

        /// <summary>
        /// ------------------GETTERS Y SETTERS------------------
        /// </summary>

        //Lista para mostrar de la base de datos
        public ListCollectionView listaUsuariosFiltro => _listaUsuarios;
        public IEnumerable<Rol> listaRoles { get { return Task.Run(rolServicio.GetAllAsync).Result; } }
        public IEnumerable<Departamento> listaDepartamentos { get { return Task.Run(dptoServicio.GetAllAsync).Result; } }
        
        public Usuario usuario 
        { 
            get { return _usuario; }
            set { _usuario = value; OnPropertyChanged(nameof(usuario)); } 
        }

        /// <summary>
        /// Lista de grupos filtrados
        /// </summary>
        public ListCollectionView listaGruposFiltro => _listaGrupos;
        /// <summary>
        /// Lista completa de grupos obtenida de la base de datos
        /// </summary>
        public IEnumerable<Grupo> listaGrupos => Task.Run(() => _grupoServicio.GetAllAsync()).Result;


        /// <summary>
        /// Grupo seleccionado en el filtro
        /// </summary>
        public Grupo grupoSeleccionado
        {
            get => _grupoSelected;
            set
            {
                _grupoSelected = value;
                OnPropertyChanged(nameof(grupoSeleccionado));
            }
        }

        /// <summary>
        /// Nombre del usuario introducido en el filtro
        /// </summary>
        public string txtNombreUsuario
        {
            get => _txtNombreUsuario;
            set
            {
                _txtNombreUsuario = value;
                OnPropertyChanged(nameof(txtNombreUsuario));
            }
        }

        /// <summary>
        /// Aplica los criterios de filtrado a la lista de usuarios
        /// </summary>
        public void Filtrar()
        {
            AddCriterios();
            listaUsuariosFiltro.Filter = predicadoFiltro;
        }

        /// <summary>
        /// Agrega criterios de filtrado en base al tipo y nombre del modelo
        /// </summary>
        public void AddCriterios()
        {
            criterios.Clear();
            if (grupoSeleccionado != null) { criterios.Add(criterioGrupo); }
            if (!string.IsNullOrEmpty(txtNombreUsuario)) { criterios.Add(criterioNombre); }
        }



        /// <summary>
        /// Método que evalúa si un usuario cumple con los criterios de filtrado
        /// </summary>
        private bool FiltroCriterios(object item)
        {
            if (item is not Usuario usuario || criterios == null || criterios.Count == 0)
                return true;  //Si no hay criterios, se muestra todo

            return criterios.TrueForAll(x => x(usuario));  //Aplicar filtros correctamente
        }



        /// <summary>
        /// Inicializa los criterios de filtrado para el grupo y nombre del usuario
        /// </summary>
        private void InicializaCriterios()
        {
            criterioGrupo = new Predicate<Usuario>(u => u.GrupoNavigation != null && u.GrupoNavigation.Equals(grupoSeleccionado));

            criterioNombre = new Predicate<Usuario>(u =>
                !string.IsNullOrEmpty(u.Nombre) &&
                u.Nombre.ToLower().StartsWith(txtNombreUsuario.ToLower()));

            
            criterios.Add(criterioGrupo);
            criterios.Add(criterioNombre);
        }


    }
}
