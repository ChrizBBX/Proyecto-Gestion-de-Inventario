using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Maps
{
    public class UsuariosMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(e => e.UsuarioId).HasName("PK_Usuarios_usuarioId");

            builder.Property(e => e.UsuarioId).HasColumnName("usuarioId");
            builder.Property(e => e.Activo).HasDefaultValueSql("((1))");
            builder.Property(e => e.EmpleadoId).HasColumnName("empleadoId");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.PerfilId).HasColumnName("perfilId");
            builder.Property(e => e.UsuarioContrasena)
                .IsUnicode(false)
                .HasColumnName("usuarioContrasena");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");
            builder.Property(e => e.UsuarioNombreUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("usuarioNombreUsuario");

            builder.HasOne(d => d.Empleado).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_empleadoId_Empleados_empleadoId");

            builder.HasOne(d => d.Perfil).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.PerfilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_perfilId_Perfiles_perfilId");

            builder.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.InverseUsuarioCreacionNavigation)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_usuarioCreacion_Usuarios_usuarioId");

            builder.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.InverseUsuarioModificacionNavigation)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Usuarios_usuarioModificacion_Usuarios_usuarioId");
        }
    }
}
