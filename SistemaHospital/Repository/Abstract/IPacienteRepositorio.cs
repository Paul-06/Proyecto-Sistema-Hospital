using SistemaHospital.Models;

namespace SistemaHospital.Repository.Abstract
{
    public interface IPacienteRepositorio : IRepositorio<Paciente>
    {
        void Actualizar(Paciente paciente);
    }
}
