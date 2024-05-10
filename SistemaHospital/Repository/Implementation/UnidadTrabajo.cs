using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        // Atributo para el DbContext
        private readonly BdHospitalContext _context;

        // Propiedades
        public ICargoRepositorio Cargo { get; private set; }
        public IEspecialidadRepositorio Especialidad { get; private set; }
        public ITipoEmpleadoRepositorio TipoEmpleado { get; private set; }
        public ITipoPagoRepositorio TipoPago { get; private set; }
        public IPacienteRepositorio Paciente { get; private set; }
        public ITipoExamenRepositorio TipoExamen { get; private set; }
        public ITipoTratamientoRepositorio TipoTratamiento { get; private set; }
        public IPersonaRepositorio Persona { get; private set; }
        public IEmpleadoRepositorio Empleado { get; private set; }
        public IHistorialMedicoRepositorio HistorialMedico { get; private set; }
        public IExamenRepositorio Examen { get; private set; }
        public ITratamientoRepositorio Tratamiento { get; private set; }
        public IResultadoRepositorio Resultado { get; private set; }

        // Hacemos la inyección en el  constructor
        public UnidadTrabajo(BdHospitalContext context)
        {
            _context = context;
            // Inicializamos los repositorios con sus respectivas implementaciones
            // Las implementaciones necesitan de _context para poder realizar
            // sus operaciones
            Cargo = new CargoRepositorio(_context);
            Especialidad = new EspecialidadRepositorio(_context);
            TipoEmpleado = new TipoEmpleadoRepositorio(_context);
            TipoPago = new TipoPagoRepositorio(_context);
            Paciente = new PacienteRepositorio(_context);
            TipoExamen = new TipoExamenRepositorio(_context);
            TipoTratamiento = new TipoTratamientoRepositorio(_context);
            Persona = new PersonaRepositorio(_context);
            Empleado = new EmpleadoRepositorio(_context);
            HistorialMedico = new HistorialMedicoRepositorio(_context);
            Examen = new ExamenRepositorio(_context);
            Tratamiento = new TratamientoRepositorio(_context);
            Resultado = new ResultadoRepositorio(_context);
        }

        public void Dispose()
        {
            _context.Dispose(); // Liberar memoria
        }

        public async Task GuardarCambios()
        {
            await _context.SaveChangesAsync();
        }


    }
}
