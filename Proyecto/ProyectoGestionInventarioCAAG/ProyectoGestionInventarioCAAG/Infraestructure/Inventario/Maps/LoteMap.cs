using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Maps
{
    public class LoteMap : IEntityTypeConfiguration<Lote>
    {
        public void Configure(EntityTypeBuilder<Lote> builder)
        {
            builder.ToTable("Lotes");
            builder.HasKey(e => e.LoteId).HasName("PK_Lotes_loteId");

            builder.Property(e => e.LoteId).HasColumnName("loteId");
            builder.Property(e => e.Activo).HasDefaultValueSql("((1))");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.LoteCantidad).HasColumnName("loteCantidad");
            builder.Property(e => e.LoteCantidadInicial).HasColumnName("loteCantidadInicial");
            builder.Property(e => e.LoteCostoCantidad)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("loteCostoCantidad");
            builder.Property(e => e.LoteFechaVencimiento)
                .HasColumnType("date")
                .HasColumnName("loteFechaVencimiento");
            builder.Property(e => e.ProductoId).HasColumnName("productoId");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            builder.HasOne(d => d.Producto).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK_Lotes_productoId_Productos_productoId");

            builder.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.LoteUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lotes_usurioCreacion_Usuarios_usuarioId");

            builder.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.LoteUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Lotes_usuarioModificacion_Usuarios_usuarioId");
        }
    }
}
