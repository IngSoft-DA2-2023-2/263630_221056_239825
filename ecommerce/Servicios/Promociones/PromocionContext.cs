using System;
using Servicios.Promociones;
using Dominio;

namespace Servicios.Promociones
{

	public class PromocionContext
	{

		public PromocionContext()
		{
		}
		public IPromocionStrategy promocionStrategy { get; set; }

        public int AplicarStrategy(List<Producto> listaCompra)
		{
			throw new NotImplementedException();
		}

        public string NombrePromocion(){
            throw new NotImplementedException();
        }

        public bool AplicarStrategyPromo(List<Producto> carrito)
		{
            throw new NotImplementedException();
        }
    }
}

