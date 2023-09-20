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
            return "Se aplico un 50% de descuento en el producto de mayor valor";
        }

        public bool AplicarPromo(List<Producto> carrito)
        {
            //if (carrito.Count < 3)
            //{
            //    return false; 
            //}

            //string colorActual = carrito[0].Color;
            //int cantidadProductosMismoColor = 1; 

            //for (int i = 1; i < carrito.Count; i++)
            //{
            //    if (carrito[i].Colores == colorActual)
            //    {
            //        cantidadProductosMismoColor++;
            //        if (cantidadProductosMismoColor >= 3)
            //        {
            //            return true; 
            //        }
            //    }
            //    else
            //    {
            //        cantidadProductosMismoColor = 1; 
            //        colorActual = carrito[i].Color;
            //    }
            //}
        }
    }
}

