using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;
using SistemaHospital.Utils;

namespace SistemaHospital.Controllers
{
    public class CargoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        // Inyección de la unidad de trabajo en nuestra variable
        public CargoController(IUnidadTrabajo unidadTrabajo) // Esto proviene de Program.cs (inyección de dependencias)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        #region NAVEGACION
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id) // Usado en asp-route. Ej. asp-route-id
        {
            var cargo = new Cargo();

            if (id is null)
            {
                // Crear nuevo cargo
                return View(cargo);
            }

            // Actualizar cargo
            cargo = await _unidadTrabajo.Cargo.ObtenerPorId(id.GetValueOrDefault()); // GetValueOrDefault() nos permite trabajar con un valor nulo (ver el parámetro)

            if (cargo is null)
            {
                return NotFound();
            }

            return View(cargo);
        }
        #endregion

        #region API
        [HttpGet]
        public async Task<JsonResult> ListarCargos()
        {
            var cargos = await _unidadTrabajo.Cargo.ObtenerTodos();
            return new JsonResult(new { data = cargos });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Sirve para evitar solicitudes externas
        public async Task<IActionResult> Upsert(Cargo cargo)
        {
            if (ModelState.IsValid) // Si el modelo es válido
            {
                if (cargo.IdCargo == 0) // Significa un nuevo registro
                {
                    await _unidadTrabajo.Cargo.Agregar(cargo);
                    TempData[DS.Exitosa] = "Cargo agregado exitosamente"; // Será usado para las notificaciones
                }
                else
                {
                    _unidadTrabajo.Cargo.Actualizar(cargo);
                    TempData[DS.Exitosa] = "Cargo actualizado exitosamente"; // Será usado para las notificaciones
                }

                await _unidadTrabajo.GuardarCambios(); // Guardar los cambios en la base de datos

                return RedirectToAction(nameof(Index)); // Redirigir al Index
            }

            // Si no se hace nada
            TempData[DS.Error] = "Error al guardar los cambios"; // Notificación de error
            return View(cargo);
        }

        [HttpDelete]
        public async Task<JsonResult> Eliminar(int id)
        {
            // Buscamos el registro a eliminar
            var registro = await _unidadTrabajo.Cargo.ObtenerPorId(id);

            if (registro is null)
            {
                return new JsonResult(new { success = false, message = "Error al borrar el cargo" });
            }

            // En caso se encuentre el registro
            _unidadTrabajo.Cargo.Remover(registro);

            // Guardar los cambios
            await _unidadTrabajo.GuardarCambios();

            // Enviamos el mensaje de éxito
            return new JsonResult(new { success = true, message = "Cargo eliminado exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<JsonResult> ValidarNombre(string nombre, int id = 0)
        {
            bool coincide = false; // Variable para identificar si hay coindicendia o no

            // Retornamos todos los elementos de Cargo
            var lista = await _unidadTrabajo.Cargo.ObtenerTodos();

            // Si el id es 0 (nuevo registro), verificamos si el nombre ya existe en la lista
            if (id == 0)
            {
                coincide = lista.Any(c => c.Nombre!.ToLower().Trim() == nombre.ToLower().Trim());
            }
            // Si el id no es 0 (registro existente), verificamos si el nombre ya existe en la lista y que el id sea diferente
            else
            {
                coincide = lista.Any(c => c.Nombre!.ToLower().Trim() == nombre.ToLower().Trim() && c.IdCargo != id);
            }

            // Retornamos la coincidencia (true or false)
            return coincide ? new JsonResult(new { data = true }) : new JsonResult(new { data = false });
        }
        #endregion
    }
}
