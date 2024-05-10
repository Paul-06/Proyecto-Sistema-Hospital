using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaHospital.Models;

namespace SistemaHospital.Repository.Abstract
{
    public interface ITratamientoRepositorio : IRepositorio<Tratamiento>
    {
        void Actualizar(Tratamiento tratamiento);
        IEnumerable<SelectListItem>? ObtenerOpcionesDropdown();
    }
}