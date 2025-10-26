using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repository.Context
{
    public class SqlDbContext : DbContext
    {
        public string DefaultConnection { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            optionsBuilder.UseSqlServer(GetConnection());
            //}
        }

        private string GetConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            DefaultConnection = configuration.GetConnectionString("SqlServerConnectionString");
            return DefaultConnection;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MasterParameter>(entity =>
            {
                entity.ToTable("tblMaestroParametro", "param");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("strIdParametro");
                entity.Property(e => e.Code).HasColumnName("strCodParametro").HasMaxLength(50);
                entity.Property(e => e.DataTypeId).HasColumnName("intIdTipoDato");
                entity.Property(e => e.InconsistencyLevelId).HasColumnName("intIdNivelInconsistencia");
                entity.Property(e => e.ModifyPermission).HasColumnName("strPermisoModificar").HasMaxLength(50);
                entity.Property(e => e.ConsultPermission).HasColumnName("strPermisoConsultar").HasMaxLength(50);
                entity.Property(e => e.CreatedBy).HasColumnName("strUsuarioCreador").HasMaxLength(50);
                entity.Property(e => e.CreationDate).HasColumnName("datFechaCreacion");
                entity.Property(e => e.ModifiedBy).HasColumnName("strModificadoPor").HasMaxLength(50);
                entity.Property(e => e.ModificationDate).HasColumnName("datFechaModificacion");

                // Relationships
                entity.HasOne(mp => mp.DataType)
                      .WithMany()
                      .HasForeignKey(mp => mp.DataTypeId);

                entity.HasOne(mp => mp.InconsistencyLevel)
                      .WithMany()
                      .HasForeignKey(mp => mp.InconsistencyLevelId);
            });

            modelBuilder.Entity<ParameterType>(entity =>
            {
                entity.ToTable("tblTipoDato", "param");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("intIdTipoDato");
                entity.Property(e => e.Description).HasColumnName("strDescripcion").HasMaxLength(50);
            });

            modelBuilder.Entity<InconsistencyLevel>(entity =>
            {
                entity.ToTable("tblNivelInconsistencia", "param");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("intIdNivelInconsistencia");
                entity.Property(e => e.Description).HasColumnName("strDescripcion").HasMaxLength(50);
            });

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<MasterParameter> MasterParameters { get; set; }
        public DbSet<ParameterType> ParameterTypes { get; set; }
        public DbSet<InconsistencyLevel> InconsistencyLevels { get; set; }
    }
}
