using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaHospital.Models.ViewModels
{
    public class EmpleadoVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El DNI es requerido")]
        [MaxLength(8, ErrorMessage = "El DNI debe ser de 8 dígitos máximo")]
        public string? Dni { get; set; }

        [Required(ErrorMessage = "El apellido paterno es requerido")]
        [MaxLength(100, ErrorMessage = "El apellido paterno no debe superar los 100 caracteres")]
        public string? ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "El apellido materno es requerido")]
        [MaxLength(100, ErrorMessage = "El apellido materno no debe superar los 100 caracteres")]
        public string? ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres")]
        public string? Nombres { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        public DateTime? FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El número de celular es requerido")]
        [MaxLength(9, ErrorMessage = "El número de celular debe ser de 9 dígitos máximo")]
        public string? Celular { get; set; }

        [MaxLength(200, ErrorMessage = "El correo no debe superar los 200 caracteres")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "La dirección es requerida")]
        [MaxLength(255, ErrorMessage = "La dirección no debe superar los 255 caracteres")]
        public string? Direccion { get; set; }

        public int? IdPersona { get; set; }

        [Required(ErrorMessage = "El cargo es requerido")]
        public int? IdCargo { get; set; }

        [Required(ErrorMessage = "La especialidad es requerida")]
        public int? IdEspecialidad { get; set; }

        [Required(ErrorMessage = "El tipo de empleado es requerido")]
        public int? IdTipoEmpleado { get; set; }

        public IEnumerable<SelectListItem>? CargoLista { get; set; }

        public IEnumerable<SelectListItem>? TipoEmpleadoLista { get; set; }

        public IEnumerable<SelectListItem>? EspecialidadLista { get; set; }
    }
}