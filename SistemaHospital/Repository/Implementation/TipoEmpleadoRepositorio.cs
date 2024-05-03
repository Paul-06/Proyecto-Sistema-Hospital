using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class TipoEmpleadoRepositorio : Repositorio<TipoEmpleado>, ITipoEmpleadoRepositorio
    {
        // Atributo para el DbContext
        private readonly BdHospitalContext _context;

        // Inyección en el constructor
        public TipoEmpleadoRepositorio(BdHospitalContext context) : base(context) // Enviar el context al padre (Repositorio)
        {
            _context = context;
        }

        public void Actualizar(TipoEmpleado tipoEmpleado)
        {
            var registro = _context.TipoEmpleados.FirstOrDefault(te => te.IdTipoEmpleado == tipoEmpleado.IdTipoEmpleado); // Buscar el registro a actualizar

            if (registro != null) // Si se encontró el registro
            {
                registro.Nombre = tipoEmpleado.Nombre;
                registro.Descripcion = tipoEmpleado.Descripcion;

                _context.SaveChanges();
            }
        }
    }
}