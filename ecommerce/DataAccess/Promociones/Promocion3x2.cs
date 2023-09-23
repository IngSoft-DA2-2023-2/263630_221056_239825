using System;
using Dominio;

namespace DataAccess.Promociones
{
	public class Promocion3x2: IPromocionStrategy
	{
		public Promocion3x2()
		{
		}

        public int AplicarPromocion(List<Producto> listaCompra)
        {
            if (listaCompra.Count < 3)
            {
                throw new InvalidOperationException("La promoción se aplica si hay, al menos, 3 productos en el carrito");
            }

            int costoTotal = 0;
            bool promoAplicada = false;

            foreach (Producto p in listaCompra)
            {
                costoTotal += p.Precio;
            }

                foreach (Producto p2 in listaCompra)
                {
                    int cant = 0;
                    int menorProducto = int.MaxValue;

                    foreach (Producto prod in listaCompra)
                    {
                        if (p2.Categoria == prod.Categoria)
                        {
                            cant++;
                            menorProducto = Math.Min(menorProducto, p2.Precio);
                        }
                    }
                    if (cant >= 2 && p2.Precio == menorProducto)
                    {
                        costoTotal -= menorProducto;
                        promoAplicada = true;                      
                    }
                }                      
                return costoTotal;            
        }

        public string NombrePromocion()
        {
            return "El producto de menor valor va de regalo";
        }

        public bool AplicarPromo(List<Producto> carrito)
        {
            //if (carrito == null || carrito.Count < 3)
            //{
            //    return false;
            //}
            //foreach (Producto prod in carrito)
            //{
            //    int contCategoria = 0;
            //    foreach (Producto p in carrito)
            //    {
            //        if (CoincideCategoria(prod, p))
            //        {
            //            contCategoria++;
            //            if (contCategoria >= 3)
            //            {
            //                return true;
            //            }
            //        }
            //    }
            //}
            return false;
        }

        //private static bool CoincideCategoria(Producto prod, Producto p)
        //{
        //    foreach (var c in prod.Categoria)
        //    {
        //        foreach (Categoria categoria in p.Categoria)
        //        {
        //            if (c.Id == categoria.Id)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}
    }
}

