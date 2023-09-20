using System;
using Dominio;

namespace DataAccess.Promociones
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
            return "Se aplico la promocion de Fidelidad, 3x1";
        }

        public bool AplicarPromo(List<Producto> carrito)
        {
            

            return false;
        }
    }
}

