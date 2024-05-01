using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int? IdPaciente { get; set; }

    public DateTime? FechaEmision { get; set; }

    public decimal? Total { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual Paciente? IdPacienteNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
