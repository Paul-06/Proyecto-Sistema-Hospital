using SistemaHospital.Models;

namespace SistemaHospital.Repository.Abstract
{
    public interface ITipoEmpleadoRepositorio : IRepositorio<TipoEmpleado>
    {
        void Actualizar(TipoEmpleado tipoEmpleado);
    }
}
