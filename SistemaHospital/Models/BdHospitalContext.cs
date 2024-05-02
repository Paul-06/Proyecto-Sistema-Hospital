using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaHospital.Models;

public partial class BdHospitalContext : DbContext
{
    public BdHospitalContext()
    {
    }

    public BdHospitalContext(DbContextOptions<BdHospitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Citum> Cita { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<Examan> Examen { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<HistorialMedico> HistorialMedicos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Recetum> Receta { get; set; }

    public virtual DbSet<Resultado> Resultados { get; set; }

    public virtual DbSet<TipoEmpleado> TipoEmpleados { get; set; }

    public virtual DbSet<TipoExaman> TipoExamen { get; set; }

    public virtual DbSet<TipoPago> TipoPagos { get; set; }

    public virtual DbSet<TipoTratamiento> TipoTratamientos { get; set; }

    public virtual DbSet<Tratamiento> Tratamientos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo).HasName("PK__Cargo__6C98562518039822");

            entity.ToTable("Cargo");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Citum>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__Cita__394B020258C33BD3");

            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.FechaHora).HasColumnType("datetime");
            entity.Property(e => e.MotivoConsulta)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__Cita__IdEmpleado__6E01572D");

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdEspecialidad)
                .HasConstraintName("FK__Cita__IdEspecial__6EF57B66");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("FK__Cita__IdPaciente__6D0D32F4");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__DetalleF__E43646A59897C0DC");

            entity.ToTable("DetalleFactura");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Cantidad]*[PrecioUnitario])", false)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK__DetalleFa__IdFac__75A278F5");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9E6AEE3D93");

            entity.ToTable("Empleado");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCargo)
                .HasConstraintName("FK__Empleado__IdCarg__693CA210");

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEspecialidad)
                .HasConstraintName("FK__Empleado__IdEspe__68487DD7");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK__Empleado__IdPers__66603565");

            entity.HasOne(d => d.IdTipoEmpleadoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdTipoEmpleado)
                .HasConstraintName("FK__Empleado__IdTipo__6754599E");
        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidad).HasName("PK__Especial__693FA0AFD4B87352");

            entity.ToTable("Especialidad");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Examan>(entity =>
        {
            entity.HasKey(e => e.IdExamen).HasName("PK__Examen__0E8DC9BE048C591B");

            entity.Property(e => e.FechaAplicacion).HasColumnType("date");
            entity.Property(e => e.FechaSolicitud).HasColumnType("date");
            entity.Property(e => e.Observaciones).HasMaxLength(255);

            entity.HasOne(d => d.IdHistorialMedicoNavigation).WithMany(p => p.Examen)
                .HasForeignKey(d => d.IdHistorialMedico)
                .HasConstraintName("FK__Examen__IdHistor__5FB337D6");

            entity.HasOne(d => d.IdTipoExamenNavigation).WithMany(p => p.Examen)
                .HasForeignKey(d => d.IdTipoExamen)
                .HasConstraintName("FK__Examen__IdTipoEx__60A75C0F");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Factura__50E7BAF1A8CC8ED1");

            entity.ToTable("Factura");

            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaEmision).HasColumnType("date");
            entity.Property(e => e.Total).HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("FK__Factura__IdPacie__72C60C4A");
        });

        modelBuilder.Entity<HistorialMedico>(entity =>
        {
            entity.HasKey(e => e.IdHistorialMedico).HasName("PK__Historia__8F4C4A762B032F87");

            entity.ToTable("HistorialMedico");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.HistorialMedicos)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("FK__Historial__IdPac__571DF1D5");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__Paciente__C93DB49B61D8E576");

            entity.ToTable("Paciente");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK__Paciente__IdPers__52593CB8");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pago__FC851A3AF5407F53");

            entity.ToTable("Pago");

            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK__Pago__IdFactura__7A672E12");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdTipoPago)
                .HasConstraintName("FK__Pago__IdTipoPago__7B5B524B");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Persona__2EC8D2ACBDA08C59");

            entity.ToTable("Persona");

            entity.HasIndex(e => e.Dni, "UQ__Persona__C03085751FCC00AD").IsUnique();

            entity.Property(e => e.ApellidoMaterno).HasMaxLength(100);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(100);
            entity.Property(e => e.Celular)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Correo).HasMaxLength(200);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Nombres).HasMaxLength(100);
        });

        modelBuilder.Entity<Recetum>(entity =>
        {
            entity.HasKey(e => e.RecetaId).HasName("PK__Receta__03D077B8D2018320");

            entity.Property(e => e.RecetaId).HasColumnName("RecetaID");
            entity.Property(e => e.FechaPrescripcion).HasColumnType("datetime");
            entity.Property(e => e.Instrucciones)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCitaNavigation).WithMany(p => p.Receta)
                .HasForeignKey(d => d.IdCita)
                .HasConstraintName("FK__Receta__IdCita__7E37BEF6");
        });

        modelBuilder.Entity<Resultado>(entity =>
        {
            entity.HasKey(e => e.IdResultado).HasName("PK__Resultad__DAF71D0B64184CB8");

            entity.ToTable("Resultado");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.FechaEntrega).HasColumnType("date");

            entity.HasOne(d => d.IdExamenNavigation).WithMany(p => p.Resultados)
                .HasForeignKey(d => d.IdExamen)
                .HasConstraintName("FK__Resultado__IdExa__6383C8BA");
        });

        modelBuilder.Entity<TipoEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdTipoEmpleado).HasName("PK__TipoEmpl__BD399225A6440886");

            entity.ToTable("TipoEmpleado");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<TipoExaman>(entity =>
        {
            entity.HasKey(e => e.IdTipoExamen).HasName("PK__TipoExam__FF2B211856592430");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<TipoPago>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).HasName("PK__TipoPago__EB0AA9E77EDFC886");

            entity.ToTable("TipoPago");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoTratamiento>(entity =>
        {
            entity.HasKey(e => e.IdTipoTratamiento).HasName("PK__TipoTrat__D34D982E3B45C3FB");

            entity.ToTable("TipoTratamiento");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Tratamiento>(entity =>
        {
            entity.HasKey(e => e.IdTratamiento).HasName("PK__Tratamie__5CB7E7530A491BD0");

            entity.ToTable("Tratamiento");

            entity.Property(e => e.FechaFinalizacion).HasColumnType("date");
            entity.Property(e => e.FechaInicio).HasColumnType("date");
            entity.Property(e => e.FechaSolicitud).HasColumnType("date");
            entity.Property(e => e.Observaciones).HasMaxLength(255);

            entity.HasOne(d => d.IdHistorialMedicoNavigation).WithMany(p => p.Tratamientos)
                .HasForeignKey(d => d.IdHistorialMedico)
                .HasConstraintName("FK__Tratamien__IdHis__59FA5E80");

            entity.HasOne(d => d.IdTipoTratamientoNavigation).WithMany(p => p.Tratamientos)
                .HasForeignKey(d => d.IdTipoTratamiento)
                .HasConstraintName("FK__Tratamien__IdTip__5AEE82B9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
