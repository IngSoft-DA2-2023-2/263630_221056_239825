using Microsoft.Extensions.DependencyInjection;
using DataAccess;
using DataAccess.Interfaces;
using Servicios;
using Servicios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ServicioFactory
{
    public class ServiciosFactory
    {
        public ServiciosFactory() { }
        public void RegistrateServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<DbContext, ECommerceContext>();
            serviceCollection.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
        }
    }
}