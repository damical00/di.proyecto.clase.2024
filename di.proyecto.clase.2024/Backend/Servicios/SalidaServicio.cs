using di.proyecto.clase._2024.Backend.Modelo;

namespace di.proyecto.clase._2024.Backend.Servicios
{
    public class SalidaServicio : ServicioGenerico<Salidum>
    {
        private DiinventarioexamenContext contexto;

        public SalidaServicio(DiinventarioexamenContext context) : base(context)
        {
            contexto = context;
        }

        public Task<IEnumerable<Salidum>> GetSalidasUsuario(int usu)
        {
            //return contexto.Set<salidum>().Where(s => s.usuario == usu).ToList();
            return FindAsync(s => s.Usuario == usu);

        }
    }
}
