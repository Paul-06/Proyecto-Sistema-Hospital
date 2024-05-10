using Microsoft.AspNetCore.Mvc;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Controllers
{
    public class HMedicoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        // Inyección de la unidad de trabajo en nuestra variable a través del controlador
        public HMedicoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        #region NAVEGACION
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DetalleHMedico(int pacienteId){
            var historial = await _unidadTrabajo.HistorialMedico.ObtenerPrimero(
                filtro: hm => hm.IdPaciente == pacienteId,
                incluirPropiedades: "IdPacienteNavigation.IdPersonaNavigation, Examen.IdTipoExamenNavigation, Examen.Resultados, Tratamientos.IdTipoTratamientoNavigation"
            );

            if (historial is null) {
                return NotFound();
            }

            return View(historial.ToDto());
        }
        #endregion

        #region API
        [HttpGet]
        public async Task<JsonResult> ObtenerHistorialMedicoPorPaciente(int pacienteId)
        {
            var historial = await _unidadTrabajo.HistorialMedico.ObtenerPrimero(
                filtro: hm => hm.IdPaciente == pacienteId,
                incluirPropiedades: "IdPacienteNavigation.IdPersonaNavigation, Examen.IdTipoExamenNavigation, Examen.Resultados, Tratamientos.IdTipoTratamientoNavigation"
            );

            if (historial == null)
                return new JsonResult(new { Message = "Historial médico no encontrado" });

            return new JsonResult(historial.ToDto());
        }

        [HttpGet]
        public async Task<JsonResult> ListarHistorialesMedicos()
        {
            var resultado = await _unidadTrabajo.HistorialMedico.ObtenerTodos(
                incluirPropiedades: "IdPacienteNavigation.IdPersonaNavigation, Examen.IdTipoExamenNavigation, Examen.Resultados, Tratamientos.IdTipoTratamientoNavigation"
            );

            var historiales = resultado.Select(hm => new
            {
                hm.IdHistorialMedico,
                Paciente = hm.IdPacienteNavigation!.IdPersonaNavigation!.Nombres + " " +
                           hm.IdPacienteNavigation.IdPersonaNavigation.ApellidoPaterno + " " +
                           hm.IdPacienteNavigation.IdPersonaNavigation.ApellidoMaterno,
                hm.IdPaciente,
                FechaNacimiento = hm.IdPacienteNavigation!.IdPersonaNavigation!.FechaNacimiento!.Value.ToString("dd/MM/yyyy"),
                CantidadExamen = hm.Examen.Count,
                CantidadTratamiento = hm.Tratamientos.Count
            });

            return new JsonResult(new {data = historiales});
        }
        #endregion  
    }
}
