using SistemaHospital.Dtos;
using SistemaHospital.Models;

namespace SistemaHospital
{
    public static class EntityExtensions
    {
        public static EmpleadoDto ToDto(this Empleado empleado)
        {
            string nombreCompleto = empleado.IdPersonaNavigation!.Nombres! + " "
            + empleado.IdPersonaNavigation!.ApellidoPaterno! + " " 
            + empleado.IdPersonaNavigation!.ApellidoMaterno!;

            string correo = empleado.IdPersonaNavigation!.Correo ?? "No disponible";
            
            return new EmpleadoDto(
                empleado.IdEmpleado,
                empleado.IdPersonaNavigation!.Dni!,
                nombreCompleto,
                empleado.IdPersonaNavigation!.Celular!,
                correo,
                empleado.IdPersonaNavigation!.Direccion!,
                empleado.IdCargoNavigation!.Nombre!,
                empleado.IdTipoEmpleadoNavigation!.Nombre!,
                empleado.IdEspecialidadNavigation!.Nombre!
            );
        }

        public static PacienteDto ToDto(this Paciente paciente)
        {
            string nombreCompleto = paciente.IdPersonaNavigation!.Nombres! + " "
            + paciente.IdPersonaNavigation!.ApellidoPaterno! + " " 
            + paciente.IdPersonaNavigation!.ApellidoMaterno!;

            string fechaNacimiento = paciente.IdPersonaNavigation!.FechaNacimiento!.Value.ToString("dd/MM/yyyy");

            string correo = paciente.IdPersonaNavigation!.Correo ?? "No disponible";

            
            return new PacienteDto(
                paciente.IdPaciente,
                paciente.IdPersonaNavigation!.Dni!,
                nombreCompleto,
                fechaNacimiento,
                paciente.IdPersonaNavigation!.Celular!,
                correo,
                paciente.IdPersonaNavigation!.Direccion!
            );
        }
    }
}