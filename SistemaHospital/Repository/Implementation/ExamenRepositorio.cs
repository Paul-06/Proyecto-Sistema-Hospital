using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class ExamenRepositorio : Repositorio<Examan>, IExamenRepositorio
    {
        // Atributo para el DbContext
        private readonly BdHospitalContext _context;

        // Inyección en el constructor
        public ExamenRepositorio(BdHospitalContext context) : base(context) // Enviar el context al padre
        {
            _context = context;
        }

        public void Actualizar(Examan examen)
        {
            // Obtener el registro a actualizar
            var registro = _context.Examen.FirstOrDefault(e => e.IdExamen == examen.IdExamen);

            if (registro != null) // Si se encontró el registro
            {
                registro.IdHistorialMedico = examen.IdHistorialMedico;
                registro.FechaSolicitud = examen.FechaSolicitud;
                registro.FechaAplicacion = examen.FechaAplicacion;
                registro.Observaciones = examen.Observaciones;
                registro.IdTipoExamen = examen.IdTipoExamen;
            }
        }

        public IEnumerable<SelectListItem>? ObtenerTiposDropdown()
        {
            return _context.TipoExamen.Select(te => new SelectListItem{
                Text = te.Nombre,
                Value = te.IdTipoExamen.ToString()
            });
        }
    }
}