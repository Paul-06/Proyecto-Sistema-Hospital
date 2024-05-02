using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaHospital.Models;

namespace SistemaHospital.Controllers
{
    public class EspecialidadController : Controller
    {
        private readonly BdHospitalContext _context;
        public EspecialidadController(BdHospitalContext context)
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
            var especialidad = new Especialidad();

            if (id is null)
            {
                // Crear nuevo cargo
                return View(especialidad);
            }

            // Actualizar cargo
            especialidad = _context.Especialidads.Find(id.GetValueOrDefault()); // GetValueOrDefault() nos permite trabajar con un valor nulo (ver el parámetro)

            if (especialidad is null)
            {
                return NotFound();
            }

            return View(especialidad);
        }
        #endregion

        #region API
        #endregion

    }
}
