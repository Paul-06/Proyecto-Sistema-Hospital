using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class EspecialidadRepositorio : Repositorio<Especialidad>, IEspecialidadRepositorio
    {
        // Atributo para el DbContext
        private readonly BdHospitalContext _context;

        // Inyección en el constructor
        public EspecialidadRepositorio(BdHospitalContext context) : base(context) // Enviar el context al padre (Repositorio)
        {
            _context = context;
        }
        public void Actualizar(Especialidad especialidad)
        {
            var registro = _context.Especialidads.FirstOrDefault(e => e.IdEspecialidad == especialidad.IdEspecialidad);

            if (registro != null) // Si se encontró el registro
            {
                registro.Nombre = especialidad.Nombre;
                registro.Descripcion = especialidad.Descripcion;

                _context.SaveChanges();
            }
        }
    }
}