using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Maps
{
    public class SucursalMap : IEntityTypeConfiguration<Sucursal>
    {
        public void Configure(EntityTypeBuilder<Sucursal> builder)
        {
            builder.ToTable("Sucursales");
            builder.HasKey(e => e.SucursalId).HasName("PK_Sucursales_sucursalId");

            builder.HasIndex(e => e.SucursalNombre, "UQ_Sucursales_sucursalNombre").IsUnique();

            builder.Property(e => e.SucursalId).HasColumnName("sucursalId");
            builder.Property(e => e.Activo).HasDefaultValueSql("((1))");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.SucursalNombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sucursalNombre");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            builder.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.SucursaleUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sucursales_usuarioCreacion_Usuarios_usuarioId");

            builder.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.SucursaleUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Sucursales_usuarioModificacion_Usuarios_usuarioId");
        }
    }
}
