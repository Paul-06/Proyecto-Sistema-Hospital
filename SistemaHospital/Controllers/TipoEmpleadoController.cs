using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaHospital.Models;

namespace SistemaHospital.Controllers
{
    public class TipoEmpleadoController : Controller
    {
        private readonly BdHospitalContext _context;

        public TipoEmpleadoController(BdHospitalContext context)
        {
            _context = context;
        }
        #region NAVEGACION
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var TipoEmpleado = new TipoEmpleado();

            if (id is null)
            {
                // Crear nuevo cargo
                return View(TipoEmpleado);
            }

            // Actualizar cargo
            TipoEmpleado = _context.TipoEmpleados.Find(id.GetValueOrDefault()); // GetValueOrDefault() nos permite trabajar con un valor nulo (ver el parámetro)

            if (TipoEmpleado is null)
            {
                return NotFound();
            }

            return View(TipoEmpleado);
        }
        #endregion

        #region API
        #endregion
    }
}
