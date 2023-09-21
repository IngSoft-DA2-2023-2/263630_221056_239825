using Dominio;
using Dominio.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class ECommerceContext: DbContext
    {
        public ECommerceContext(DbContextOptions options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Color> Colores { get; set; }
        public DbSet<Compra> Compras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);

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
            
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.Categoria);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Marca)
                .WithMany(m => m.Productos)
                .HasForeignKey(p => p.Marca);

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
            PlatformID.Win32NT => configuration.GetConnectionString("WinECommerceDB")!
        };

    }
}
