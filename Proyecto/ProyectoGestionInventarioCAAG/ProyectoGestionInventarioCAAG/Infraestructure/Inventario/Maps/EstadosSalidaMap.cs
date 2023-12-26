using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Maps
{
    public class EstadosSalidaMap : IEntityTypeConfiguration<EstadosSalida>
    {
        public void Configure(EntityTypeBuilder<EstadosSalida> builder)
        {
            builder.ToTable("EstadosSalidos");
            builder.HasKey(e => e.EstadoSalidaId).HasName("PK_EstadosSalidas_estadoSalidaId");

            builder.HasIndex(e => e.EstadoSalidaNombre, "UQ_EstadosSalidas_estadoSalidaNombre").IsUnique();

            builder.Property(e => e.EstadoSalidaId).HasColumnName("estadoSalidaId");
            builder.Property(e => e.Activo).HasDefaultValueSql("((1))");
            builder.Property(e => e.EstadoSalidaNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("estadoSalidaNombre");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            builder.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.EstadosSalidaUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EstadosSalidas_usuarioCreacion_Usuarios_usuarioId");

            builder.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.EstadosSalidaUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_EstadosSalidas_usuarioModificacion_Usuarios_usuarioId");
        }
    }
}
