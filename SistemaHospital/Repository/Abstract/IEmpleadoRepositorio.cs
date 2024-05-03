using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaHospital.Models;

namespace SistemaHospital.Repository.Abstract
{
    public interface IEmpleadoRepositorio : IRepositorio<Empleado>
    {
        void Actualizar(Empleado empleado);
        IEnumerable<SelectListItem>? ObtenerOpcionesDropdownPorTipo(string tipo);
    }
}
