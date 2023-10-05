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
        public IPromocionStrategy PromocionStrategy { get; set; }

        public int AplicarStrategy(List<Producto> listaCompra)
        {
            return PromocionStrategy.AplicarPromocion(listaCompra);
        }

        public string NombrePromocion()
        {
            return PromocionStrategy.NombrePromocion;
        }

        public bool AplicarStrategyPromo(List<Producto> carrito)
        {
            return PromocionStrategy.AplicarPromo(carrito);
        }
    }
}
