using Microsoft.AspNetCore.Mvc;
using SistemaHospital.Models.ViewModels;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        // Inyección de la unidad de trabajo en nuestra variable a través del controlador
        public EmpleadoController(IUnidadTrabajo unidadTrabajo)
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
            var empleadoVm = new EmpleadoVm()
            {
                CargoLista = _unidadTrabajo.Empleado.ObtenerOpcionesDropdownPorTipo("Cargo"),
                EspecialidadLista = _unidadTrabajo.Empleado.ObtenerOpcionesDropdownPorTipo("Especialidad"),
                TipoEmpleadoLista = _unidadTrabajo.Empleado.ObtenerOpcionesDropdownPorTipo("TipoEmpleado")
            };

            if (id is null)
            {
                // Crear nuevo empleado
                return View(empleadoVm);
            }

            // Actualizar empleado
            var empleado = await _unidadTrabajo.Empleado.ObtenerPrimero(
                e => e.IdEmpleado == id.GetValueOrDefault(), // GetValueOrDefault() nos permite trabajar con un valor nulo (ver el parámetro)
                "IdPersonaNavigation"
            );

            if (empleado is null)
            {
                return NotFound(); // Si no se encuentra el empleado
            }

            // Mapeo de datos del Empleado a EmpleadoVm
            empleadoVm.Id = empleado.IdEmpleado;
            empleadoVm.IdCargo = empleado.IdCargo;
            empleadoVm.IdEspecialidad = empleado.IdEspecialidad;
            empleadoVm.IdTipoEmpleado = empleado.IdTipoEmpleado;

            if (empleado.IdPersonaNavigation != null)
            {
                empleadoVm.Dni = empleado.IdPersonaNavigation.Dni;
                empleadoVm.ApellidoPaterno = empleado.IdPersonaNavigation.ApellidoPaterno;
                empleadoVm.ApellidoMaterno = empleado.IdPersonaNavigation.ApellidoMaterno;
                empleadoVm.Nombres = empleado.IdPersonaNavigation.Nombres;
                empleadoVm.FechaNacimiento = empleado.IdPersonaNavigation.FechaNacimiento;
                empleadoVm.Celular = empleado.IdPersonaNavigation.Celular;
                empleadoVm.Correo = empleado.IdPersonaNavigation.Correo;
                empleadoVm.Direccion = empleado.IdPersonaNavigation.Direccion;
            }

            return View(empleadoVm);
        }
        #endregion

        #region API
        [HttpGet]
        public async Task<JsonResult> ListarEmpleados()
        {
            var empleados = await _unidadTrabajo.Empleado.ObtenerTodos(
                null, null,
                "IdPersonaNavigation,IdCargoNavigation,IdEspecialidadNavigation,IdTipoEmpleadoNavigation",
                false
            );
            return new JsonResult(new { data = empleados });
        }

        // [HttpPost]
        // [ValidateAntiForgeryToken] // Sirve para evitar solicitudes externas
        // public async Task<IActionResult> Upsert(Especialidad especialidad)
        // {
        //     if (ModelState.IsValid) // Si el modelo es válido
        //     {
        //         if (especialidad.IdEspecialidad == 0) // Significa un nuevo registro
        //         {
        //             await _unidadTrabajo.Especialidad.Agregar(especialidad);
        //             TempData[DS.Exitosa] = "Especialidad agregada exitosamente"; // Será usado para las notificaciones
        //         }
        //         else
        //         {
        //             _unidadTrabajo.Especialidad.Actualizar(especialidad);
        //             TempData[DS.Exitosa] = "Especialidad actualizada exitosamente"; // Será usado para las notificaciones
        //         }

        //         await _unidadTrabajo.GuardarCambios(); // Guardar los cambios en la base de datos

        //         return RedirectToAction(nameof(Index)); // Redirigir al Index
        //     }

        //     // Si no se hace nada
        //     TempData[DS.Error] = "Error al guardar los cambios"; // Notificación de error
        //     return View(especialidad);
        // }

        // [HttpDelete]
        // public async Task<JsonResult> Eliminar(int id)
        // {
        //     // Buscamos el registro a eliminar
        //     var registro = await _unidadTrabajo.Especialidad.ObtenerPorId(id);

        //     if (registro is null)
        //     {
        //         return new JsonResult(new { success = false, message = "Error al borrar la especialidad" });
        //     }

        //     // En caso se encuentre el registro
        //     _unidadTrabajo.Especialidad.Remover(registro);

        //     // Guardar los cambios
        //     await _unidadTrabajo.GuardarCambios();

        //     // Enviamos el mensaje de éxito
        //     return new JsonResult(new { success = true, message = "Especialidad eliminada exitosamente" });
        // }

        // [ActionName("ValidarNombre")]
        // public async Task<JsonResult> ValidarNombre(string nombre, int id = 0)
        // {
        //     bool coincide = false; // Variable para identificar si hay coindicendia o no

        //     // Retornamos todos los elementos de Especialidad
        //     var lista = await _unidadTrabajo.Especialidad.ObtenerTodos();

        //     // Si el id es 0 (nuevo registro), verificamos si el nombre ya existe en la lista
        //     if (id == 0)
        //     {
        //         coincide = lista.Any(e => e.Nombre!.ToLower().Trim() == nombre.ToLower().Trim());
        //     }
        //     // Si el id no es 0 (registro existente), verificamos si el nombre ya existe en la lista y que el id sea diferente
        //     else
        //     {
        //         coincide = lista.Any(e => e.Nombre!.ToLower().Trim() == nombre.ToLower().Trim() && e.IdEspecialidad != id);
        //     }

        //     // Retornamos la coincidencia (true or false)
        //     return coincide ? new JsonResult(new { data = true }) : new JsonResult(new { data = false });
        // }
        #endregion
    }
}
