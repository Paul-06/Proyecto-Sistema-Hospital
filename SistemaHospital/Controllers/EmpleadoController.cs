using Microsoft.AspNetCore.Mvc;
using SistemaHospital.Models;
using SistemaHospital.Models.ViewModels;
using SistemaHospital.Repository.Abstract;
using SistemaHospital.Utils;

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
            empleadoVm.IdPersona = empleado.IdPersona;
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
                incluirPropiedades: "IdPersonaNavigation, IdCargoNavigation, IdEspecialidadNavigation, IdTipoEmpleadoNavigation"
            );

            var empleadoLista = empleados.Select(e => e.ToDto());
            return new JsonResult(new { data = empleadoLista });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Sirve para evitar solicitudes externas
        public async Task<IActionResult> Upsert(EmpleadoVm empleadoVm)
        {
            if (!ModelState.IsValid)
            {
                TempData[DS.Error] = "Error al guardar los cambios";
                return View(empleadoVm);
            }

            using var transaccion = _unidadTrabajo.IniciarTransaccion();
            try
            {
                if (empleadoVm.Id == 0) // Se trata de una insercion
                {
                    await AgregarEmpleado(empleadoVm);
                    TempData[DS.Exitosa] = "Empleado agregado exitosamente";
                }
                else
                {
                    ActualizarEmpleado(empleadoVm);
                    TempData[DS.Exitosa] = "Empleado actualizado exitosamente";
                }

                await _unidadTrabajo.GuardarCambios();
                transaccion.Commit();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                transaccion.Rollback();
                TempData[DS.Error] = "Error al guardar los cambios";
                return View(empleadoVm);
            }
        }

        [HttpDelete]
        public async Task<JsonResult> Eliminar(int id)
        {
            using var transaccion = _unidadTrabajo.IniciarTransaccion();
            try
            {
                // Buscamos el registro a eliminar
                var registro = await _unidadTrabajo.Empleado.ObtenerPorId(id);

                if (registro is null)
                {
                    return new JsonResult(new { success = false, message = "Error al encontrar el empleado" });
                }

                var persona = await _unidadTrabajo.Persona.ObtenerPorId((int)registro.IdPersona!);

                if (persona is null)
                {
                    return new JsonResult(new { success = false, message = "Error al encontrar la persona" });
                }

                // En caso se encuentre el registro
                _unidadTrabajo.Empleado.Remover(registro);

                // Borramos la persona asociada al empleado
                _unidadTrabajo.Persona.Remover(persona);

                // Guardar los cambios
                await _unidadTrabajo.GuardarCambios();

                // Confirmar transacción
                transaccion.Commit();

                // Enviamos el mensaje de éxito
                return new JsonResult(new { success = true, message = "Empleado eliminado exitosamente" });
            }
            catch (Exception)
            {
                transaccion.Rollback();
                return new JsonResult(new { success = false, message = "Error al borrar el empleado" });
            }

        }
        #endregion

        #region METODOS PRIVADOS
        private async Task AgregarEmpleado(EmpleadoVm empleadoVm)
        {
            var persona = new Persona
            {
                Dni = empleadoVm.Dni,
                ApellidoPaterno = empleadoVm.ApellidoPaterno,
                ApellidoMaterno = empleadoVm.ApellidoMaterno,
                Nombres = empleadoVm.Nombres,
                FechaNacimiento = empleadoVm.FechaNacimiento,
                Celular = empleadoVm.Celular,
                Correo = empleadoVm.Correo,
                Direccion = empleadoVm.Direccion
            };

            await _unidadTrabajo.Persona.Agregar(persona);
            await _unidadTrabajo.GuardarCambios();

            if (persona.IdPersona != 0)
            {
                throw new Exception("Error al guardar la persona en la base de datos");
            }

            var empleado = new Empleado
            {
                IdPersona = persona.IdPersona,
                IdTipoEmpleado = empleadoVm.IdTipoEmpleado,
                IdEspecialidad = empleadoVm.IdEspecialidad,
                IdCargo = empleadoVm.IdCargo
            };

            await _unidadTrabajo.Empleado.Agregar(empleado);
        }

        private void ActualizarEmpleado(EmpleadoVm empleadoVm)
        {
            var persona = new Persona
            {
                IdPersona = (int)empleadoVm.IdPersona!,
                Dni = empleadoVm.Dni,
                ApellidoPaterno = empleadoVm.ApellidoPaterno,
                ApellidoMaterno = empleadoVm.ApellidoMaterno,
                Nombres = empleadoVm.Nombres,
                FechaNacimiento = empleadoVm.FechaNacimiento,
                Celular = empleadoVm.Celular,
                Correo = empleadoVm.Correo,
                Direccion = empleadoVm.Direccion
            };

            _unidadTrabajo.Persona.Actualizar(persona);

            var empleado = new Empleado
            {
                IdEmpleado = empleadoVm.Id,
                IdTipoEmpleado = empleadoVm.IdTipoEmpleado,
                IdEspecialidad = empleadoVm.IdEspecialidad,
                IdCargo = empleadoVm.IdCargo
            };

            _unidadTrabajo.Empleado.Actualizar(empleado);
        }
        #endregion
    }
}
