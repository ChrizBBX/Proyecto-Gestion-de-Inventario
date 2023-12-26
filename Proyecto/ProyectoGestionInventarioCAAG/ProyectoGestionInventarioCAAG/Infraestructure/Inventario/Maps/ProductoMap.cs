using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Maps
{
    public class ProductoMap : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");
            builder.HasKey(e => e.ProductoId).HasName("PK_Productos_productoId");

            builder.Property(e => e.ProductoId).HasColumnName("productoId");
            builder.Property(e => e.Activo).HasDefaultValueSql("((1))");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.ProductoNombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("productoNombre");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            builder.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.ProductoUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_usuarioCreacion_Usuarios_usuarioId");

            builder.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.ProductoUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Productos_usuarioModificacion_Usuarios_usuarioId");
        }
    }
}
