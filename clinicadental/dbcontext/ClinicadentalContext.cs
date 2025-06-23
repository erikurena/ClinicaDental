using System;
using System.Collections.Generic;
using clinicadental.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace clinicadental.dbcontext;

public partial class ClinicadentalContext : IdentityDbContext<IdentityUser>
{  
    public ClinicadentalContext(DbContextOptions<ClinicadentalContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Afeccion> Afeccions { get; set; }

    public virtual DbSet<Antecedentebucodental> Antecedentebucodentals { get; set; }

    public virtual DbSet<Antecedenteenfermedad> Antecedenteenfermedads { get; set; }

    public virtual DbSet<Antecedentegeneral> Antecedentegenerals { get; set; }

    public virtual DbSet<Antecedentehigieneoral> Antecedentehigieneorals { get; set; }

    public virtual DbSet<Antecedentepatologico> Antecedentepatologicos { get; set; }

    public virtual DbSet<Avancetratamiento> Avancetratamientos { get; set; }

    public virtual DbSet<Clinica> Clinicas { get; set; }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Enfermerdad> Enfermerdads { get; set; }

    public virtual DbSet<Estadoperiodontale> Estadoperiodontales { get; set; }

    public virtual DbSet<Examenextraoral> Examenextraorals { get; set; }

    public virtual DbSet<Examenintraoral> Examenintraorals { get; set; }

    public virtual DbSet<Historialclinico> Historialclinicos { get; set; }

    public virtual DbSet<Odontograma> Odontogramas { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<PagosTratamiento> PagosTratamientos { get; set; } 

    public virtual DbSet<Respiracion> Respiracions { get; set; }

    public virtual DbSet<Tratamiento> Tratamientos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Afeccion>(entity =>
        {
            entity.HasKey(e => e.IdAfeccion).HasName("PRIMARY");

            entity.ToTable("afeccion");

            entity.Property(e => e.Afeccion1)
                .HasMaxLength(45)
                .HasColumnName("Afeccion");
        });

        modelBuilder.Entity<Antecedentebucodental>(entity =>
        {
            entity.HasKey(e => e.IdAntecedenteBucoDental).HasName("PRIMARY");

            entity.ToTable("antecedentebucodental");

            entity.Property(e => e.Bebe).HasMaxLength(45).HasConversion<string>();
            entity.Property(e => e.Fuma).HasMaxLength(45).HasConversion<string>();
            entity.Property(e => e.Otro).HasMaxLength(45);
            entity.Property(e => e.UltimaVisitaDental).HasColumnType("datetime");
        });

        modelBuilder.Entity<Antecedenteenfermedad>(entity =>
        {
            entity.HasKey(e => e.IdAntecedenteEnfermedad).HasName("PRIMARY");

            entity.ToTable("antecedenteenfermedad");

            entity.HasIndex(e => e.IdAntecedentePatologico, "fk_antecedenteenfermedad_antecedentepatologico1_idx");

            entity.HasIndex(e => e.IdEnfermerdad, "fk_antecedenteenfermedad_enfermerdad1_idx");

            entity.HasOne(d => d.IdAntecedentePatologicoNavigation).WithMany(p => p.Antecedenteenfermedads)
                .HasForeignKey(d => d.IdAntecedentePatologico)
                 .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_antecedenteenfermedad_antecedentepatologico1");

            entity.HasOne(d => d.IdEnfermerdadNavigation).WithMany(p => p.Antecedenteenfermedads)
                .HasForeignKey(d => d.IdEnfermerdad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_antecedenteenfermedad_enfermerdad1");
        });

        modelBuilder.Entity<Antecedentegeneral>(entity =>
        {
            entity.HasKey(e => e.IdAntecedenteGeneral).HasName("PRIMARY");

            entity.ToTable("antecedentegeneral");

            entity.HasIndex(e => e.IdAntecedenteGeneral, "idantecedentegeneral_UNIQUE").IsUnique();

            entity.Property(e => e.Alergia).HasMaxLength(45).HasConversion<string>();
            entity.Property(e => e.Embarazo).HasMaxLength(45).HasConversion<string>();
            entity.Property(e => e.EspecificacionHemorragia).HasMaxLength(45).HasConversion<string>();
            entity.Property(e => e.HemorragiaDental).HasMaxLength(45).HasConversion<string>();
            entity.Property(e => e.SemanaEmbarazo).HasMaxLength(45);
            entity.Property(e => e.TipoAlergia).HasMaxLength(100);
        });

        modelBuilder.Entity<Antecedentehigieneoral>(entity =>
        {
            entity.HasKey(e => e.IdAntecedenteHigieneOral).HasName("PRIMARY");

            entity.ToTable("antecedentehigieneoral");

            entity.Property(e => e.CepilloDental).HasMaxLength(45).HasConversion<string>();
            entity.Property(e => e.EnjuagueBucal).HasMaxLength(45).HasConversion<string>();
            entity.Property(e => e.FrecuenciaCepilladoDental).HasMaxLength(45);
            entity.Property(e => e.IdHigieneBucal).HasMaxLength(45).HasConversion<string>();
            entity.Property(e => e.IdHiloDental).HasMaxLength(45).HasConversion<string>();
            entity.Property(e => e.SangradoEncias).HasMaxLength(45).HasConversion<string>();
        });

        modelBuilder.Entity<Antecedentepatologico>(entity =>
        {
            entity.HasKey(e => e.IdAntecedentePatologico).HasName("PRIMARY");

            entity.ToTable("antecedentepatologico");

            entity.Property(e => e.AntecedentePatologicoFamiliar).HasMaxLength(100);
            entity.Property(e => e.Otro).HasMaxLength(100);
            entity.Property(e => e.RecibeMedicacion).HasMaxLength(45);
            entity.Property(e => e.TratamientoMedico).HasMaxLength(45);


        });

        modelBuilder.Entity<Avancetratamiento>(entity =>
        {
            entity.HasKey(e => e.IdAvanceTratamiento).HasName("PRIMARY");

            entity.ToTable("avancetratamiento");

            entity.HasIndex(e => e.IdTratamiento, "fk_avancetratamiento_tratamiento1_idx");

            entity.Property(e => e.PiezaDental).HasMaxLength(200);
            entity.Property(e => e.Avance).HasMaxLength(200);

            entity.HasOne(d => d.IdTratamientoNavigation).WithMany(p => p.Avancetratamientos)
                .HasForeignKey(d => d.IdTratamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_avancetratamiento_tratamiento1");
        });
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PRIMARY");
            entity.ToTable("cita");
            entity.HasIndex(e => e.IdUsuario, "fk_cita_usuario1_idx");

            entity.Property(e => e.FechaHoraCita).HasColumnType("date");
            entity.Property(e => e.HorainicioCita).HasColumnType("datetime");
            entity.Property(e => e.HorafinCita).HasColumnType("datetime");
            entity.Property(e => e.NombrePaciente).HasMaxLength(100);
            entity.Property(e => e.MotivoCita).HasMaxLength(200);
            entity.Property(e => e.EstadoCita).HasMaxLength(45);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.CorreoElectronico).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(45);
            entity.HasIndex(e => e.IdUsuario, "fk_cita_usuario1_idx");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Citas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cita_usuario1");
        });
        modelBuilder.Entity<Clinica>(entity =>
        {
            entity.HasKey(e => e.IdClinica).HasName("PRIMARY");

            entity.ToTable("clinica");
            entity.HasIndex(e => e.IdUsuario, "fk_clinica_usuario1_idx");

            entity.Property(e => e.Celular)
                .HasMaxLength(45)
                .HasColumnName("celular");
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.FotoClinica).HasMaxLength(45);
            entity.Property(e => e.Nit).HasMaxLength(45);
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Pmc).HasMaxLength(45);
            entity.Property(e => e.Ujsedes).HasMaxLength(45);
            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.Pais).HasMaxLength(50);

            entity.HasOne(d => d.UsuariosNavigation).WithMany(p => p.Clinicas)
                 .HasForeignKey(d => d.IdUsuario)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("fk_clinica_usuario1");
        });

        modelBuilder.Entity<Enfermerdad>(entity =>
        {
            entity.HasKey(e => e.IdEnfermerdad).HasName("PRIMARY");

            entity.ToTable("enfermerdad");

            entity.Property(e => e.Enfermedad).HasMaxLength(45);
        });

        modelBuilder.Entity<Estadoperiodontale>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPeriodontal).HasName("PRIMARY");

            entity.ToTable("estadoperiodontal");

            entity.HasIndex(e => e.IdOdontograma, "fk_estadoperiodontal_odontograma1_idx");

            entity.Property(e => e.CeoC).HasColumnName("CEO_C");
            entity.Property(e => e.CeoE).HasColumnName("CEO_E");
            entity.Property(e => e.CeoO).HasColumnName("CEO_O");
            entity.Property(e => e.CeoTotal).HasColumnName("TotalCeo");
            entity.Property(e => e.CpodC).HasColumnName("CPO_C");
            entity.Property(e => e.CpodEi).HasColumnName("CPO_EI");
            entity.Property(e => e.CpodO).HasColumnName("CPO_O");
            entity.Property(e => e.CpodP).HasColumnName("CPO_P");
            entity.Property(e => e.CpodTotal).HasColumnName("TotalCpo");
            entity.Property(e => e.TotalPiezasSanas).HasColumnName("TotalPiezasSanas");
            entity.Property(e => e.TotalPiezasDentarias).HasColumnName("TotalPiezasDentarias");

            entity.HasOne(d => d.IdOdontogramaNavigation).WithMany(p => p.Estadoperiodontales)
                .HasForeignKey(d => d.IdOdontograma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_estadoperiodontal_odontograma1");
        });

        modelBuilder.Entity<Examenextraoral>(entity =>
        {
            entity.HasKey(e => e.IdExamenExtraOral).HasName("PRIMARY");

            entity.ToTable("examenextraoral");

            entity.HasIndex(e => e.IdRespiracion, "fk_examenextraoral_respiracion1_idx");

            entity.Property(e => e.Atm).HasMaxLength(45);
            entity.Property(e => e.GanglioLinfatico).HasMaxLength(45);
            entity.Property(e => e.Otro).HasMaxLength(45);

            entity.HasOne(d => d.IdRespiracionNavigation).WithMany(p => p.Examenextraorals)
                .HasForeignKey(d => d.IdRespiracion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_examenextraoral_respiracion1");
        });

        modelBuilder.Entity<Examenintraoral>(entity =>
        {
            entity.HasKey(e => e.IdExamenIntraoral).HasName("PRIMARY");

            entity.ToTable("examenintraoral");

            entity.Property(e => e.Encia).HasMaxLength(45);
            entity.Property(e => e.Labio).HasMaxLength(45);
            entity.Property(e => e.Lengua).HasMaxLength(45);
            entity.Property(e => e.MucosaYugal).HasMaxLength(45);
            entity.Property(e => e.Paladar).HasMaxLength(45);
            entity.Property(e => e.PisoDeBoca).HasMaxLength(45);
            entity.Property(e => e.ProtesisDental).HasMaxLength(45).HasConversion<string>();
        });

     

        modelBuilder.Entity<Historialclinico>(entity =>
        {
            entity.HasKey(e => e.IdHistorialClinico).HasName("PRIMARY");

            entity.ToTable("historialclinico");

            entity.HasIndex(e => e.IdAntecedenteBucoDental, "fk_HistorialClinico_antecedentebucodental1_idx");

            entity.HasIndex(e => e.IdAntecedenteHigieneOral, "fk_HistorialClinico_antecedentehigieneoral1_idx");

            entity.HasIndex(e => e.IdAntecedentePatologico, "fk_HistorialClinico_antecedentepatologico1_idx");

            entity.HasIndex(e => e.IdExamenExtraOral, "fk_HistorialClinico_examenextraoral1_idx");

            entity.HasIndex(e => e.IdExamenIntraoral, "fk_HistorialClinico_examenintraoral1_idx");

            entity.HasIndex(e => e.IdPaciente, "fk_HistorialClinico_paciente1_idx");

            entity.HasIndex(e => e.IdUsuario, "fk_HistorialClinico_usuario1_idx");

            entity.HasIndex(e => e.IdAntecedenteGeneral, "fk_historialclinico_antecedentegeneral1_idx");

            entity.Property(e => e.Ci).HasMaxLength(45);
            entity.Property(e => e.CodigoHistorial).HasMaxLength(45);
            entity.Property(e => e.MotivoCita).HasMaxLength(200);
            entity.Property(e => e.Observacion).HasMaxLength(200);

            entity.HasOne(d => d.IdAntecedenteBucoDentalNavigation).WithMany(p => p.Historialclinicos)
                .HasForeignKey(d => d.IdAntecedenteBucoDental)
                 .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_HistorialClinico_antecedentebucodental1");

            entity.HasOne(d => d.IdAntecedenteGeneralNavigation).WithMany(p => p.Historialclinicos)
                .HasForeignKey(d => d.IdAntecedenteGeneral)
                  .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_historialclinico_antecedentegeneral1");

            entity.HasOne(d => d.IdAntecedenteHigieneOralNavigation).WithMany(p => p.Historialclinicos)
                .HasForeignKey(d => d.IdAntecedenteHigieneOral)
                 .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_HistorialClinico_antecedentehigieneoral1");

            entity.HasOne(d => d.IdAntecedentePatologicoNavigation).WithMany(p => p.Historialclinicos)
                .HasForeignKey(d => d.IdAntecedentePatologico)
                 .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_HistorialClinico_antecedentepatologico1");

            entity.HasOne(d => d.IdExamenExtraOralNavigation).WithMany(p => p.Historialclinicos)
                .HasForeignKey(d => d.IdExamenExtraOral)
                 .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_HistorialClinico_examenextraoral1");

            entity.HasOne(d => d.IdExamenIntraoralNavigation).WithMany(p => p.Historialclinicos)
                .HasForeignKey(d => d.IdExamenIntraoral)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_HistorialClinico_examenintraoral1");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Historialclinicos)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_HistorialClinico_paciente1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Historialclinicos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_HistorialClinico_usuario1");
        });

        modelBuilder.Entity<Odontograma>(entity =>
        {
            entity.HasKey(e => e.IdOdontograma).HasName("PRIMARY");

            entity.ToTable("odontograma");

            entity.HasIndex(e => e.IdAfeccion, "fk_odontograma_afeccion1_idx");

            entity.HasIndex(e => e.IdHistorialClinico, "fk_odontograma_historialclinico1_idx");

            entity.Property(e => e.CaraPiezaDental).HasMaxLength(45);

            entity.HasOne(d => d.IdAfeccionNavigation).WithMany(p => p.Odontogramas)
                .HasForeignKey(d => d.IdAfeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_odontograma_afeccion1");

            entity.HasOne(d => d.IdHistorialClinicoNavigation).WithMany(p => p.Odontogramas)
                .HasForeignKey(d => d.IdHistorialClinico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_odontograma_historialclinico1");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PRIMARY");

            entity.ToTable("paciente");

            entity.Property(e => e.ApellidoMaterno).HasMaxLength(45);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(45);
            entity.Property(e => e.Celular).HasMaxLength(45);
            entity.Property(e => e.CodigoPaciente).HasMaxLength(45);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Nombres).HasMaxLength(45);
            entity.Property(e => e.Ocupacion).HasMaxLength(45);
            entity.Property(e => e.FechaNacimientoPaciente).HasMaxLength(45);
            entity.Property(e => e.Sexo).HasMaxLength(45).HasConversion<string>();
            entity.Property(e => e.IdEstadoCivil).HasMaxLength(45).HasConversion<string>();
            entity.Property(e => e.IdGradoInstruccion).HasMaxLength(45).HasConversion<string>();
            entity.Property(e => e.IdLugarNacimiento).HasMaxLength(45);
        });

        modelBuilder.Entity<PagosTratamiento>(entity =>
        {
            entity.HasKey(e => e.IdPagoTratamiento).HasName("PRIMARY");

            entity.ToTable("pagostratamiento");   
            entity.Property(e => e.Monto).HasPrecision(10, 2);

            entity.HasIndex(e => e.IdTratamiento, "fk_pagostratamiento_tratamiento1_idx");

            entity.HasOne(d => d.IdTratamientoNavigation).WithMany(p => p.PagoTratamiento)
                .HasForeignKey(d => d.IdTratamiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pagostratamiento_tratamiento1");
        });       
        modelBuilder.Entity<Respiracion>(entity =>
        {
            entity.HasKey(e => e.IdRespiracion).HasName("PRIMARY");

            entity.ToTable("respiracion");

            entity.Property(e => e.TipoRespiracion).HasMaxLength(45);
        });


        modelBuilder.Entity<Tratamiento>(entity =>
        {
            entity.HasKey(e => e.IdTratamiento).HasName("PRIMARY");

            entity.ToTable("tratamiento");
            entity.Property(e => e.PresupuestoTotal).HasPrecision(10,2);
            entity.Property(e => e.ACuenta).HasPrecision(10, 2);
            entity.Property(e => e.Saldo).HasPrecision(10, 2);

            entity.HasIndex(e => e.IdHistorialClinico, "fk_tratamiento_historialclinico1_idx");
                     
            entity.Property(e => e.Analisis).HasMaxLength(300);
            entity.Property(e => e.Objetivo).HasMaxLength(300);
            entity.Property(e => e.PlanAccion).HasMaxLength(300); 
            entity.Property(e => e.Subjetivo).HasMaxLength(300);

            entity.HasOne(d => d.IdHistorialClinicoNavigation).WithMany(p => p.Tratamientos)
                .HasForeignKey(d => d.IdHistorialClinico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tratamiento_historialclinico1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.Property(e => e.ApellidoMaterno).HasMaxLength(45);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(45);
            entity.Property(e => e.Celular).HasMaxLength(45);
            entity.Property(e => e.CodigoUsuario).HasMaxLength(36);
            entity.Property(e => e.Especialidad).HasMaxLength(45);
            entity.Property(e => e.FechaNacimiento).HasMaxLength(45);
            entity.Property(e => e.FotoUsuario).HasMaxLength(45);
            entity.Property(e => e.PrimerNombre).HasMaxLength(45);
            entity.Property(e => e.SegundoNombre).HasMaxLength(45);
            entity.Property(e => e.Sexo).HasMaxLength(45).HasConversion<string>();

        });

        modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
