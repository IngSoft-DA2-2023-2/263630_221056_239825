using Dominio;
using Dominio.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class ECommerceContext: DbContext
    {
        public ECommerceContext() : base() { }

        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Color> Colores { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Usuario>()
                .Property(e => e.Roles)
                .HasConversion(
                    v => string.Join(",", v.Select(e => e.ToString("D")).ToArray()),
                    v => v.Split(new[] { ',' })
                        .Select(e =>  Enum.Parse(typeof(CategoriaRol), e))
                        .Cast<CategoriaRol>()
                        .ToList()
                );
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

                var connectionString = configuration.GetConnectionString("ECommerceDB");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}