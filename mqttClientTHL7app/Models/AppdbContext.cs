using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace mqttClientTHL7app.Models;

public partial class AppdbContext : DbContext
{
    public AppdbContext()
    {
    }

    public AppdbContext(DbContextOptions<AppdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hl7> Hl7s { get; set; }

    public virtual DbSet<Nst> Nsts { get; set; }

    public virtual DbSet<Suat> Suats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-GQHK6MJ\\SQLEXPRESS;Database=APPDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hl7>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_HL77");

            entity.ToTable("HL7");

            entity.Property(e => e.Deger)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SinyalId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sinyal_Id");
        });

        modelBuilder.Entity<Nst>(entity =>
        {
            entity.ToTable("NST");

            entity.Property(e => e.NstId).HasColumnName("nstId");
            entity.Property(e => e.Fhr).HasColumnName("FHR");
            entity.Property(e => e.Ua).HasColumnName("UA");
        });

        modelBuilder.Entity<Suat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SUATİ");

            entity.ToTable("SUAT");

            entity.Property(e => e.Adi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lakap)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Plaka)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Takimi)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
