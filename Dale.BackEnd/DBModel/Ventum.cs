using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Dale.BackEnd.DBModel
{
    [Table("VENTA")]
    [Index(nameof(CliId), Name = "VENTA_CLIENTE_FK")]
    public partial class Ventum
    {
        public Ventum()
        {
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        [Key]
        [Column("VN_ID")]
        public Guid VnId { get; set; }
        [Column("CLI_ID")]
        public Guid? CliId { get; set; }
        [Column("VN_FECHA", TypeName = "datetime")]
        public DateTime VnFecha { get; set; }
        [Column("VN_TOTAL_FACTURA")]
        public long? VnTotalFactura { get; set; }

        [ForeignKey(nameof(CliId))]
        [InverseProperty(nameof(Cliente.Venta))]
        public virtual Cliente Cli { get; set; }
        [InverseProperty(nameof(DetalleVentum.Vn))]
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
