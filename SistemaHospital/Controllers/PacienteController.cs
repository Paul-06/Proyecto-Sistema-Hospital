using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaHospital.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaHospital.Controllers
{
    public class PacienteController : Controller
    {
        private readonly BdHospitalContext _context;

        public PacienteController(BdHospitalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var paciente = new Paciente();

            if (id == null)
            {
                // Crear un nuevo paciente
                return View(paciente);
            }

            // Actualizar paciente existente
            paciente = _context.Pacientes
                .Include(p => p.IdPersonaNavigation) // Cargar la entidad relacionada Persona
                .FirstOrDefault(p => p.IdPaciente == id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var pacientes = await _context.Pacientes
                .Include(p => p.IdPersonaNavigation) // Cargar la entidad relacionada Persona
                .ToListAsync();
            return new JsonResult(new { data = pacientes });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                if (paciente.IdPaciente == 0)
                {
                    // Nuevo paciente
                    _context.Pacientes.Add(paciente);
                }
                else
                {
                    // Actualizar paciente existente
                    _context.Pacientes.Update(paciente);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido, retorna a la vista con los errores
            return View(paciente);
        }
    }
}
