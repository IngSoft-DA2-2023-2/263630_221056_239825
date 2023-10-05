using System;
using Dominio;

namespace Servicios.Promociones
{
    public class Promocion20Off : IPromocionStrategy
    {
        public string NombrePromocion { get; } = "Se aplico un 20% de descuento en el producto de mayor valor";

        public Promocion20Off()
        {
        }

        public int AplicarPromocion(List<Producto> listaCompra)
        {
            if (listaCompra == null || listaCompra.Count < 2)
            {
                throw new InvalidOperationException("La promocion se aplica si hay, al menos, 2 productos en el carrito");
            }
            else
            {
                int costoTotal = 0;
                Producto productoMayorValor = listaCompra.OrderByDescending(p => p.Precio).First();
                int productoConDescuento = (int)(productoMayorValor.Precio * 0.20);
                foreach (Producto p in listaCompra)
                {
                    if (p == productoMayorValor)
                    {
                        costoTotal += p.Precio - productoConDescuento;
                    }
                    else
                    {
                        costoTotal += p.Precio;
                    }

                }
                return costoTotal;
            }
        }

        public bool AplicarPromo(List<Producto> carrito)
        {
            if (carrito == null || carrito.Count < 2 || carrito.Any(p => p == null))
            {
                return false;
            }
            return true;
        }

    }
}

