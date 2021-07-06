using System;
using System.Collections.Generic;

namespace Dale.Model
{
    public class Venta
    {
        public string Id { get; set; }
        public string ClienteId { get; set; }
        public DateTime FechaVenta { get; set; }
        public long TotalFactura { get; set; }
        public IList<DetalleVenta> Detalle { get; set; }
    }
}
