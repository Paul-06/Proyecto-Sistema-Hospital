using Microsoft.AspNetCore.Mvc;

namespace SistemaHospital.Controllers
{
    public class TipoExamenController : Controller
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
