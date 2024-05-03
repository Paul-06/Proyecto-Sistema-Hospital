using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class PacienteRepositorio : Repositorio<Paciente>, IPacienteRepositorio
    {
        // Atributo para el DbContext
        private readonly BdHospitalContext _context;

        // Inyección en el constructor
        public PacienteRepositorio(BdHospitalContext context) : base(context) // Enviar el context al padre (Repositorio)
        {
            _context = context;
        }

        public void Actualizar(Paciente paciente)
        {
            var registro = _context.Pacientes.FirstOrDefault(p => p.IdPaciente == paciente.IdPaciente); // Buscar el registro a actualizar

            if (registro != null) // Si se encontró el registro
            {
                registro.IdPersona = paciente.IdPersona;

                _context.SaveChanges();
            }
        }   
    }
}