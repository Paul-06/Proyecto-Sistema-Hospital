using SistemaHospital.Models;

namespace SistemaHospital.Repository.Abstract
{
    public interface ICargoRepositorio : IRepositorio<Cargo>
    {
        void Actualizar(Cargo cargo);
    }
}
