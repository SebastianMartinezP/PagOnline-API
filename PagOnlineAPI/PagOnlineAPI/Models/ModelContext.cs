using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PagOnlineAPI.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Models.ComprobantePago> ComprobantePago { get; set; } = null!;

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
            modelBuilder.HasDefaultSchema("C##SECURITY")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<Models.ComprobantePago>(entity =>
            {
                entity.HasKey(e => e.Idcomprobante)
                    .HasName("SYS_C007679");

                entity.ToTable("COMPROBANTE_PAGO");

                entity.Property(e => e.Idcomprobante)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("IDCOMPROBANTE");

                entity.Property(e => e.Fecharegistro)
                    .HasColumnType("DATE")
                    .HasColumnName("FECHAREGISTRO");

                entity.Property(e => e.Fechavalida)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("FECHAVALIDA");

                entity.Property(e => e.Monto)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("MONTO");

                entity.Property(e => e.Numerotarjeta)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("NUMEROTARJETA");

                entity.Property(e => e.Pintarjeta)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PINTARJETA");

                entity.Property(e => e.Tipomoneda)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TIPOMONEDA");

                entity.Property(e => e.Valoruf)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("VALORUF");

                entity.Property(e => e.Valorusd)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("VALORUSD");

                entity.Property(e => e.Valorutm)
                    .HasColumnType("NUMBER(10,2)")
                    .HasColumnName("VALORUTM");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
