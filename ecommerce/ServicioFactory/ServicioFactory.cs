using Microsoft.Extensions.DependencyInjection;
using DataAccess;
using DataAccess.Interfaces;
using Servicios;
using Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;
using Servicios.Promociones;

namespace ServicioFactory
{
    public class ServiciosFactory
    {
        public ServiciosFactory() { }
        public void RegistrateServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<DbContext, ECommerceContext>();

            serviceCollection.AddScoped<IRepositorioProducto, RepositorioProducto>();
            serviceCollection.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            serviceCollection.AddScoped<IRepositorioCompra, RepositorioCompra>();
            
            serviceCollection.AddScoped<IServicioCompra, ServicioCompra>();
            serviceCollection.AddScoped<IManejadorUsuario, ManejadorUsuario>();
            serviceCollection.AddScoped<IServicioProducto, ServicioProducto>();

            serviceCollection.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        //.WithOrigins("https://ecommerceui-a5663.web.app/")
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
          
            serviceCollection.AddScoped<IPromocionStrategy, PromocionTotalLook>();
            serviceCollection.AddScoped<IPromocionStrategy, Promocion20Off>();
            serviceCollection.AddScoped<IPromocionStrategy, Promocion3xModelable>();
        }
    }
}