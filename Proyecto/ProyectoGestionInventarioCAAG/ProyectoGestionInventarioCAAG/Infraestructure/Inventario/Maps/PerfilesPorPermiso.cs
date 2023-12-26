using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Maps
{
    public class PerfilesPorPermiso : IEntityTypeConfiguration<PerfilesPorPermiso>
    {
        public void Configure(EntityTypeBuilder<PerfilesPorPermiso> builder)
        {
            builder.HasKey(e => e.PerfilPorPermisoId).HasName("PK_PerfilesPorPermisos_perfilPorPermisoId");

            builder.Property(e => e.PerfilPorPermisoId)
                .ValueGeneratedNever()
                .HasColumnName("perfilPorPermisoId");
            builder.Property(e => e.Activo).HasDefaultValueSql("((1))");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.PerfilId).HasColumnName("perfilId");
            builder.Property(e => e.PermisoId).HasColumnName("permisoId");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            builder.HasOne(d => d.Perfil).WithMany(p => p.PefilesPorPermisos)
                .HasForeignKey(d => d.PerfilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PerfilesPorPermisos_perfilId_Perfiles_perfilId");

            builder.HasOne(d => d.Permiso).WithMany(p => p.PefilesPorPermisos)
                .HasForeignKey(d => d.PermisoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PerfilesPorPermisos_permisoId_Permisos_permisoId");

            builder.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.PefilesPorPermisoUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PerfilesPorPermisos_usuarioCreacion_Usuarios_usuarioId");

            builder.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.PefilesPorPermisoUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_PerfilesPorPermisos_usuarioModificacion_Usuarios_usuarioId");
        }
    }
}
