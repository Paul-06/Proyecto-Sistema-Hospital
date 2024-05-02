using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class CargoRepositorio : Repositorio<Cargo>, ICargoRepositorio
    {
        // Atributo para el DbContext
        private readonly BdHospitalContext _context;

        // Inyección en el constructor
        public CargoRepositorio(BdHospitalContext context) : base(context) // Enviar el context al padre
        {
            _context = context;
        }

        public void Actualizar(Cargo cargo)
        {
            var registro = _context.Cargos.FirstOrDefault(c => c.IdCargo ==  cargo.IdCargo);

            if (registro != null) // Si se encontró el registro (cargo)
            {
                registro.Nombre = cargo.Nombre;
                registro.Descripcion = cargo.Descripcion;

                _context.SaveChanges();
            }
        }
    }
}
