using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Dale.BackEnd.DBModel
{
    [Table("DETALLE_VENTA")]
    [Index(nameof(PrdId), Name = "PRODUCTO_VENTA_FK")]
    [Index(nameof(VnId), Name = "VENTA_DETALLE_FK")]
    public partial class DetalleVentum
    {
        [Key]
        [Column("DV_ID")]
        public Guid DvId { get; set; }
        [Column("PRD_ID")]
        public Guid? PrdId { get; set; }
        [Column("VN_ID")]
        public Guid? VnId { get; set; }
        [Column("DV_CANTIDAD")]
        public int DvCantidad { get; set; }

        [ForeignKey(nameof(PrdId))]
        [InverseProperty(nameof(Producto.DetalleVenta))]
        public virtual Producto Prd { get; set; }
        [ForeignKey(nameof(VnId))]
        [InverseProperty(nameof(Ventum.DetalleVenta))]
        public virtual Ventum Vn { get; set; }
    }
}
