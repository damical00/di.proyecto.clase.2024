using di.proyecto.clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.MVVM;

namespace di.proyecto.clase._2024.Backend.Servicios
{
    /*
     * Clase que contiene las reglas de negocio de la clase Articulo
     */
    public class ArticuloServicio : ServicioGenerico<Articulo>
    {
        private DiinventarioexamenContext contexto;

        /*
         * Constructor
         */
        public ArticuloServicio(DiinventarioexamenContext context) : base(context)
        {
            contexto = context;
        }

        /// <summary>
        /// Obtiene el último id de la tabla articulo
        /// La clave primaria no es autoincrementable y debemos gestionarla nosotros
        /// </summary>
        /// <returns>El número que identifica el último ID</returns>
        public int GetLastId()
        {
            int id = 0;
            Articulo art = contexto.Set<Articulo>().OrderByDescending(a => a.Idarticulo).FirstOrDefault();
            if (art != null) { id = art.Idarticulo; }
            return id;
        }

        /// <summary>
        /// Devuelve true en caso de que el número de serie no se encuentre en la base de datos
        /// Devuelve false en caso de que el número de serie ya exista
        /// </summary>
        /// <param name="serie">Número de serie a comprobar</param>
        /// <returns>Verdadero en caso de que sea único, falso en caso contrario</returns>
        public bool NumserieUnico(string serie)
        {
            bool unico = true;
            if (contexto.Set<Articulo>().Where(a => a.Numserie == serie).Count() > 0)
            {
                unico = false;
            }
            return unico;
        }

        public DateTime GetFechaInicial()
        {
            Articulo art = contexto.Set<Articulo>().OrderBy(a => a.Fechaalta).FirstOrDefault();
            return (DateTime)art.Fechaalta;
        }

        public DateTime GetFechaFinal()
        {
            Articulo art = contexto.Set<Articulo>().OrderByDescending(a => a.Fechaalta).FirstOrDefault();
            return (DateTime)art.Fechaalta;
        }

        internal bool IsValid(AddArticuloMVVM addArticuloMVVM)
        {
            throw new NotImplementedException();
        }
    }
}
