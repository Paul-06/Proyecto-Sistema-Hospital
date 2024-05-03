using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class TipoTratamientoRepositorio : Repositorio<TipoTratamiento>, ITipoTratamientoRepositorio
    {
        // Atributo para el DbContext
        private readonly BdHospitalContext _context;

        // Inyección en el constructor
        public TipoTratamientoRepositorio(BdHospitalContext context) : base(context) // Enviar el context al padre (Repositorio)
        {
            _context = context;
        }

        public void Actualizar(TipoTratamiento tipoTratamiento)
        {
            var registro = _context.TipoTratamientos.FirstOrDefault(tt => tt.IdTipoTratamiento == tipoTratamiento.IdTipoTratamiento); // Buscar el registro a actualizar

            if (registro != null) // Si se encontró el registro
            {
                registro.Descripcion = tipoTratamiento.Descripcion;

                _context.SaveChanges();
            }
        }
    }
}