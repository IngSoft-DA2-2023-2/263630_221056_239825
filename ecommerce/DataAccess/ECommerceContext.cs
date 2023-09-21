using Dominio;
using Dominio.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class ECommerceContext: DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        //public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Color> Colores { get; set; }
        public DbSet<Compra> Compras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);*/

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

                var connectionString = configuration.GetConnectionString("ECommerceDB");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
