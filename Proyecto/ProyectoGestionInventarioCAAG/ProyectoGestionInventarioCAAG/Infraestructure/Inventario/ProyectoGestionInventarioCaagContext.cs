using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Entities;
using ProyectoGestionInventarioCAAG.Infraestructure.Inventario.Maps;

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

    public virtual DbSet<PerfilesPorPermiso> PefilesPorPermisos { get; set; }

    public virtual DbSet<Perfil> Perfiles { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Salida> Salidas { get; set; }

    public virtual DbSet<SalidasDetalle> SalidasDetalles { get; set; }

    public virtual DbSet<Sucursal> Sucursales { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmpleadoMap());
        modelBuilder.ApplyConfiguration(new EstadosSalidaMap());
        modelBuilder.ApplyConfiguration(new LoteMap());
        modelBuilder.ApplyConfiguration(new PerfilesPorPermisoMap());
        modelBuilder.ApplyConfiguration(new PerfilMap());
        modelBuilder.ApplyConfiguration(new PermisoMap());
        modelBuilder.ApplyConfiguration(new ProductoMap());
        modelBuilder.ApplyConfiguration(new SalidaMap());
        modelBuilder.ApplyConfiguration(new SalidasDetalleMap());
        modelBuilder.ApplyConfiguration(new SucursalMap());
        modelBuilder.ApplyConfiguration(new UsuariosMap());
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
