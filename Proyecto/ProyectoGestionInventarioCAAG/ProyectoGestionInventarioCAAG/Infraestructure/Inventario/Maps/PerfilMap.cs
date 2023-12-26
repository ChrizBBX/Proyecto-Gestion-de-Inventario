using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Maps
{
    public class PerfilMap : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("Perfiles");
            builder.HasKey(e => e.PerfilId).HasName("PK_Perfiles_perfilId");

            builder.HasIndex(e => e.PerfilNombre, "UQ_Perfiles_perfilNombre").IsUnique();

            builder.Property(e => e.PerfilId).HasColumnName("perfilId");
            builder.Property(e => e.Activo).HasDefaultValueSql("((1))");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.PerfilNombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("perfilNombre");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            builder.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.PerfileUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Perfiles_usuarioCreacion_Usuarios_usuarioId");

            builder.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.PerfileUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Perfiles_usuarioModificacion_Usuarios_usuarioId");
        }
    }
}
