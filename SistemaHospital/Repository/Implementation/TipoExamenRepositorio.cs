using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class TipoExamenRepositorio : Repositorio<TipoExaman>, ITipoExamenRepositorio
    {
        // Atributo para el DbContext
        private readonly BdHospitalContext _context;

        // Inyección en el constructor
        public TipoExamenRepositorio(BdHospitalContext context) : base(context) // Enviar el context al padre (Repositorio)
        {
            _context = context;
        }

        public void Actualizar(TipoExaman tipoExamen)
        {
            var registro = _context.TipoExamen.FirstOrDefault(te => te.IdTipoExamen == tipoExamen.IdTipoExamen); // Buscar el registro a actualizar

            if (registro != null) // Si se encontró el registro
            {
                registro.Nombre = tipoExamen.Nombre;
                registro.Descripcion = tipoExamen.Descripcion;

                _context.SaveChanges();
            }
        }
    }
}