using System;
using Dominio;

namespace Repositorio.Promociones
{
	public class Promocion20Off: IPromocionStrategy
	{
		public Promocion20Off()
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

