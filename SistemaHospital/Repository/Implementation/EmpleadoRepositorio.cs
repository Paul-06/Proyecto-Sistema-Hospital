using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class EmpleadoRepositorio : Repositorio<Empleado>, IEmpleadoRepositorio
    {
        // Atributo para el DbContext
        private readonly BdHospitalContext _context;

        // Inyección en el constructor
        public EmpleadoRepositorio(BdHospitalContext context) : base(context) // Enviar context al padre
        {
            _context = context;
        }

        public void Actualizar(Empleado empleado)
        {
            // Obtener el registro a actualizar
            var registro = _context.Empleados.FirstOrDefault(e => e.IdEmpleado == empleado.IdEmpleado);

            if (registro != null) // Si se encontró el registro
            {
                registro.IdEspecialidad = empleado.IdEspecialidad;
                registro.IdCargo = empleado.IdCargo;
                registro.IdTipoEmpleado = empleado.IdTipoEmpleado;
            }
        }

        public IEnumerable<SelectListItem>? ObtenerOpcionesDropdownPorTipo(string tipo)
        {
            switch (tipo)
            {
                case "Cargo":
                    return _context.Cargos.Select(c => new SelectListItem{
                        Text = c.Nombre,
                        Value = c.IdCargo.ToString() // Value acepta valores de tipo string
                    });

                case "TipoEmpleado":
                    return _context.TipoEmpleados.Select(te => new SelectListItem{
                        Text = te.Nombre,
                        Value = te.IdTipoEmpleado.ToString()
                    });
                
                case "Especialidad":
                    return _context.Especialidads.Select(e => new SelectListItem{
                        Text = e.Nombre,
                        Value = e.IdEspecialidad.ToString()
                    });
                    
                default:
                    return null;
            }
        }
    }
}