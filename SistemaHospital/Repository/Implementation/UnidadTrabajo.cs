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

        // Hacemos la inyección en el  constructor
        public UnidadTrabajo(BdHospitalContext context)
        {
            _context = context;
            // Inicializamos los repositorios con sus respectivas implementaciones
            // Las implementaciones necesitan de _context para poder realizar
            // sus operaciones
            Cargo = new CargoRepositorio(_context);
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
