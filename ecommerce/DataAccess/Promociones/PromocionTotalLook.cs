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
            Dictionary<string, int> coloresCount = new Dictionary<string, int>();
            Dictionary<string, Producto> productosMasCarosPorColor = new Dictionary<string, Producto>();
            int costoTotal = 0;

            foreach(Producto p in listaCompra)
            {
                costoTotal += p.Precio;
            }

            // Paso 1: Contar cuántas veces aparece cada color y realizar un seguimiento del producto más caro de cada color.
            foreach (var producto in listaCompra)
            {
                foreach (var color in producto.Colores)
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

            // Paso 2: Buscar colores que se repiten al menos tres veces.
            foreach (var colorNombre in coloresCount.Keys)
            {
                if (coloresCount[colorNombre] >= 3)
                {
                    // Paso 3: Aplicar un 50% de descuento al producto más caro de ese color.
                    Producto productoMasCaro = productosMasCarosPorColor[colorNombre];
                    int prodMasCaro = productoMasCaro.Precio / 2;
                    return costoTotal -= prodMasCaro;
                }
            }


            // Si no se aplica descuento, puedes devolver un valor predeterminado, como -1.
            return -1;
        }

        public string NombrePromocion()
        {
            return "Se aplico un 50% de descuento en el producto de mayor valor";
        }

        public bool AplicarPromo(List<Producto> carrito)
        {
            if(carrito == null || carrito.Count < 3)
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

