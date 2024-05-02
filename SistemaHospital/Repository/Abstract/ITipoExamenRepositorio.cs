using SistemaHospital.Models;

namespace SistemaHospital.Repository.Abstract
{
    public interface ITipoExamenRepositorio : IRepositorio<TipoExaman>
    {
        void Actualizar(TipoExaman tipoExaman);
    }
}
