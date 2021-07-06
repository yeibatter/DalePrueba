using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Dale.BackEnd.DBModel
{
    [Table("CLIENTE")]
    public partial class Cliente
    {
        public Cliente()
        {
            Venta = new HashSet<Ventum>();
        }

        [Key]
        [Column("CLI_ID")]
        public Guid CliId { get; set; }
        [Required]
        [Column("CLI_DOCUMENTO")]
        [StringLength(10)]
        public string CliDocumento { get; set; }
        [Required]
        [Column("CLI_NOMBRES")]
        [StringLength(100)]
        public string CliNombres { get; set; }
        [Required]
        [Column("CLI_APELLIDOS")]
        [StringLength(100)]
        public string CliApellidos { get; set; }
        [Column("CLI_NUMERO_TELEFONO")]
        [StringLength(10)]
        public string CliNumeroTelefono { get; set; }

        [InverseProperty(nameof(Ventum.Cli))]
        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
