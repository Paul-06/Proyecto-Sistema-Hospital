using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaHospital.Models;

namespace SistemaHospital.Repository.Abstract
{
    public interface IExamenRepositorio : IRepositorio<Examan>
    {
        void Actualizar(Examan examen);
        IEnumerable<SelectListItem>? ObtenerTiposDropdown();
    }
}