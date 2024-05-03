using Microsoft.AspNetCore.Mvc;
using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;
using SistemaHospital.Utils;

namespace SistemaHospital.Controllers
{
    public class EspecialidadController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        // Inyección de la unidad de trabajo en nuestra variable a través del controlador
        public EspecialidadController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        #region NAVEGACION
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var especialidad = new Especialidad();

            if (id is null)
            {
                // Crear nueva especialidad
                return View(especialidad);
            }

            // Actualizar especialidad
            especialidad = await _unidadTrabajo.Especialidad.ObtenerPorId(id.GetValueOrDefault()); // GetValueOrDefault() nos permite trabajar con un valor nulo (ver el parámetro)

            if (especialidad is null)
            {
                return NotFound(); // Si no se encuentra la especialidad
            }

            return View(especialidad);
        }
        #endregion

        #region API
        [HttpGet]
        public async Task<JsonResult> ListarEspecialidades()
        {
            var especialidades = await _unidadTrabajo.Especialidad.ObtenerTodos();
            return new JsonResult(new { data = especialidades });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Sirve para evitar solicitudes externas
        public async Task<IActionResult> Upsert(Especialidad especialidad)
        {
            if (ModelState.IsValid) // Si el modelo es válido
            {
                if (especialidad.IdEspecialidad == 0) // Significa un nuevo registro
                {
                    await _unidadTrabajo.Especialidad.Agregar(especialidad);
                    TempData[DS.Exitosa] = "Especialidad agregada exitosamente"; // Será usado para las notificaciones
                }
                else
                {
                    _unidadTrabajo.Especialidad.Actualizar(especialidad);
                    TempData[DS.Exitosa] = "Especialidad actualizada exitosamente"; // Será usado para las notificaciones
                }

                await _unidadTrabajo.GuardarCambios(); // Guardar los cambios en la base de datos

                return RedirectToAction(nameof(Index)); // Redirigir al Index
            }

            // Si no se hace nada
            TempData[DS.Error] = "Error al guardar los cambios"; // Notificación de error
            return View(especialidad);
        }

        [HttpDelete]
        public async Task<JsonResult> Eliminar(int id)
        {
            // Buscamos el registro a eliminar
            var registro = await _unidadTrabajo.Especialidad.ObtenerPorId(id);

            if (registro is null)
            {
                return new JsonResult(new { success = false, message = "Error al borrar la especialidad" });
            }

            // En caso se encuentre el registro
            _unidadTrabajo.Especialidad.Remover(registro);

            // Guardar los cambios
            await _unidadTrabajo.GuardarCambios();

            // Enviamos el mensaje de éxito
            return new JsonResult(new { success = true, message = "Especialidad eliminada exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<JsonResult> ValidarNombre(string nombre, int id = 0)
        {
            bool coincide = false; // Variable para identificar si hay coindicendia o no

            // Retornamos todos los elementos de Especialidad
            var lista = await _unidadTrabajo.Especialidad.ObtenerTodos();

            // Si el id es 0 (nuevo registro), verificamos si el nombre ya existe en la lista
            if (id == 0)
            {
                coincide = lista.Any(e => e.Nombre!.ToLower().Trim() == nombre.ToLower().Trim());
            }
            // Si el id no es 0 (registro existente), verificamos si el nombre ya existe en la lista y que el id sea diferente
            else
            {
                coincide = lista.Any(e => e.Nombre!.ToLower().Trim() == nombre.ToLower().Trim() && e.IdEspecialidad != id);
            }

            // Retornamos la coincidencia (true or false)
            return coincide ? new JsonResult(new { data = true }) : new JsonResult(new { data = false });
        }
        #endregion

    }
}
