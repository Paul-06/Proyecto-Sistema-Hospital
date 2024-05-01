using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class DetalleFactura
{
    public int IdDetalle { get; set; }

    public int? IdFactura { get; set; }

    public string? Descripcion { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual Factura? IdFacturaNavigation { get; set; }
}
