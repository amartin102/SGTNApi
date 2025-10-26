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

            // ParameterValue configuration COMPLETA
            modelBuilder.Entity<ParameterValue>(entity =>
            {
                entity.ToTable("tblValorParametro", "param");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("strIdValorParametro");

                entity.Property(e => e.ParameterId)
                    .HasColumnName("strIdParametro");

                entity.Property(e => e.ClientId)
                    .HasColumnName("strIdCliente");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("strIdEmpleado");

                entity.Property(e => e.TextValue)
                    .HasColumnName("strValorTexto")
                    .HasMaxLength(50);

                entity.Property(e => e.NumericValue)
                    .HasColumnName("dblValorNumerico")
                    .HasColumnType("decimal(16,4)");

                entity.Property(e => e.DateValue)
                    .HasColumnName("datValorFecha");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("strUsuarioCreador")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.CreationDate)
                    .HasColumnName("datFechaCreacion");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("strModificadoPor")
                    .HasMaxLength(50);

                entity.Property(e => e.ModificationDate)
                    .HasColumnName("datFechaModificacion");

                // Relationships completas
                entity.HasOne(pv => pv.MasterParameter)
                      .WithMany(mp => mp.ParameterValues)
                      .HasForeignKey(pv => pv.ParameterId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pv => pv.Client)
                      .WithMany(c => c.ParameterValues)
                      .HasForeignKey(pv => pv.ClientId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pv => pv.Employee)
                      .WithMany(e => e.ParameterValues)
                      .HasForeignKey(pv => pv.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Client configuration
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("tblCliente", "client");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("strIdCliente");

                entity.Property(e => e.Name)
                    .HasColumnName("strNombreCliente")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Nit)
                    .HasColumnName("strNit")
                    .HasMaxLength(15)
                    .IsRequired();

                entity.Property(e => e.Address)
                    .HasColumnName("strDireccion")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Phone)
                    .HasColumnName("strTelefono")
                    .HasMaxLength(50);

                entity.Property(e => e.Cellphone)
                    .HasColumnName("strCelular")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.CityId)
                    .HasColumnName("strIdCiudad");

                entity.Property(e => e.Status)
                    .HasColumnName("logEstado");

                // Relationships
                entity.HasOne(c => c.City)
                      .WithMany(ci => ci.Clients)
                      .HasForeignKey(c => c.CityId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Employee configuration
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("tblEmpleado", "client");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("strIdEmpleado");

                entity.Property(e => e.FirstName)
                    .HasColumnName("strNombre")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .HasColumnName("strApellido")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Identification)
                    .HasColumnName("strIdentificacion")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.IdentificationTypeId)
                    .HasColumnName("intIdTipoIdentificacion");

                entity.Property(e => e.Address)
                    .HasColumnName("strDireccion")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Phone)
                    .HasColumnName("strTelefono")
                    .HasMaxLength(50);

                entity.Property(e => e.Cellphone)
                    .HasColumnName("strCelular")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .HasColumnName("logEstado");

                // Relationships
                entity.HasOne(e => e.IdentificationType)
                      .WithMany()
                      .HasForeignKey(e => e.IdentificationTypeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // IdentificationType configuration
            modelBuilder.Entity<IdentificationType>(entity =>
            {
                entity.ToTable("tblTipoIdentificacion", "client");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("intIdTipoIdentificacion");

                entity.Property(e => e.Description)
                    .HasColumnName("strDescripcion")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            // Country configuration
            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("tblPais", "client");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("strIdPais");

                entity.Property(e => e.Description)
                    .HasColumnName("strDescripcion")
                    .HasMaxLength(255)
                    .IsRequired();
            });

            // Department configuration
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("tblDepartamento", "client");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("strIdDepartamento");

                entity.Property(e => e.Description)
                    .HasColumnName("strDescripcion")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.CountryId)
                    .HasColumnName("strIdPais");

                // Relationships
                entity.HasOne(d => d.Country)
                      .WithMany(c => c.Departments)
                      .HasForeignKey(d => d.CountryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // City configuration
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("tblCiudad", "client");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("strIdCiudad");

                entity.Property(e => e.Description)
                    .HasColumnName("strDescripcion")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("strIdDepartamento");

                // Relationships
                entity.HasOne(c => c.Department)
                      .WithMany(d => d.Cities)
                      .HasForeignKey(c => c.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<MasterParameter> MasterParameters { get; set; }
        public DbSet<ParameterType> ParameterTypes { get; set; }
        public DbSet<InconsistencyLevel> InconsistencyLevels { get; set; }

        public DbSet<ParameterValue> ParameterValues { get; set; }

        // Clientes y Empleados
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<IdentificationType> IdentificationTypes { get; set; }

        // Ubicación
        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
