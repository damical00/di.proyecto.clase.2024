using di.proyecto.clase._2024.Backend.Modelo;

namespace di.proyecto.clase._2024.Backend.Servicios
{
    public class TipoArticuloServicio : ServicioGenerico<Tipoarticulo>
    {
        public TipoArticuloServicio(DiinventarioexamenContext context) : base(context)
        {
        }
    }
}
