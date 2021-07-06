using Dale.BackEnd.DBModel;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Dale.BackEnd.Context
{
    public partial class DaleContext : DbContext
    {
        public DaleContext()
        {
        }

        public DaleContext(DbContextOptions<DaleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Ventum> Venta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CliId)
                    .IsClustered(false);

                entity.Property(e => e.CliId).ValueGeneratedNever();

                entity.Property(e => e.CliApellidos).IsUnicode(false);

                entity.Property(e => e.CliDocumento).IsUnicode(false);

                entity.Property(e => e.CliNombres).IsUnicode(false);

                entity.Property(e => e.CliNumeroTelefono).IsUnicode(false);
            });

            modelBuilder.Entity<DetalleVentum>(entity =>
            {
                entity.HasKey(e => e.DvId)
                    .IsClustered(false);

                entity.Property(e => e.DvId).ValueGeneratedNever();

                entity.HasOne(d => d.Prd)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.PrdId)
                    .HasConstraintName("FK_DETALLE__PRODUCTO__PRODUCTO");

                entity.HasOne(d => d.Vn)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.VnId)
                    .HasConstraintName("FK_DETALLE__VENTA_DET_VENTA");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.PrdId)
                    .IsClustered(false);

                entity.Property(e => e.PrdId).ValueGeneratedNever();

                entity.Property(e => e.PrdNombre).IsUnicode(false);
            });

            modelBuilder.Entity<Ventum>(entity =>
            {
                entity.HasKey(e => e.VnId)
                    .IsClustered(false);

                entity.Property(e => e.VnId).ValueGeneratedNever();

                entity.HasOne(d => d.Cli)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.CliId)
                    .HasConstraintName("FK_VENTA_VENTA_CLI_CLIENTE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
