using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace PagOnlineAPI.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext() { }

        public ModelContext(DbContextOptions<ModelContext> options) 
            : base(options) { }

        public virtual DbSet<Models.Pago> Pagos { get; set; } = null!;
        public virtual DbSet<Models.Tarjeta> Tarjeta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().
                    SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseOracle(configuration.GetConnectionString("PagOnlineAPI_Database"));
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("C##TESTINGAPI")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.Idpago)
                    .HasName("SYS_C007327");

                entity.ToTable("PAGO");

                entity.Property(e => e.Idpago)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("IDPAGO");

                entity.Property(e => e.Detallepago)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("DETALLEPAGO");

                entity.Property(e => e.Fechapago)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHAPAGO");

                entity.Property(e => e.Idtarjeta)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("IDTARJETA");

                entity.HasOne(d => d.IdtarjetaNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.Idtarjeta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IDTARJETA");
            });

            modelBuilder.Entity<Models.Tarjeta>(entity =>
            {
                entity.ToTable("TARJETA");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.FechaValida)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("FECHA_VALIDA");

                entity.Property(e => e.Numero)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("NUMERO");

                entity.Property(e => e.Pin)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PIN");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
