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
                int cant = 0;
                int menorProducto = int.MaxValue;

                foreach (Producto p2 in listaCompra)
                {
                    costoTotal += p.Precio;
                    if (p2.Categoria == p.Categoria)
                    {
                        cant++;
                        menorProducto = Math.Min(menorProducto, p2.Precio);
                    }
                }

                if (cant >= 2 && p.Precio == menorProducto)
                {
                    p.Precio = 0; 
                    promoAplicada = true;
                }

                costoTotal -= p.Precio;
            }           
                return costoTotal;            
        }

        public string NombrePromocion()
        {
            return "El producto de menor valor va de regalo";
        }

        public bool AplicarPromo(List<Producto> carrito)
        {
            throw new NotImplementedException();
        }
    }
}

