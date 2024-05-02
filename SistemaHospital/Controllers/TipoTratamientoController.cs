using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaHospital.Models;

namespace SistemaHospital.Controllers
{
    public class TipoTratamientoController : Controller
    {
        private readonly BdHospitalContext _context;
        public TipoTratamientoController(BdHospitalContext context)
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
            var tipotratamiento = new TipoTratamiento();

            if (id is null)
            {
                // Crear nuevo cargo
                return View(tipotratamiento);
            }

            // Actualizar cargo
            tipotratamiento = _context.TipoTratamientos.Find(id.GetValueOrDefault()); // GetValueOrDefault() nos permite trabajar con un valor nulo (ver el parámetro)

            if (tipotratamiento is null)
            {
                return NotFound();
            }

            return View(tipotratamiento);
        }
        #endregion

        #region API
        #endregion
    }
}
