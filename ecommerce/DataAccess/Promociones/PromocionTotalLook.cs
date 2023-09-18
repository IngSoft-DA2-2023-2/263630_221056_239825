using System;
using Dominio;

namespace DataAccess.Promociones
{
	public class PromocionTotalLook: IPromocionStrategy
	{
		public PromocionTotalLook()
		{
		}

        public int AplicarPromocion(List<Producto> listaCompra)
        {
            throw new NotImplementedException();
        }

        public string NombrePromocion()
        {
            throw new NotImplementedException();
        }

        public bool AplicarPromo(List<Producto> carrito)
        {
            throw new NotImplementedException();
        }
    }
}

