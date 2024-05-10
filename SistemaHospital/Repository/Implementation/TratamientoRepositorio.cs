using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class TratamientoRepositorio : Repositorio<Tratamiento>, ITratamientoRepositorio
    {
        // Atributo para el DbContext
        private readonly BdHospitalContext _context;

        // Inyección en el constructor
        public TratamientoRepositorio(BdHospitalContext context) : base(context) // Enviar el context al padre
        {
            _context = context;
        }

        public void Actualizar(Tratamiento tratamiento)
        {
            // Obtener el registro a actualizar
            var registro = _context.Tratamientos.FirstOrDefault(t => t.IdTratamiento == tratamiento.IdTratamiento);

            if (registro != null) // Si se encontró el registro
            {
                registro.IdHistorialMedico = tratamiento.IdHistorialMedico;
                registro.IdTipoTratamiento = tratamiento.IdTipoTratamiento;
                registro.FechaSolicitud = tratamiento.FechaSolicitud;
                registro.FechaInicio = tratamiento.FechaInicio;
                registro.FechaFinalizacion = tratamiento.FechaFinalizacion;
                registro.Observaciones = tratamiento.Observaciones;
            }
        }

        public IEnumerable<SelectListItem>? ObtenerOpcionesDropdown()
        {
            return _context.TipoTratamientos.Select(tt => new SelectListItem{
                Text = tt.Nombre,
                Value = tt.IdTipoTratamiento.ToString()
            });
        }
    }
}