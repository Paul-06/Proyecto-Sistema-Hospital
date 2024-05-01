using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string? Dni { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Nombres { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? Celular { get; set; }

    public string? Correo { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
