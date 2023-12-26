using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Maps
{
    public class EmpleadoMap : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.ToTable("Empleados");
            builder.HasKey(e => e.EmpleadoId).HasName("PK_Empleados_empleadoId");

            builder.Property(e => e.EmpleadoId).HasColumnName("empleadoId");
            builder.Property(e => e.Activo).HasDefaultValueSql("((1))");
            builder.Property(e => e.EmpleadoApellido)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("empleadoApellido");
            builder.Property(e => e.EmpleadoFechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("empleadoFechaNacimiento");
            builder.Property(e => e.EmpleadoNombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("empleadoNombre");
            builder.Property(e => e.EmpleadoSexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("empleadoSexo");
            builder.Property(e => e.EmpleadoTelefono)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("empleadoTelefono");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            builder.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.EmpleadoUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleados_usuarioCreacion");

            builder.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.EmpleadoUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Empleados_usuarioModificacion");
        }
    }
}
