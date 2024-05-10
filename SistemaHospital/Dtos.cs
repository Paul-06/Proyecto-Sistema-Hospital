using System.Collections.Immutable;

namespace SistemaHospital.Dtos;

// Aquí se almcenarán los DTOs
// para listar objetos

public record EmpleadoDto(
    int IdEmpleado,
    string Dni,
    string NombreCompleto,
    string Celular,
    string Correo,
    string Direccion,
    string Cargo,
    string TipoEmpleado,
    string Especialidad
);

public record PacienteDto(
    int? IdPaciente, // Id del paciente opcional
    string Dni,
    string NombreCompleto,
    string FechaNacimiento,
    string? Edad,
    string Celular,
    string Correo,
    string Direccion
);

public record ResultadoDto(
    string FechaEntrega,
    string Descripcion
);

public record ExamenDto(
    string TipoExamen,
    string FechaSolicitud,
    string FechaAplicacion,
    string Observaciones,
    ImmutableList<ResultadoDto> Resultados
);

public record TratamientoDto(
    string TipoTratamiento,
    string FechaInicio,
    string FechaFin,
    string Observaciones
);

public record HMedicoDto(
    int IdHMedico,
    PacienteDto Paciente,
    ImmutableList<ExamenDto> Examenes,
    ImmutableList<TratamientoDto> Tratamientos
);
