using di.proyecto.clase._2024.Backend.Modelo;

namespace di.proyecto.clase._2024.Backend.Servicios
{
    public class EspacioServicio : ServicioGenerico<Espacio>
    {
        public EspacioServicio(DiinventarioexamenContext context) : base(context)
        {
        }
    }
}
