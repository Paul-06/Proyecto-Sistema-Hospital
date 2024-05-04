using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class HistorialMedicoRepositorio : Repositorio<HistorialMedico>, IHistorialMedicoRepositorio
    {
        private readonly BdHospitalContext _context;

        public HistorialMedicoRepositorio(BdHospitalContext context) : base(context)
        {
            _context = context;
        }
    }
}