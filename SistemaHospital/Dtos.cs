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
    int IdPaciente,
    string Dni,
    string NombreCompleto,
    string FechaNacimiento,
    string Celular,
    string Correo,
    string Direccion
);
