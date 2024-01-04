using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Maps
{
    public class SalidaMap : IEntityTypeConfiguration<Salida>
    {
        public void Configure(EntityTypeBuilder<Salida> builder)
        {
            builder.ToTable("Salidas");
            builder.HasKey(e => e.SalidaId).HasName("PK_Salidas_salidaId");

            builder.Property(e => e.SalidaId).HasColumnName("salidaId");
            builder.Property(e => e.Activo).HasDefaultValueSql("((1))");
            builder.Property(e => e.EstadoSalidaId).HasColumnName("estadoSalidaId");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.SalidaFecha)
                .HasColumnType("datetime")
                .HasColumnName("salidaFecha");
            builder.Property(e => e.SalidaFechaRecibido)
                .HasColumnType("datetime")
                .HasColumnName("salidaFechaRecibido");
            builder.Property(e => e.SalidaTotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("salidaTotal");
            builder.Property(e => e.SucursalId).HasColumnName("sucursalId");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            builder.Property(e => e.UsuarioId).HasColumnName("usuarioId");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            builder.HasOne(d => d.EstadoSalida).WithMany(p => p.Salida)
                .HasForeignKey(d => d.EstadoSalidaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salidas_estadoSalidaId_EstadosSalidas_estadoSalidaId");

            builder.HasOne(d => d.Sucursal).WithMany(p => p.Salida)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salidas_sucursalId_Sucursales_sucursalId");

            builder.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.SalidaUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salidas_usuarioCreacion_Usuarios_usuarioId");

            builder.HasOne(d => d.Usuario).WithMany(p => p.SalidaUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salidas_UsuarioId_Usuarios_usuarioId");

            builder.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.SalidaUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Salidas_usuarioModificacion_Usuarios_usuarioId");
        }
    }
}
