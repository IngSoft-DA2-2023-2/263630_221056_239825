using Dominio;
using Dominio.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class ECommerceContext: DbContext
    {
        public ECommerceContext() 
        {
        }

        public ECommerceContext(DbContextOptions<ECommerceContext> optionsBuilderOptions) : base(optionsBuilderOptions)
        {
        }

        }

        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Color> Colores { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compra>()
                .Property(c => c.MetodoDePago)
                .HasConversion<string>();
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

                String? connectionString = configuration.GetConnectionString("ECommerceDB");

                if (connectionString is not null)
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
            }
        }
    }
}