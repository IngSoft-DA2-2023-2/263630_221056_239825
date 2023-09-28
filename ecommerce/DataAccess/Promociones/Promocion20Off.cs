using System;
using Dominio;

namespace DataAccess.Promociones
{
    public class Promocion20Off : IPromocionStrategy
    {
        public string NombrePromocion { get; set; } = "Se aplico un 20% de descuento en el producto de mayor valor";

        public Promocion20Off()
        {
        }

        public int AplicarPromocion(List<Producto> listaCompra)
        {
            if (listaCompra.Count < 2 || listaCompra == null)
            {
                throw new InvalidOperationException("La promocion se aplica si hay, al menos, 2 productos en el carrito");
            }
            else
            {
                int costoTotal = 0;
                Producto productoMayorValor = listaCompra.OrderByDescending(p => p.Precio).First();
                int productoConDescuento = (int)(productoMayorValor.Precio * 0.80);
                foreach (Producto p in listaCompra)
                {
                    p.Precio = productoConDescuento;
                    costoTotal += p.Precio;

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

