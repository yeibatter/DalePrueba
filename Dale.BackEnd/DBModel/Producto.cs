using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Dale.BackEnd.DBModel
{
    [Table("PRODUCTO")]
    public partial class Producto
    {
        public Producto()
        {
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        [Key]
        [Column("PRD_ID")]
        public Guid PrdId { get; set; }
        [Required]
        [Column("PRD_NOMBRE")]
        [StringLength(100)]
        public string PrdNombre { get; set; }
        [Column("PRD_VALOR")]
        public long PrdValor { get; set; }

        [InverseProperty(nameof(DetalleVentum.Prd))]
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
