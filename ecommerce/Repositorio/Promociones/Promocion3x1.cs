using System;
using Dominio;

namespace Repositorio.Promociones
{
	public class Promocion3x1: IPromocionStrategy
	{
		public Promocion3x1()
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

