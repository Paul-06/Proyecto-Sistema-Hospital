using SistemaHospital.Models;
using SistemaHospital.Repository.Abstract;

namespace SistemaHospital.Repository.Implementation
{
    public class TipoPagoRepositorio : Repositorio<TipoPago>, ITipoPagoRepositorio
    {
        // Atributo para el DbContext
        private readonly BdHospitalContext _context;

        // Inyección en el constructor
        public TipoPagoRepositorio(BdHospitalContext context) : base(context) // Enviar el context al padre (Repositorio)
        {
            _context = context;
        }

        public void Actualizar(TipoPago tipoPago)
        {
            var registro = _context.TipoPagos.FirstOrDefault(tp => tp.IdTipoPago == tipoPago.IdTipoPago); // Buscar el registro a actualizar

            if (registro != null) // Si se encontró el registro
            {
                registro.Descripcion = tipoPago.Descripcion;

                _context.SaveChanges();
            }
        }
    }
}