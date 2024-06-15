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
        public async Task<JsonResult> ObtenerDistribucionxGrupoEtario()
        {
            var pacientes = await _unidadTrabajo.Paciente.ObtenerTodos(
                incluirPropiedades: "IdPersonaNavigation"
            );

            // CalculateAge() es un método definido en Entity Extensions
            var resultado = pacientes
                .Select(p =>
                {
                    var edadCompleta = p.IdPersonaNavigation!.FechaNacimiento!.Value.CalculateAge();
                    var edadAnios = int.Parse(edadCompleta.Split(' ')[0]);
                    return edadAnios;
                })
                .GroupBy(age =>
                {
                    if (age < 18)
                        return "Menores (<18)";
                    else if (age >= 18 && age <= 35)
                        return "Jóvenes (18-35)";
                    else if (age >= 36 && age <= 60)
                        return "Adultos (36-60)";
                    else
                        return "Mayores (>60)";
                })
                .Select(g => new
                {
                    GrupoEdad = g.Key,
                    NroPacientes = g.Count()
                });

            return new JsonResult(new { data = resultado });
        }

        [HttpGet]
        public async Task<JsonResult> ListarPacientes()
        {
            var pacientes = await _unidadTrabajo.Paciente.ObtenerTodos(
                incluirPropiedades: "IdPersonaNavigation"
            );

            var pacienteLista = pacientes.Select(p => p.ToDto());
            return new JsonResult(new { data = pacienteLista });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PacienteVm pacienteVm)
        {
            if (!ModelState.IsValid)
            {
                TempData[DS.Error] = "Error al guardar los cambios";
                return View(pacienteVm);
            }

            using var transaccion = _unidadTrabajo.IniciarTransaccion();
            try
            {
                if (pacienteVm.Id == 0)
                {
                    await AgregarPaciente(pacienteVm);
                    TempData[DS.Exitosa] = "Paciente agregado exitosamente";
                }
                else
                {
                    ActualizarPaciente(pacienteVm);
                    TempData[DS.Exitosa] = "Paciente actualizado exitosamente";
                }

                await _unidadTrabajo.GuardarCambios();
                transaccion.Commit();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                TempData[DS.Error] = $"Error al guardar los cambios: {ex.Message}";
                return View(pacienteVm);
            }
        }


        [HttpDelete]
        public async Task<JsonResult> Eliminar(int id)
        {
            using var transaccion = _unidadTrabajo.IniciarTransaccion();

            try
            {
                // Buscamos el registro a eliminar
                var paciente = await _unidadTrabajo.Paciente.ObtenerPorId(id);

                if (paciente is null)
                {
                    return new JsonResult(new { success = false, message = "Error al encontrar el paciente" });
                }

                var persona = await _unidadTrabajo.Persona.ObtenerPrimero(pe => pe.IdPersona == paciente.IdPersona);

                if (persona is null)
                {
                    return new JsonResult(new { success = false, message = "Error al encontrar la persona" });
                }

                var historialMedico = await _unidadTrabajo.HistorialMedico.ObtenerPrimero(hm => hm.IdPaciente == paciente.IdPaciente);

                if (historialMedico is null)
                {
                    return new JsonResult(new { success = false, message = "Error al encontrar el historial médico" });
                }

                // En caso se encuentre el registro
                // Primero borramos el historial médico
                _unidadTrabajo.HistorialMedico.Remover(historialMedico);

                // Luego, borramos el paciente
                _unidadTrabajo.Paciente.Remover(paciente);

                // Finalmente borramos la persona asociada al paciente
                _unidadTrabajo.Persona.Remover(persona);

                // Guardar los cambios
                await _unidadTrabajo.GuardarCambios();

                // Confirmar transacción
                transaccion.Commit();

                // Enviamos el mensaje de éxito
                return new JsonResult(new { success = true, message = "Paciente eliminado exitosamente" });
            }
            catch (Exception)
            {
                transaccion.Rollback();
                return new JsonResult(new { success = false, message = "Error al borrar el paciente " });
            }
        }
        #endregion

        #region METODOS PRIVADOS
        private async Task AgregarPaciente(PacienteVm pacienteVm)
        {
            var persona = new Persona
            {
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

            if (persona.IdPersona == 0)
            {
                throw new Exception("Error al guardar la persona en la base de datos");
            }

            var paciente = new Paciente
            {
                IdPersona = persona.IdPersona
            };

            await _unidadTrabajo.Paciente.Agregar(paciente);
            await _unidadTrabajo.GuardarCambios();

            if (paciente.IdPaciente == 0)
            {
                throw new Exception("Error al guardar el paciente en la base de datos");
            }

            var historialMedico = new HistorialMedico
            {
                IdPaciente = paciente.IdPaciente
            };

            await _unidadTrabajo.HistorialMedico.Agregar(historialMedico);
        }

        private void ActualizarPaciente(PacienteVm pacienteVm)
        {
            var persona = new Persona
            {
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
        }
        #endregion
    }
}
