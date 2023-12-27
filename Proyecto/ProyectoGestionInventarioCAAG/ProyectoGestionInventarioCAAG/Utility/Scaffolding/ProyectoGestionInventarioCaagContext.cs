using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoGestionInventarioCAAG.Utility.Scaffolding;

public partial class ProyectoGestionInventarioCaagContext : DbContext
{
    public ProyectoGestionInventarioCaagContext()
    {
    }

    public ProyectoGestionInventarioCaagContext(DbContextOptions<ProyectoGestionInventarioCaagContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EstadosSalida> EstadosSalidas { get; set; }

    public virtual DbSet<Lote> Lotes { get; set; }

    public virtual DbSet<PefilesPorPermiso> PefilesPorPermisos { get; set; }

    public virtual DbSet<Perfile> Perfiles { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Salida> Salidas { get; set; }

    public virtual DbSet<SalidasDetalle> SalidasDetalles { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.1.33\\\\\\\\academiagfs,49194;Database=ProyectoGestionInventarioCAAG;User id=academiadev;Password=Demia#20;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadoId).HasName("PK_Empleados_empleadoId");

            entity.HasIndex(e => e.EmpleadoIdentidad, "UQ_Empleados_empleadoIdentidad").IsUnique();

            entity.Property(e => e.EmpleadoId).HasColumnName("empleadoId");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.EmpleadoApellido)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("empleadoApellido");
            entity.Property(e => e.EmpleadoFechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("empleadoFechaNacimiento");
            entity.Property(e => e.EmpleadoIdentidad)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("empleadoIdentidad");
            entity.Property(e => e.EmpleadoNombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("empleadoNombre");
            entity.Property(e => e.EmpleadoSexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("empleadoSexo");
            entity.Property(e => e.EmpleadoTelefono)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("empleadoTelefono");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.EmpleadoUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleados_usuarioCreacion");

            entity.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.EmpleadoUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Empleados_usuarioModificacion");
        });

        modelBuilder.Entity<EstadosSalida>(entity =>
        {
            entity.HasKey(e => e.EstadoSalidaId).HasName("PK_EstadosSalidas_estadoSalidaId");

            entity.HasIndex(e => e.EstadoSalidaNombre, "UQ_EstadosSalidas_estadoSalidaNombre").IsUnique();

            entity.Property(e => e.EstadoSalidaId).HasColumnName("estadoSalidaId");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.EstadoSalidaNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("estadoSalidaNombre");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.EstadosSalidaUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EstadosSalidas_usuarioCreacion_Usuarios_usuarioId");

            entity.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.EstadosSalidaUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_EstadosSalidas_usuarioModificacion_Usuarios_usuarioId");
        });

        modelBuilder.Entity<Lote>(entity =>
        {
            entity.HasKey(e => e.LoteId).HasName("PK_Lotes_loteId");

            entity.Property(e => e.LoteId).HasColumnName("loteId");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.LoteCantidad).HasColumnName("loteCantidad");
            entity.Property(e => e.LoteCantidadInicial).HasColumnName("loteCantidadInicial");
            entity.Property(e => e.LoteCostoCantidad)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("loteCostoCantidad");
            entity.Property(e => e.LoteFechaVencimiento)
                .HasColumnType("date")
                .HasColumnName("loteFechaVencimiento");
            entity.Property(e => e.ProductoId).HasColumnName("productoId");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            entity.HasOne(d => d.Producto).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK_Lotes_productoId_Productos_productoId");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.LoteUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lotes_usurioCreacion_Usuarios_usuarioId");

            entity.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.LoteUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Lotes_usuarioModificacion_Usuarios_usuarioId");
        });

        modelBuilder.Entity<PefilesPorPermiso>(entity =>
        {
            entity.HasKey(e => e.PerfilPorPermisoId).HasName("PK_PerfilesPorPermisos_perfilPorPermisoId");

            entity.Property(e => e.PerfilPorPermisoId)
                .ValueGeneratedNever()
                .HasColumnName("perfilPorPermisoId");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.PerfilId).HasColumnName("perfilId");
            entity.Property(e => e.PermisoId).HasColumnName("permisoId");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            entity.HasOne(d => d.Perfil).WithMany(p => p.PefilesPorPermisos)
                .HasForeignKey(d => d.PerfilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PerfilesPorPermisos_perfilId_Perfiles_perfilId");

            entity.HasOne(d => d.Permiso).WithMany(p => p.PefilesPorPermisos)
                .HasForeignKey(d => d.PermisoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PerfilesPorPermisos_permisoId_Permisos_permisoId");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.PefilesPorPermisoUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PerfilesPorPermisos_usuarioCreacion_Usuarios_usuarioId");

            entity.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.PefilesPorPermisoUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_PerfilesPorPermisos_usuarioModificacion_Usuarios_usuarioId");
        });

        modelBuilder.Entity<Perfile>(entity =>
        {
            entity.HasKey(e => e.PerfilId).HasName("PK_Perfiles_perfilId");

            entity.HasIndex(e => e.PerfilNombre, "UQ_Perfiles_perfilNombre").IsUnique();

            entity.Property(e => e.PerfilId).HasColumnName("perfilId");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.PerfilNombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("perfilNombre");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.PerfileUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Perfiles_usuarioCreacion_Usuarios_usuarioId");

            entity.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.PerfileUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Perfiles_usuarioModificacion_Usuarios_usuarioId");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.PermisoId).HasName("PK_Permisos_permisoId");

            entity.Property(e => e.PermisoId).HasColumnName("permisoId");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.PermisoNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("permisoNombre");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.PermisoUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permisos_usuarioCreacion_Usuarios_usuarioId");

            entity.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.PermisoUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Permisos_usuarioModificacion_Usuarios_usuarioId");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK_Productos_productoId");

            entity.Property(e => e.ProductoId).HasColumnName("productoId");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.ProductoNombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("productoNombre");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.ProductoUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_usuarioCreacion_Usuarios_usuarioId");

            entity.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.ProductoUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Productos_usuarioModificacion_Usuarios_usuarioId");
        });

        modelBuilder.Entity<Salida>(entity =>
        {
            entity.HasKey(e => e.SalidaId).HasName("PK_Salidas_salidaId");

            entity.Property(e => e.SalidaId).HasColumnName("salidaId");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.EstadoId).HasColumnName("estadoId");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.SalidaFecha)
                .HasColumnType("datetime")
                .HasColumnName("salidaFecha");
            entity.Property(e => e.SalidaFechaRecibido)
                .HasColumnType("datetime")
                .HasColumnName("salidaFechaRecibido");
            entity.Property(e => e.SalidaTotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("salidaTotal");
            entity.Property(e => e.SucursalId).HasColumnName("sucursalId");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Salida)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salidas_sucursalId_Sucursales_sucursalId");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.SalidaUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salidas_usuarioCreacion_Usuarios_usuarioId");

            entity.HasOne(d => d.Usuario).WithMany(p => p.SalidaUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salidas_UsuarioId_Usuarios_usuarioId");

            entity.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.SalidaUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Salidas_usuarioModificacion_Usuarios_usuarioId");
        });

        modelBuilder.Entity<SalidasDetalle>(entity =>
        {
            entity.HasKey(e => e.SalidaDetalle).HasName("PK_SalidasDetalle_salidaDetalle");

            entity.ToTable("SalidasDetalle");

            entity.Property(e => e.SalidaDetalle).HasColumnName("salidaDetalle");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.LoteId).HasColumnName("loteId");
            entity.Property(e => e.SalidaDetalleCantidad).HasColumnName("salidaDetalleCantidad");
            entity.Property(e => e.SalidaId).HasColumnName("salidaId");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            entity.HasOne(d => d.Lote).WithMany(p => p.SalidasDetalles)
                .HasForeignKey(d => d.LoteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalidasDetalle_loteId_Lotes_loteId");

            entity.HasOne(d => d.Salida).WithMany(p => p.SalidasDetalles)
                .HasForeignKey(d => d.SalidaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalidasDetalle_salidaId_Salidas_salidaId");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.SalidasDetalleUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_salidasDetalle_usuarioCreacion_Usuarios_usuarioId");

            entity.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.SalidasDetalleUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_salidasDetalle_usuarioModificacion_Usuarios_usuarioId");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.SucursalId).HasName("PK_Sucursales_sucursalId");

            entity.HasIndex(e => e.SucursalNombre, "UQ_Sucursales_sucursalNombre").IsUnique();

            entity.Property(e => e.SucursalId).HasColumnName("sucursalId");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.SucursalNombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sucursalNombre");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.SucursaleUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sucursales_usuarioCreacion_Usuarios_usuarioId");

            entity.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.SucursaleUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Sucursales_usuarioModificacion_Usuarios_usuarioId");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK_Usuarios_usuarioId");

            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");
            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.EmpleadoId).HasColumnName("empleadoId");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.PerfilId).HasColumnName("perfilId");
            entity.Property(e => e.UsuarioContrasena)
                .IsUnicode(false)
                .HasColumnName("usuarioContrasena");
            entity.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            entity.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");
            entity.Property(e => e.UsuarioNombreUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("usuarioNombreUsuario");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_empleadoId_Empleados_empleadoId");

            entity.HasOne(d => d.Perfil).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.PerfilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_perfilId_Perfiles_perfilId");

            entity.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.InverseUsuarioCreacionNavigation)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_usuarioCreacion_Usuarios_usuarioId");

            entity.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.InverseUsuarioModificacionNavigation)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_Usuarios_usuarioModificacion_Usuarios_usuarioId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
