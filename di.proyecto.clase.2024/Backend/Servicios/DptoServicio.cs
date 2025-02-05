using di.proyecto.clase._2024.Backend.Modelo;

namespace di.proyecto.clase._2024.Backend.Servicios
{
    public class DptoServicio : ServicioGenerico<Departamento>
    {

        public DptoServicio(DiinventarioexamenContext context) : base(context)
        {
        }
    }
}
