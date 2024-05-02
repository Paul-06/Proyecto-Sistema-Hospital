using SistemaHospital.Models;

namespace SistemaHospital.Repository.Abstract
{
    public interface ITipoTratamientoRepositorio : IRepositorio<TipoTratamiento>
    {
        void Actualizar(TipoTratamiento tipoTratamiento);
    }
}
