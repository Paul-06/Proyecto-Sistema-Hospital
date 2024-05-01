using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class Pago
{
    public int IdPago { get; set; }

    public int? IdFactura { get; set; }

    public int? IdTipoPago { get; set; }

    public DateTime? FechaPago { get; set; }

    public decimal? Monto { get; set; }

    public virtual Factura? IdFacturaNavigation { get; set; }

    public virtual TipoPago? IdTipoPagoNavigation { get; set; }
}
