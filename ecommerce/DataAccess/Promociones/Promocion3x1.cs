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
            //if (listaCompra.Count < 3 || listaCompra == null)
            //{
            //    throw new InvalidOperationException("La promoción se aplica si hay, al menos, 3 productos en el carrito");
            //}

            //int costoTotal = 0;
            //bool promoAplicada = false;
            ////int prodGratis = 0;

            //foreach (Producto p in listaCompra)
            //{
            //    costoTotal += p.Precio;
            //}

            //foreach (Producto p2 in listaCompra)
            //{
            //    int cant = 0;
            //    int menorProducto = int.MaxValue;

            //    foreach (Producto prod in listaCompra)
            //    {
            //        if (p2.Marca == prod.Marca)
            //        {
            //            cant++;
            //            menorProducto = Math.Min(menorProducto, p2.Precio);
            //        }
            //    }
            //    if (cant >= 2 && p2.Precio == menorProducto)
            //    {
            //        costoTotal -= menorProducto;
            //        promoAplicada = true;
            //    }
            //}
            //return costoTotal;
        }

        public string NombrePromocion()
        {
            return "Se aplico la promocion de Fidelidad, 3x1";
        }

        public bool AplicarPromo(List<Producto> carrito)
        {
            if (carrito == null || carrito.Count < 3 || carrito.Any(p => p == null))
            {
                return false;
            }
            return true;
        }
    }
}

