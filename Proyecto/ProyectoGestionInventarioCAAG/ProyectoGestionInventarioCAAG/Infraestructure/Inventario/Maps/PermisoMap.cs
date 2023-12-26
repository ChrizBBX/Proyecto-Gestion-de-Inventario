using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Maps
{
    public class PermisoMap : IEntityTypeConfiguration<Permiso>
    {
        public void Configure(EntityTypeBuilder<Permiso> builder)
        {
            builder.ToTable("Permisos");
            builder.HasKey(e => e.PermisoId).HasName("PK_Permisos_permisoId");

            builder.Property(e => e.PermisoId).HasColumnName("permisoId");
            builder.Property(e => e.Activo).HasDefaultValueSql("((1))");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.PermisoNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("permisoNombre");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            builder.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.PermisoUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permisos_usuarioCreacion_Usuarios_usuarioId");

            builder.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.PermisoUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Permisos_usuarioModificacion_Usuarios_usuarioId");
        }
    }
}
