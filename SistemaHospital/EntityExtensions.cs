using System.Collections.Immutable;
using SistemaHospital.Dtos;
using SistemaHospital.Models;

namespace SistemaHospital
{
    public static class EntityExtensions
    {
        public static EmpleadoDto ToDto(this Empleado empleado)
        {
            return new EmpleadoDto(
                empleado.IdEmpleado,
                empleado.IdPersonaNavigation!.Dni!,
                empleado.IdPersonaNavigation!.ToCompleteName(),
                empleado.IdPersonaNavigation!.Celular!,
                empleado.IdPersonaNavigation!.Correo ?? "No disponible",
                empleado.IdPersonaNavigation!.Direccion!,
                empleado.IdCargoNavigation!.Nombre!,
                empleado.IdTipoEmpleadoNavigation!.Nombre!,
                empleado.IdEspecialidadNavigation!.Nombre!
            );
        }

        public static PacienteDto ToDto(this Paciente paciente, bool includeId = true, bool includeAge = false)
        {
            return new PacienteDto(
                includeId ? paciente.IdPaciente : null, // Si no se quiere incluir el Id, se retorna null
                paciente.IdPersonaNavigation!.Dni!,
                paciente.IdPersonaNavigation!.ToCompleteName(),
                paciente.IdPersonaNavigation!.FechaNacimiento!.Value.ToString("dd/MM/yyyy"),
                includeAge ? paciente.IdPersonaNavigation!.FechaNacimiento!.Value.CalculateAge() : null,
                paciente.IdPersonaNavigation!.Celular!,
                paciente.IdPersonaNavigation!.Correo ?? "No disponible",
                paciente.IdPersonaNavigation!.Direccion!
            );
        }

        public static ResultadoDto ToDto(this Resultado resultado)
        {
            return new ResultadoDto(
                resultado.FechaEntrega?.ToString("dd/MM/yyyy") ?? "No disponible",
                resultado.Descripcion ?? "No disponible"
            );
        }

        public static ExamenDto ToDto(this Examan examen)
        {
            return new ExamenDto(
                examen.IdTipoExamenNavigation!.Nombre!,
                examen.FechaSolicitud?.ToString("dd/MM/yyyy") ?? "No disponible",
                examen.FechaAplicacion?.ToString("dd/MM/yyyy") ?? "No disponible",
                examen.Observaciones ?? "No disponible",
                examen.Resultados?.Select(r => r.ToDto()).ToImmutableList() ?? ImmutableList<ResultadoDto>.Empty
            );
        }

        public static TratamientoDto ToDto(this Tratamiento tratamiento)
        {
            return new TratamientoDto(
                tratamiento.IdTipoTratamientoNavigation!.Nombre!,
                tratamiento.FechaInicio?.ToString("dd/MM/yyyy") ?? "No disponible",
                tratamiento.FechaFinalizacion?.ToString("dd/MM/yyyy") ?? "No disponible",
                tratamiento.Observaciones ?? "No disponible"
            );
        }

        public static HMedicoDto ToDto(this HistorialMedico historialMedico)
        {
            // ImmutableList<ExamenDto> examenes = historialMedico.Examen.Select(e => e.ToDto()).ToImmutableList();
            // ImmutableList<TratamientoDto> tratamientos = historialMedico.Tratamientos.Select(t => t.ToDto()).ToImmutableList();

            return new HMedicoDto(
                historialMedico.IdHistorialMedico,
                historialMedico.IdPacienteNavigation!.ToDto(includeId: false, includeAge: true), // Indicamos que no queremos incluir el Id del paciente
                historialMedico.Examen?.Select(e => e.ToDto()).ToImmutableList() ?? ImmutableList<ExamenDto>.Empty,
                historialMedico.Tratamientos?.Select(t => t.ToDto()).ToImmutableList() ?? ImmutableList<TratamientoDto>.Empty
            );
        }

        public static string ToCompleteName(this Persona persona)
        {
            return $"{persona.Nombres} {persona.ApellidoPaterno} {persona.ApellidoMaterno}";
        }

        public static string CalculateAge(this DateTime birthDate)
        {
            var today = DateTime.Today;
            int years = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-years))
                years--;

            int months = today.Month - birthDate.Month;
            if (birthDate.Day > today.Day)
                months--;
            if (months < 0)
                months += 12;

            int days = (today - birthDate.AddYears(years).AddMonths(months)).Days;

            return $"{years} años, {months} meses y {days} días";
        }
    }
}