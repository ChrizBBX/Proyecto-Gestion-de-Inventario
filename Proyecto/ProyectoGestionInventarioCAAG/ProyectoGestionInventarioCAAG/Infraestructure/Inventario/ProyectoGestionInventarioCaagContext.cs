using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;

namespace ProyectoGestionInventarioCAAG.Infraestructure.Inventario;

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

    public virtual DbSet<Sucursal> Sucursales { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.builder<Perfile>(builder =>
        {
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
        });

        modelBuilder.builder<Permiso>(builder =>
        {
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
        });

        modelBuilder.builder<Producto>(builder =>
        {
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
        });

        modelBuilder.builder<Salida>(builder =>
        {
            builder.HasKey(e => e.SalidaId).HasName("PK_Salidas_salidaId");

            builder.Property(e => e.SalidaId).HasColumnName("salidaId");
            builder.Property(e => e.Activo).HasDefaultValueSql("((1))");
            builder.Property(e => e.EstadoId).HasColumnName("estadoId");
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
        });

        modelBuilder.builder<SalidasDetalle>(builder =>
        {
            builder.HasKey(e => e.SalidaDetalle).HasName("PK_SalidasDetalle_salidaDetalle");

            builder.ToTable("SalidasDetalle");

            builder.Property(e => e.SalidaDetalle).HasColumnName("salidaDetalle");
            builder.Property(e => e.Activo).HasDefaultValueSql("((1))");
            builder.Property(e => e.FechaCreacion).HasColumnType("datetime");
            builder.Property(e => e.FechaModificacion).HasColumnType("datetime");
            builder.Property(e => e.LoteId).HasColumnName("loteId");
            builder.Property(e => e.SalidaDetalleCantidad).HasColumnName("salidaDetalleCantidad");
            builder.Property(e => e.SalidaId).HasColumnName("salidaId");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("usuarioCreacion");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("usuarioModificacion");

            builder.HasOne(d => d.Lote).WithMany(p => p.SalidasDetalles)
                .HasForeignKey(d => d.LoteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalidasDetalle_loteId_Lotes_loteId");

            builder.HasOne(d => d.Salida).WithMany(p => p.SalidasDetalles)
                .HasForeignKey(d => d.SalidaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalidasDetalle_salidaId_Salidas_salidaId");

            builder.HasOne(d => d.UsuarioCreacionNavigation).WithMany(p => p.SalidasDetalleUsuarioCreacionNavigations)
                .HasForeignKey(d => d.UsuarioCreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_salidasDetalle_usuarioCreacion_Usuarios_usuarioId");

            builder.HasOne(d => d.UsuarioModificacionNavigation).WithMany(p => p.SalidasDetalleUsuarioModificacionNavigations)
                .HasForeignKey(d => d.UsuarioModificacion)
                .HasConstraintName("FK_salidasDetalle_usuarioModificacion_Usuarios_usuarioId");
        });

        modelBuilder.builder<Sucursal>(builder =>
        {
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
