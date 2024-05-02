using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaHospital.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaHospital.Controllers
{
    public class TipoPagoController : Controller
    {
        private readonly BdHospitalContext _context;

        public TipoPagoController(BdHospitalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var tipoPago = new TipoPago();

            if (id == null)
            {
                // Crear un nuevo tipo de pago
                return View(tipoPago);
            }

            // Actualizar tipo de pago existente
            tipoPago = _context.TipoPagos.Find(id);

            if (tipoPago == null)
            {
                return NotFound();
            }

            return View(tipoPago);
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var tiposPago = await _context.TipoPagos.ToListAsync();
            return new JsonResult(new { data = tiposPago });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TipoPago tipoPago)
        {
            if (ModelState.IsValid)
            {
                if (tipoPago.IdTipoPago == 0)
                {
                    // Nuevo registro
                    await _context.TipoPagos.AddAsync(tipoPago);
                }
                else
                {
                    // Actualizar registro existente
                    _context.TipoPagos.Update(tipoPago);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si el modelo no es válido, retorna a la vista con los errores
            return View(tipoPago);
        }
    }
}
