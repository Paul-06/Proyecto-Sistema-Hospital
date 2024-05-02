using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaHospital.Models;

namespace SistemaHospital.Controllers
{
    public class TipoExamenController : Controller
    {
        private readonly BdHospitalContext _context;
        public TipoExamenController(BdHospitalContext context)
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
            var tipoExaman = new TipoExaman();

            if (id is null)
            {
                // Crear nuevo cargo
                return View(tipoExaman);
            }

            // Actualizar cargo
            tipoExaman = _context.TipoExamen.Find(id.GetValueOrDefault()); // GetValueOrDefault() nos permite trabajar con un valor nulo (ver el parámetro)

            if (tipoExaman is null)
            {
                return NotFound();
            }

            return View(tipoExaman);
        }
        #endregion

        #region API
        #endregion
    }
}
