using System;
using Dominio;

namespace DataAccess.Promociones
{
	public class Promocion20Off: IPromocionStrategy
	{
		public Promocion20Off()
		{
		}

        public int AplicarPromocion(List<Producto> listaCompra)
        {
            if(listaCompra.Count < 2)
            {
                throw new InvalidOperationException("La promocion se aplica si hay, al menos, 2 productos en el carrito");
            }
            else
            {
                int costoTotal = 0;
                Producto productoMayorValor = listaCompra.OrderByDescending(p => p.Precio).First();
                int productoConDescuento = (int)(productoMayorValor.Precio * 0.80);
                bool esPrimero = true;
                foreach(Producto p in listaCompra)
                {
                    if (esPrimero)
                    {
                        p.Precio = productoConDescuento;
                        esPrimero = false;
                    }
                    costoTotal += p.Precio;

                }

                return costoTotal;
            }           
        }

        public string NombrePromocion()
        {
            return "Se aplico un 20% de descuento en el producto de mayor valor";
        }

        public bool AplicarPromo(List<Producto> carrito)
        {
            if (carrito.Count < 2)
            {
                return false;
            }
            else
            {
                return true;
            }
            //Verificar que no sean nulos
        }

    }
}

