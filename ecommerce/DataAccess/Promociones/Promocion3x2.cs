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
            if (listaCompra == null || listaCompra.Count < 3)
            {
                throw new InvalidOperationException("La promoción se aplica si hay, al menos, 3 productos en el carrito");
            }

            int costoTotal = 0;
            bool promoAplicada = false;

            foreach (Producto p in listaCompra)
            {
                costoTotal += p.Precio;
            }

            Dictionary<Categoria, int> categorias = new();

            foreach (Producto prod in listaCompra)
            {
                if (!categorias.ContainsKey(prod.Categoria))
                {
                    categorias[prod.Categoria] = 1;
                }
                else
                {
                    categorias[prod.Categoria]++;
                }
            }

            foreach (Producto p in listaCompra)
            {
                if (categorias[p.Categoria] >= 3)
                {
                    int menorPrecio = int.MaxValue;
                    foreach (Producto prod in listaCompra)
                    {
                        if (CoincideCategoria(prod, p) && prod.Precio < menorPrecio)
                        {
                            menorPrecio = prod.Precio;
                        }
                    }
                    costoTotal -= menorPrecio;
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
            if (carrito == null || carrito.Count < 3 || carrito.Any(p => p == null))
            {
                return false;
            }
            return true;
        }

        private static bool CoincideCategoria(Producto prod, Producto p)
        {
            return prod.Categoria == p.Categoria;
        }
    }
}

