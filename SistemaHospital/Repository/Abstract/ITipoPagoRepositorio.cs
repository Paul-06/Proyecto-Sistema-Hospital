using SistemaHospital.Models;

namespace SistemaHospital.Repository.Abstract
{
    public interface ITipoPagoRepositorio : IRepositorio<TipoPago>
    {
        void Actualizar(TipoPago tipoPago);
    }
}
