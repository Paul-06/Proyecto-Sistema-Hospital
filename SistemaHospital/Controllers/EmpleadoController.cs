using Microsoft.AspNetCore.Mvc;

namespace SistemaHospital.Controllers
{
    public class EmpleadoController : Controller
    {
        #region NAVEGACION
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert()
        {
            return View();
        }
        #endregion


        #region API
        #endregion
    }
}
