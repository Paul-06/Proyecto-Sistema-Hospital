using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaHospital.Models;

namespace SistemaHospital.Controllers
{
    public class CargoController : Controller
    {
        // DbContext
        private readonly BdHospitalContext _context;

        // Inyección del DbContext (ver Program.cs)
        public CargoController(BdHospitalContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var cargo = new Cargo();

            if (id is null)
            {
                // Crear nuevo cargo
                return View(cargo);
            }

            // Actualizar cargo
            cargo = _context.Cargos.Find(id.GetValueOrDefault()); // GetValueOrDefault() nos permite trabajar con un valor nulo (ver el parámetro)

            if (cargo is null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var cargos = await _context.Cargos.ToListAsync();
            return new JsonResult(new { data = cargos });
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Cargo cargo)
        {
            if (ModelState.IsValid) // Si el modelo es válido
            {
                if (cargo.IdCargo == 0) // Significa un nuevo registro
                {
                    await _context.Cargos.AddAsync(cargo);
                }
                else
                {
                    _context.Cargos.Update(cargo);
                }

                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return RedirectToAction(nameof(Index)); // Redirigir al Index
            }

            // Si no se hace nada
            return View(cargo);
        }
    }
}
