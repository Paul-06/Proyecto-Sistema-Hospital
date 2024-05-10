using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class ResultadoRepositorio : Repositorio<Resultado>, IResultadoRepositorio
    {
        // Atributo para el DbContext
        private readonly BdHospitalContext _context;

        // Inyección en el constructor
        public ResultadoRepositorio(BdHospitalContext context) : base(context) // Enviar context al padre
        {
            _context = context;
        }

        public void Actualizar(Resultado resultado)
        {
            // Obtener el registro a actualizar
            var registro = _context.Resultados.FirstOrDefault(r => r.IdResultado == resultado.IdResultado);

            if (registro != null) // Si se encontró el registro
            {
                registro.IdExamen = resultado.IdExamen;
                registro.FechaEntrega = resultado.FechaEntrega;
                registro.Descripcion = resultado.Descripcion;
            }
        }
    }
}