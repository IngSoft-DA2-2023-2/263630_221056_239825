using System;
using Dominio;

namespace Servicios.Promociones
{
    public class PromocionTotalLook : IPromocionStrategy
    {
        public string NombrePromocion { get;} = "Se aplico un 50% de descuento en el producto de mayor valor";

        public PromocionTotalLook()
        {
        }

        public int AplicarPromocion(List<Producto> listaCompra)
        {
            if (listaCompra == null || listaCompra.Count < 3)
            {
                throw new InvalidOperationException("La promoción se aplica si hay, al menos, 3 productos en el carrito");
            }

            Dictionary<string, int> coloresCount = new();
            Dictionary<string, Producto> productosMasCarosPorColor = new();
            int costoTotal = 0;

            foreach (Producto p in listaCompra)
            {
                costoTotal += p.Precio;
            }

            foreach (var producto in listaCompra)
            {
                foreach (Color color in producto.Colores)
                {
                    string colorNombre = color.Nombre;

                    if (coloresCount.ContainsKey(colorNombre))
                    {
                        coloresCount[colorNombre]++;
                        if (producto.Precio > productosMasCarosPorColor[colorNombre].Precio)
                        {
                            productosMasCarosPorColor[colorNombre] = producto;
                        }
                    }
                    else
                    {
                        coloresCount[colorNombre] = 1;
                        productosMasCarosPorColor[colorNombre] = producto;
                    }
                }
            }

            foreach (var colorNombre in coloresCount.Keys)
            {
                if (coloresCount[colorNombre] >= 3)
                {
                    Producto productoMasCaro = productosMasCarosPorColor[colorNombre];
                    int prodMasCaro = productoMasCaro.Precio / 2;
                    return costoTotal -= prodMasCaro;
                }

            }
            return -1;
        }

        public bool AplicarPromo(List<Producto> carrito)
        {
            if (carrito == null || carrito.Count < 3)
            {
                return false;
            }
            foreach (Producto prod in carrito)
            {
                int contColor = 0;
                foreach (Producto p in carrito)
                {
                    if (CoincideColor(prod, p))
                    {
                        contColor++;
                        if (contColor >= 3)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static bool CoincideColor(Producto prod, Producto p)
        {
            if (prod == null || p == null)
            {
                throw new ArgumentNullException("el producto es null");
            }
            foreach (Color color in prod.Colores)
            {
                foreach (Color c in p.Colores)
                {
                    if (color.Id == c.Id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

