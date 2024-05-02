using SistemaHospital.Models;

namespace SistemaHospital.Repository.Abstract
{
    public interface IEspecialidadRepositorio : IRepositorio<Especialidad>
    {
        void Actualizar(Especialidad especialidad);
    }
}
