using di.proyecto.clase._2024.Backend.Modelo;

namespace di.proyecto.clase._2024.Backend.Servicios
{
    public class GrupoServicio : ServicioGenerico<Grupo>
    {
        public GrupoServicio(DiinventarioexamenContext context) : base(context)
        {
        }

        public bool IsEnable { get; internal set; }
    }
}
