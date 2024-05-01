using Microsoft.AspNetCore.Mvc;

namespace SistemaHospital.Controllers
{
    public class PersonaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
