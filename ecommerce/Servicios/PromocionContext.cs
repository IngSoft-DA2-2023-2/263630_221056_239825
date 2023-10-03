using System;
using Servicios;
using Servicios.Promociones;
using Dominio;

namespace Servicios
{

    public class PromocionContext
    {

        public PromocionContext()
        {
        }
        public IPromocionStrategy promocionStrategy { get; set; }

        public int AplicarStrategy(List<Producto> listaCompra)
        {
            return promocionStrategy.AplicarPromocion(listaCompra);
        }

        public string NombrePromocion()
        {
            return promocionStrategy.NombrePromocion;
        }

        public bool AplicarStrategyPromo(List<Producto> carrito)
        {
            return promocionStrategy.AplicarPromo(carrito);
        }
    }
}
