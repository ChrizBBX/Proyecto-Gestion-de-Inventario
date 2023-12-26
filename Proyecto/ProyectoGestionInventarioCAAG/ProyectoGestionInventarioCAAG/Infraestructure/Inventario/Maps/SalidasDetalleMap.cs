using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Maps
{
    public class SalidasDetalleMap : IEntityTypeConfiguration<SalidasDetalle>
    {
        public void Configure(EntityTypeBuilder<SalidasDetalle> builder)
        {
            builder.ToTable("SalidasDetalles");
            builder.HasKey(e => e.SalidaDetalle).HasName("PK_SalidasDetalle_salidaDetalle");

            builder.ToTable("SalidasDetalle");

            builder.Property(e => e.SalidaDetalle).HasColumnName("salidaDetalle");
            builder.Property(e => e.Activo).HasDefaultValueSql("((1))");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.LoteId).HasColumnName("loteId");
            builder.Property(e => e.SalidaDetalleCantidad).HasColumnName("salidaDetalleCantidad");
            builder.Property(e => e.SalidaId).HasColumnName("salidaId");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            builder.HasOne(d => d.Lote).WithMany(p => p.SalidasDetalles)
                .HasForeignKey(d => d.LoteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalidasDetalle_loteId_Lotes_loteId");

            builder.HasOne(d => d.Salida).WithMany(p => p.SalidasDetalles)
                .HasForeignKey(d => d.SalidaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalidasDetalle_salidaId_Salidas_salidaId");

            builder.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.SalidasDetalleUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_salidasDetalle_usuarioCreacion_Usuarios_usuarioId");

            builder.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.SalidasDetalleUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_salidasDetalle_usuarioModificacion_Usuarios_usuarioId");
        }
    }
}
