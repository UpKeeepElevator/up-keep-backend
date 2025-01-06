using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UpKeep.Data.Models;

namespace UpKeep.Data.Context;

public partial class UpKeepDbContext : DbContext
{
    public UpKeepDbContext()
    {
    }

    public UpKeepDbContext(DbContextOptions<UpKeepDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnexoChequeo> AnexoChequeos { get; set; }

    public virtual DbSet<Ascensor> Ascensors { get; set; }

    public virtual DbSet<AscensorRutum> AscensorRuta { get; set; }

    public virtual DbSet<Averium> Averia { get; set; }

    public virtual DbSet<Chequeo> Chequeos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Edificio> Edificios { get; set; }

    public virtual DbSet<EstadoSeccion> EstadoSeccions { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Mantenimiento> Mantenimientos { get; set; }

    public virtual DbSet<Prioridad> Prioridads { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Rutum> Ruta { get; set; }

    public virtual DbSet<Seccion> Seccions { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Solicitud> Solicituds { get; set; }

    public virtual DbSet<TipoAverium> TipoAveria { get; set; }

    public virtual DbSet<TipoSeccion> TipoSeccions { get; set; }

    public virtual DbSet<TipoServicio> TipoServicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("anexo_chequeo_tipo", new[] { "Video", "Foto" })
            .HasPostgresEnum("anexochequeo_anexo_tipo", new[] { "Video", "Foto" })
            .HasPostgresEnum("solicitud_estado", new[] { "Pendiente", "En progreso", "Completado" });

        modelBuilder.Entity<AnexoChequeo>(entity =>
        {
            entity.HasKey(e => e.AnexoId).HasName("AnexoChequeo_pkey");

            entity.ToTable("AnexoChequeo");

            entity.Property(e => e.AnexoId)
                .ValueGeneratedNever()
                .HasColumnName("anexoId");
            entity.Property(e => e.AnexoNombre)
                .HasMaxLength(255)
                .HasColumnName("anexo_nombre");
            entity.Property(e => e.AnexoPeso)
                .HasMaxLength(10)
                .HasColumnName("anexo_peso");
            entity.Property(e => e.AnexoRuta)
                .HasMaxLength(255)
                .HasColumnName("anexo_ruta");
            entity.Property(e => e.ChequeoId).HasColumnName("chequeoId");

            entity.HasOne(d => d.Chequeo).WithMany(p => p.AnexoChequeos)
                .HasForeignKey(d => d.ChequeoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("anexochequeo_chequeo_fk");
        });

        modelBuilder.Entity<Ascensor>(entity =>
        {
            entity.HasKey(e => e.AscensorId).HasName("Ascensor_pkey");

            entity.ToTable("Ascensor");

            entity.Property(e => e.AscensorId)
                .ValueGeneratedNever()
                .HasColumnName("ascensorId");
            entity.Property(e => e.Capacidad).HasColumnName("capacidad");
            entity.Property(e => e.EdificioId).HasColumnName("edificioId");
            entity.Property(e => e.Geolocalizacion)
                .HasMaxLength(100)
                .HasColumnName("geolocalizacion");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .HasColumnName("marca");
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .HasColumnName("modelo");
            entity.Property(e => e.NumeroPisos).HasColumnName("numeroPisos");
            entity.Property(e => e.TipoAscensor)
                .HasMaxLength(50)
                .HasColumnName("tipoAscensor");
            entity.Property(e => e.TipoDeUso)
                .HasMaxLength(50)
                .HasColumnName("tipo_de_uso");
            entity.Property(e => e.UbicacionEnEdificio)
                .HasMaxLength(50)
                .HasColumnName("ubicacion_en_edificio");

            entity.HasOne(d => d.Edificio).WithMany(p => p.Ascensors)
                .HasForeignKey(d => d.EdificioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ascensor_edificio_fk");
        });

        modelBuilder.Entity<AscensorRutum>(entity =>
        {
            entity.HasKey(e => new { e.RutaId, e.AscensorId }).HasName("Ascensor_ruta_pkey");

            entity.ToTable("Ascensor_ruta");

            entity.Property(e => e.RutaId)
                .HasMaxLength(10)
                .HasColumnName("rutaId");
            entity.Property(e => e.AscensorId).HasColumnName("ascensorId");
            entity.Property(e => e.FechaVisita).HasColumnName("fecha_visita");
            entity.Property(e => e.FechaVisitada).HasColumnName("fecha_visitada");
            entity.Property(e => e.Orden)
                .HasDefaultValue(0)
                .HasColumnName("orden");

            entity.HasOne(d => d.Ascensor).WithMany(p => p.AscensorRuta)
                .HasForeignKey(d => d.AscensorId)
                .HasConstraintName("ascensor_ruta_ascensor_fk");

            entity.HasOne(d => d.Ruta).WithMany(p => p.AscensorRuta)
                .HasForeignKey(d => d.RutaId)
                .HasConstraintName("ascensor_ruta_ruta_fk");
        });

        modelBuilder.Entity<Averium>(entity =>
        {
            entity.HasKey(e => e.AveriaId).HasName("Averias_pkey");

            entity.Property(e => e.AveriaId)
                .ValueGeneratedNever()
                .HasColumnName("averiaId");
            entity.Property(e => e.AscensorId).HasColumnName("ascensorId");
            entity.Property(e => e.ComentarioAveria).HasColumnName("comentarioAveria");
            entity.Property(e => e.ErrorEncontrado).HasColumnName("errorEncontrado");
            entity.Property(e => e.Evidencia)
                .HasMaxLength(255)
                .HasColumnName("evidencia");
            entity.Property(e => e.FechaReporte).HasColumnName("fechaReporte");
            entity.Property(e => e.FechaRespuesta).HasColumnName("fechaRespuesta ");
            entity.Property(e => e.Firma)
                .HasMaxLength(255)
                .HasColumnName("firma");
            entity.Property(e => e.Geolocalizacion)
                .HasMaxLength(100)
                .HasColumnName("geolocalizacion");
            entity.Property(e => e.ReparacionRealizada).HasColumnName("reparacionRealizada");
            entity.Property(e => e.SeccionAveria).HasColumnName("seccionAveria");
            entity.Property(e => e.TecnicoId).HasColumnName("tecnicoId");
            entity.Property(e => e.TiempoReparacion).HasColumnName("tiempoReparacion");
            entity.Property(e => e.TiempoRespuesta).HasColumnName("tiempoRespuesta");
            entity.Property(e => e.TipoAveriaId).HasColumnName("tipoAveriaId");

            entity.HasOne(d => d.Ascensor).WithMany(p => p.Averia)
                .HasForeignKey(d => d.AscensorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("averia_ascensor_fk");

            entity.HasOne(d => d.SeccionAveriaNavigation).WithMany(p => p.Averia)
                .HasForeignKey(d => d.SeccionAveria)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("averia_seccion_fk");

            entity.HasOne(d => d.Tecnico).WithMany(p => p.Averia)
                .HasForeignKey(d => d.TecnicoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("averia_usuario_fk");

            entity.HasOne(d => d.TipoAveria).WithMany(p => p.Averia)
                .HasForeignKey(d => d.TipoAveriaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("averia_tipoaveria_fk");
        });

        modelBuilder.Entity<Chequeo>(entity =>
        {
            entity.HasKey(e => e.ChequeoId).HasName("chequeo_pk");

            entity.ToTable("Chequeo");

            entity.Property(e => e.ChequeoId)
                .ValueGeneratedNever()
                .HasColumnName("chequeoId");
            entity.Property(e => e.ChequeoComentarios).HasColumnName("chequeo_comentarios");
            entity.Property(e => e.ChequeoFecha).HasColumnName("chequeo_fecha");
            entity.Property(e => e.ChequeoHora).HasColumnName("chequeo_hora");
            entity.Property(e => e.EstadoSeccionId).HasColumnName("estadoSeccionId");
            entity.Property(e => e.MantenimientoId).HasColumnName("mantenimientoId");
            entity.Property(e => e.SeccionId).HasColumnName("seccionId");

            entity.HasOne(d => d.EstadoSeccion).WithMany(p => p.Chequeos)
                .HasForeignKey(d => d.EstadoSeccionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("chequeo_estadoseccion_fk");

            entity.HasOne(d => d.Mantenimiento).WithMany(p => p.Chequeos)
                .HasForeignKey(d => d.MantenimientoId)
                .HasConstraintName("chequeo_mantenimiento_fk");

            entity.HasOne(d => d.Seccion).WithMany(p => p.Chequeos)
                .HasForeignKey(d => d.SeccionId)
                .HasConstraintName("chequeo_seccion_fk");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("Cliente_pkey");

            entity.ToTable("Cliente");

            entity.Property(e => e.ClienteId).HasColumnName("clienteId");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreContacto)
                .HasMaxLength(255)
                .HasColumnName("nombreContacto");
            entity.Property(e => e.Telefono)
                .HasMaxLength(16)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Edificio>(entity =>
        {
            entity.HasKey(e => e.EdificioId).HasName("Edificio_pkey");

            entity.ToTable("Edificio");

            entity.Property(e => e.EdificioId).HasColumnName("edificioId");
            entity.Property(e => e.ClienteId).HasColumnName("clienteId");
            entity.Property(e => e.Edificio1)
                .HasMaxLength(50)
                .HasColumnName("edificio");
            entity.Property(e => e.EdificioUbicacion)
                .HasMaxLength(100)
                .HasColumnName("edificioUbicacion");
            entity.Property(e => e.Geolocalizacion)
                .HasMaxLength(100)
                .HasColumnName("geolocalizacion");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Edificios)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("edificio_cliente_fk");
        });

        modelBuilder.Entity<EstadoSeccion>(entity =>
        {
            entity.HasKey(e => e.EstadoSeccionId).HasName("EstadoSeccion_pkey");

            entity.ToTable("EstadoSeccion");

            entity.Property(e => e.EstadoSeccionId).HasColumnName("estadoSeccionId");
            entity.Property(e => e.EstadoDescripcion).HasColumnName("estado_descripcion");
            entity.Property(e => e.EstadoNombre)
                .HasMaxLength(255)
                .HasColumnName("estado_nombre");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.FacturaId).HasName("Factura_pkey");

            entity.ToTable("Factura");

            entity.Property(e => e.FacturaId).HasColumnName("facturaId");
            entity.Property(e => e.ClienteId).HasColumnName("clienteId");
            entity.Property(e => e.FechaFactura).HasColumnName("fecha_factura");
            entity.Property(e => e.FechaPagado).HasColumnName("fecha_pagado");
            entity.Property(e => e.ImpuestoFactura)
                .HasPrecision(9, 2)
                .HasColumnName("impuesto_factura");
            entity.Property(e => e.MontoFactura)
                .HasPrecision(9, 2)
                .HasColumnName("monto_factura");
            entity.Property(e => e.TotalFactura)
                .HasPrecision(9, 2)
                .HasColumnName("total_factura");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("factura_cliente_fk");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.HasKey(e => e.MantenimientoId).HasName("Mantenimiento_pkey");

            entity.ToTable("Mantenimiento");

            entity.Property(e => e.MantenimientoId)
                .ValueGeneratedNever()
                .HasColumnName("mantenimientoId");
            entity.Property(e => e.AscensorId).HasColumnName("ascensorId");
            entity.Property(e => e.Duracion).HasColumnName("duracion");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Firma)
                .HasMaxLength(255)
                .HasColumnName("firma");
            entity.Property(e => e.Geolocalizacion)
                .HasMaxLength(100)
                .HasColumnName("geolocalizacion");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.TecnicoId).HasColumnName("tecnicoId");

            entity.HasOne(d => d.Ascensor).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.AscensorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("mantenimiento_ascensor_fk");

            entity.HasOne(d => d.Tecnico).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.TecnicoId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("mantenimiento_usuario_fk");
        });

        modelBuilder.Entity<Prioridad>(entity =>
        {
            entity.HasKey(e => e.PrioridadId).HasName("Prioridad_pkey");

            entity.ToTable("Prioridad");

            entity.Property(e => e.PrioridadId).HasColumnName("prioridadId");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.NombrePrioridad)
                .HasMaxLength(100)
                .HasColumnName("nombrePrioridad");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("Rol_pkey");

            entity.ToTable("Rol");

            entity.Property(e => e.RolId).HasColumnName("rol_Id");
            entity.Property(e => e.RolDescripcion)
                .HasMaxLength(255)
                .HasColumnName("rol_descripcion");

            entity.HasMany(d => d.Usuarios).WithMany(p => p.Rols)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioRol",
                    r => r.HasOne<Usuario>().WithMany()
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("usuario_rol_usuario_fk"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("RolId")
                        .HasConstraintName("usuario_rol_rol_fk"),
                    j =>
                    {
                        j.HasKey("RolId", "UsuarioId").HasName("Usuario_rol_pkey");
                        j.ToTable("Usuario_rol");
                        j.IndexerProperty<int>("RolId").HasColumnName("rol_Id");
                        j.IndexerProperty<int>("UsuarioId").HasColumnName("usuarioId");
                    });
        });

        modelBuilder.Entity<Rutum>(entity =>
        {
            entity.HasKey(e => e.RutaId).HasName("Ruta_pkey");

            entity.Property(e => e.RutaId)
                .HasMaxLength(10)
                .HasColumnName("rutaId");
            entity.Property(e => e.CantidadAscensores).HasColumnName("cantidadAscensores");
            entity.Property(e => e.CantidadVisitas).HasColumnName("cantidadVisitas");
            entity.Property(e => e.NombreRuta)
                .HasMaxLength(100)
                .HasColumnName("nombreRuta");
        });

        modelBuilder.Entity<Seccion>(entity =>
        {
            entity.HasKey(e => e.SeccionId).HasName("Seccion_pkey");

            entity.ToTable("Seccion");

            entity.Property(e => e.SeccionId)
                .ValueGeneratedNever()
                .HasColumnName("seccionId");
            entity.Property(e => e.NombreSeccion)
                .HasMaxLength(150)
                .HasColumnName("nombreSeccion");
            entity.Property(e => e.TipoSeccionId).HasColumnName("tipoSeccionId");
            entity.Property(e => e.UltimaRevision).HasColumnName("ultimaRevision");

            entity.HasOne(d => d.TipoSeccion).WithMany(p => p.Seccions)
                .HasForeignKey(d => d.TipoSeccionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("seccion_tiposeccion_fk");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.ServicioId).HasName("Servicio_pkey");

            entity.ToTable("Servicio");

            entity.Property(e => e.ServicioId).HasColumnName("servicioId");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(255)
                .HasColumnName("nombreServicio");
            entity.Property(e => e.TipoServicioId).HasColumnName("tipoServicioId");

            entity.HasOne(d => d.TipoServicio).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.TipoServicioId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("servicio_tiposervicio_fk");
        });

        modelBuilder.Entity<Solicitud>(entity =>
        {
            entity.HasKey(e => e.SolicitudId).HasName("Solicitud_pkey");

            entity.ToTable("Solicitud");

            entity.Property(e => e.SolicitudId)
                .ValueGeneratedNever()
                .HasColumnName("solicitudId");
            entity.Property(e => e.AscensorId).HasColumnName("ascensorId");
            entity.Property(e => e.DescripcionSolicitud).HasColumnName("descripcionSolicitud");
            entity.Property(e => e.FechaRespuesta).HasColumnName("fechaRespuesta");
            entity.Property(e => e.FechaSolicitud).HasColumnName("fechaSolicitud");
            entity.Property(e => e.PrioridadId).HasColumnName("prioridadId");
            entity.Property(e => e.ServicioId).HasColumnName("servicioId");
            entity.Property(e => e.TecnicoId).HasColumnName("tecnicoId");

            entity.HasOne(d => d.Ascensor).WithMany(p => p.Solicituds)
                .HasForeignKey(d => d.AscensorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("solicitud_ascensor_fk");

            entity.HasOne(d => d.Prioridad).WithMany(p => p.Solicituds)
                .HasForeignKey(d => d.PrioridadId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("solicitud_prioridad_fk");

            entity.HasOne(d => d.Servicio).WithMany(p => p.Solicituds)
                .HasForeignKey(d => d.ServicioId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("solicitud_servicio_fk");

            entity.HasOne(d => d.Tecnico).WithMany(p => p.Solicituds)
                .HasForeignKey(d => d.TecnicoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("solicitud_usuario_fk");
        });

        modelBuilder.Entity<TipoAverium>(entity =>
        {
            entity.HasKey(e => e.TipoAveriaId).HasName("TipoAveria_pkey");

            entity.Property(e => e.TipoAveriaId)
                .ValueGeneratedNever()
                .HasColumnName("tipoAveriaId");
            entity.Property(e => e.TipoDescripcion).HasColumnName("tipo_descripcion");
            entity.Property(e => e.TipoNombre)
                .HasMaxLength(100)
                .HasColumnName("tipo_nombre");
        });

        modelBuilder.Entity<TipoSeccion>(entity =>
        {
            entity.HasKey(e => e.TipoSeccionId).HasName("TipoSeccion_pkey");

            entity.ToTable("TipoSeccion");

            entity.Property(e => e.TipoSeccionId).HasColumnName("tipoSeccionId");
            entity.Property(e => e.TipoDescripcion).HasColumnName("tipo_descripcion");
            entity.Property(e => e.TipoSeccionNombre)
                .HasMaxLength(255)
                .HasColumnName("tipoSeccion_nombre");
        });

        modelBuilder.Entity<TipoServicio>(entity =>
        {
            entity.HasKey(e => e.TipoServicioId).HasName("TipoServicio_pkey");

            entity.ToTable("TipoServicio");

            entity.HasIndex(e => e.NombreServicio, "UNIQUE_tipo_servicio");

            entity.Property(e => e.TipoServicioId).HasColumnName("tipoServicioId");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(100)
                .HasColumnName("nombreServicio");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("Usuario_pkey");

            entity.ToTable("Usuario");

            entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .HasColumnName("correo");
            entity.Property(e => e.Nombres)
                .HasMaxLength(255)
                .HasColumnName("nombres");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.RutaId)
                .HasMaxLength(10)
                .HasColumnName("rutaId");
            entity.Property(e => e.Salt)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("salt");
            entity.Property(e => e.Telefono)
                .HasMaxLength(16)
                .HasColumnName("telefono");

            entity.HasOne(d => d.Ruta).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RutaId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("usuario_ruta_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
