using Microsoft.AspNetCore.Mvc;
using SistemaHospital.Models;
using SistemaHospital.Models.ViewModels;
using SistemaHospital.Repository.Abstract;
using SistemaHospital.Utils;

namespace SistemaHospital.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        // Inyección de la unidad de trabajo en nuestra variable a través del controlador
        public PacienteController(IUnidadTrabajo unidadTrabajo)
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
            var pacienteVm = new PacienteVm();

            if (id is null)
            {
                // Crear nuevo paciente
                return View(pacienteVm);
            }

            // Actualizar paciente
            var paciente = await _unidadTrabajo.Paciente.ObtenerPrimero(
                p => p.IdPaciente == id.GetValueOrDefault(), // GetValueOrDefault() nos permite trabajar con un valor nulo (ver el parámetro)
                "IdPersonaNavigation"
            );

            if (paciente is null)
            {
                return NotFound(); // Si no se encuentra el empleado
            }

            // Mapeo de datos del Empleado a EmpleadoVm
            pacienteVm.Id = paciente.IdPaciente;
            pacienteVm.IdPersona = paciente.IdPersona;

            if (paciente.IdPersonaNavigation != null)
            {
                pacienteVm.Dni = paciente.IdPersonaNavigation.Dni;
                pacienteVm.ApellidoPaterno = paciente.IdPersonaNavigation.ApellidoPaterno;
                pacienteVm.ApellidoMaterno = paciente.IdPersonaNavigation.ApellidoMaterno;
                pacienteVm.Nombres = paciente.IdPersonaNavigation.Nombres;
                pacienteVm.FechaNacimiento = paciente.IdPersonaNavigation.FechaNacimiento;
                pacienteVm.Celular = paciente.IdPersonaNavigation.Celular;
                pacienteVm.Correo = paciente.IdPersonaNavigation.Correo;
                pacienteVm.Direccion = paciente.IdPersonaNavigation.Direccion;
            }

            return View(pacienteVm);
        }
        #endregion

        #region API
        [HttpGet]
        public async Task<JsonResult> ListarPacientes()
        {
            var pacientes = await _unidadTrabajo.Paciente.ObtenerTodos(
                null, null,
                "IdPersonaNavigation"
            );

            var pacienteLista = pacientes.Select(p => p.ToDto());
            return new JsonResult(new { data = pacienteLista });
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Sirve para evitar solicitudes externas
        public async Task<IActionResult> Upsert(PacienteVm pacienteVm)
        {
            if (ModelState.IsValid) // Si el modelo es válido
            {
                if (pacienteVm.Id == 0) // Significa un nuevo registro
                {
                    var persona = new Persona{
                        Dni = pacienteVm.Dni,
                        ApellidoPaterno = pacienteVm.ApellidoPaterno,
                        ApellidoMaterno = pacienteVm.ApellidoMaterno,
                        Nombres = pacienteVm.Nombres,
                        FechaNacimiento = pacienteVm.FechaNacimiento,
                        Celular = pacienteVm.Celular,
                        Correo = pacienteVm.Correo,
                        Direccion = pacienteVm.Direccion
                    };

                    await _unidadTrabajo.Persona.Agregar(persona);
                    await _unidadTrabajo.GuardarCambios();

                    var paciente = new Paciente{
                        IdPersona = persona.IdPersona, // Al ejecutar SaveChanges(), se recupera el Id del objeto
                    };

                    await _unidadTrabajo.Paciente.Agregar(paciente);
                    TempData[DS.Exitosa] = "Paciente agregado exitosamente"; // Será usado para las notificaciones
                }
                else
                {
                    var persona = new Persona{
                        IdPersona = (int)pacienteVm.IdPersona!,
                        Dni = pacienteVm.Dni,
                        ApellidoPaterno = pacienteVm.ApellidoPaterno,
                        ApellidoMaterno = pacienteVm.ApellidoMaterno,
                        Nombres = pacienteVm.Nombres,
                        FechaNacimiento = pacienteVm.FechaNacimiento,
                        Celular = pacienteVm.Celular,
                        Correo = pacienteVm.Correo,
                        Direccion = pacienteVm.Direccion
                    };

                    _unidadTrabajo.Persona.Actualizar(persona);

                    TempData[DS.Exitosa] = "Paciente actualizado exitosamente"; // Será usado para las notificaciones
                }

                await _unidadTrabajo.GuardarCambios(); // Guardar los cambios en la base de datos

                return RedirectToAction(nameof(Index)); // Redirigir al Index
            }

            // Si no se hace nada
            TempData[DS.Error] = "Error al guardar los cambios"; // Notificación de error
            return View(pacienteVm);
        }

        [HttpDelete]
        public async Task<JsonResult> Eliminar(int id)
        {
            // Buscamos el registro a eliminar
            var registro = await _unidadTrabajo.Paciente.ObtenerPorId(id);

            if (registro is null)
            {
                return new JsonResult(new { success = false, message = "Error al borrar el paciente" });
            }

            var persona = await _unidadTrabajo.Persona.ObtenerPorId((int)registro.IdPersona!);

            if (persona is null)
            {
                return new JsonResult(new { success = false, message = "Error al borrar el paciente" });
            }

            // En caso se encuentre el registro
            _unidadTrabajo.Paciente.Remover(registro);

            // Borramos la persona asociada al paciente
            _unidadTrabajo.Persona.Remover(persona);

            // Guardar los cambios
            await _unidadTrabajo.GuardarCambios();

            // Enviamos el mensaje de éxito
            return new JsonResult(new { success = true, message = "Paciente eliminado exitosamente" });
        }
        #endregion
    }
}
