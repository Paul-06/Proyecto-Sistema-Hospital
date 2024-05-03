using Microsoft.AspNetCore.Mvc;
using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;
using SistemaHospital.Utils;

namespace SistemaHospital.Controllers
{
    public class TipoTratamientoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        // Inyección de la unidad de trabajo en nuestra variable a través del controlador
        public TipoTratamientoController(IUnidadTrabajo unidadTrabajo)
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
            var tipoTratamiento = new TipoTratamiento();

            if (id is null)
            {
                // Crear nuevo tipo de empleado
                return View(tipoTratamiento);
            }

            // Actualizar tipo de empleado
            tipoTratamiento = await _unidadTrabajo.TipoTratamiento.ObtenerPorId(id.GetValueOrDefault()); // GetValueOrDefault() nos permite trabajar con un valor nulo (ver el parámetro)

            if (tipoTratamiento is null)
            {
                return NotFound(); // Si no se encuentra el tipo de empleado
            }

            return View(tipoTratamiento);
        }
        #endregion

        #region API
        [HttpGet]
        public async Task<JsonResult> ListarTiposTratamiento()
        {
            var tiposTratamiento = await _unidadTrabajo.TipoTratamiento.ObtenerTodos();
            return new JsonResult(new { data = tiposTratamiento });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Sirve para evitar solicitudes externas
        public async Task<IActionResult> Upsert(TipoTratamiento tipoTratamiento)
        {
            if (ModelState.IsValid) // Si el modelo es válido
            {
                if (tipoTratamiento.IdTipoTratamiento == 0) // Significa un nuevo registro
                {
                    await _unidadTrabajo.TipoTratamiento.Agregar(tipoTratamiento);
                    TempData[DS.Exitosa] = "Tipo de tratamiento agregado exitosamente"; // Será usado para las notificaciones
                }
                else
                {
                    _unidadTrabajo.TipoTratamiento.Actualizar(tipoTratamiento);
                    TempData[DS.Exitosa] = "Tipo de tratamiento actualizado exitosamente"; // Será usado para las notificaciones
                }

                await _unidadTrabajo.GuardarCambios(); // Guardar los cambios en la base de datos

                return RedirectToAction(nameof(Index)); // Redirigir al Index
            }

            // Si no se hace nada
            TempData[DS.Error] = "Error al guardar los cambios"; // Notificación de error
            return View(tipoTratamiento);
        }

        [HttpDelete]
        public async Task<JsonResult> Eliminar(int id)
        {
            // Buscamos el registro a eliminar
            var registro = await _unidadTrabajo.TipoTratamiento.ObtenerPorId(id);

            if (registro is null)
            {
                return new JsonResult(new { success = false, message = "Error al borrar el tipo de tratamiento" });
            }

            // En caso se encuentre el registro
            _unidadTrabajo.TipoTratamiento.Remover(registro);

            // Guardar los cambios
            await _unidadTrabajo.GuardarCambios();

            // Enviamos el mensaje de éxito
            return new JsonResult(new { success = true, message = "Tipo de tratamiento eliminado exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<JsonResult> ValidarNombre(string nombre, int id = 0)
        {
            bool coincide = false; // Variable para identificar si hay coindicendia o no

            // Retornamos todos los elementos de TipoTratamiento
            var lista = await _unidadTrabajo.TipoTratamiento.ObtenerTodos();

            // Si el id es 0 (nuevo registro), verificamos si el nombre ya existe en la lista
            if (id == 0)
            {
                coincide = lista.Any(tt => tt.Nombre!.ToLower().Trim() == nombre.ToLower().Trim());
            }
            // Si el id no es 0 (registro existente), verificamos si el nombre ya existe en la lista y que el id sea diferente
            else
            {
                coincide = lista.Any(tt => tt.Nombre!.ToLower().Trim() == nombre.ToLower().Trim() && tt.IdTipoTratamiento != id);
            }

            // Retornamos la coincidencia (true or false)
            return coincide ? new JsonResult(new { data = true }) : new JsonResult(new { data = false });
        }
        #endregion
    }
}
