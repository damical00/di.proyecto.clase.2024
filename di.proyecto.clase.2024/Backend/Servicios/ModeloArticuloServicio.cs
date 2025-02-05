using di.proyecto.clase._2024.Backend.Modelo;

namespace di.proyecto.clase._2024.Backend.Servicios
{
    public class ModeloArticuloServicio : ServicioGenerico<Modeloarticulo>
    {
        private DiinventarioexamenContext contexto;

        public ModeloArticuloServicio(DiinventarioexamenContext context) : base(context)
        {
            contexto = context;
        }

        public Task<IEnumerable<Modeloarticulo>> getModelosPorTipo (int tipo)
        {
            //return contexto.Set<modeloarticulo>().Where(m => m.tipo == tipo).ToList();
            return FindAsync(m => m.Tipo == tipo);
        }
    }
}
