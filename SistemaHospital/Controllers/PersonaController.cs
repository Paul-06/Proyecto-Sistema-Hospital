using Microsoft.AspNetCore.Mvc;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        // Inyección de la unidad de trabajo en nuestra variable a través del controlador
        public PersonaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        [ActionName("ValidarDni")]
        public async Task<JsonResult> ValidarDni(string dni, int id = 0)
        {
            bool coincide = false; // Variable para identificar si hay coindicendia o no

            // Retornamos todos los elementos de Especialidad
            var lista = await _unidadTrabajo.Persona.ObtenerTodos();

            // Si el id es 0 (nuevo registro), verificamos si el nombre ya existe en la lista
            if (id == 0)
            {
                coincide = lista.Any(p => p.Dni!.ToLower().Trim() == dni.ToLower().Trim());
            }
            // Si el id no es 0 (registro existente), verificamos si el nombre ya existe en la lista y que el id sea diferente
            else
            {
                coincide = lista.Any(p => p.Dni!.ToLower().Trim() == dni.ToLower().Trim() && p.IdPersona != id);
            }

            // Retornamos la coincidencia (true or false)
            return coincide ? new JsonResult(new { data = true }) : new JsonResult(new { data = false });
        }
    }
}
