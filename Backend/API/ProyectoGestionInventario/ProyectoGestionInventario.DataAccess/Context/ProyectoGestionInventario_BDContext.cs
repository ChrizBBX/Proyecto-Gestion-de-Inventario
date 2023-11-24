﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProyectoGestionInventario.Entities.Entities;

namespace ProyectoGestionInventario.DataAccess.Context;

public partial class ProyectoGestionInventario_BDContext : DbContext
{
    public ProyectoGestionInventario_BDContext()
    {
    }

    public ProyectoGestionInventario_BDContext(DbContextOptions<ProyectoGestionInventario_BDContext> options)
        : base(options)
    {
    }

    public virtual DbSet<tbProducto> tbProductos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tbProducto>(entity =>
        {
            entity.HasKey(e => e.prod_Id).HasName("PK_inve_tbProductos_prod_Id");

            entity.ToTable("tbProductos", "inve");

            entity.Property(e => e.prod_Descripcion).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}