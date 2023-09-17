using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Repositorio
{
    public class ECommerceContext: DbContext
    {
        public ECommerceContext(DbContextOptions options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ColorPorProducto>()
                .HasKey(cpp => new { cpp.ProductoId, cpp.ColorId });

            modelBuilder.Entity<ColorPorProducto>()
                .HasOne(cpp => cpp.Producto)
                .WithMany(p => p.ColoresDelProducto)
                .HasForeignKey(cpp => cpp.ProductoId);

            modelBuilder.Entity<ColorPorProducto>()
                .HasOne(cpp => cpp.Color)
                .WithMany(c => c.ProductosDelColor)
                .HasForeignKey(cpp => cpp.ColorId);

            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(directory)
                 .AddJsonFile("appsettings.json")
                 .Build();

                var connectionString = DBConnectionStringFactory(configuration);

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        private string DBConnectionStringFactory(IConfigurationRoot configuration) => Environment.OSVersion.Platform switch
        {
            PlatformID.MacOSX => configuration.GetConnectionString("AppleECommerceDB")!,
            _ => configuration.GetConnectionString("WinECommerceDB")!
        };

    }
}
