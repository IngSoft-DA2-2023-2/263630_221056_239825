using System;
using Dominio;

namespace DataAccess.Promociones
{
	public class Promocion3x1: IPromocionStrategy
	{
        public string NombrePromocion { get; set; } = "Se aplicó la promoción de Fidelidad, 3x1";

        public Promocion3x1()
		{
		}

        public int AplicarPromocion(List<Producto> listaCompra)
        {
            if (listaCompra == null || listaCompra.Count < 3)
            {
                throw new InvalidOperationException("La promoción se aplica si hay, al menos, 3 productos en el carrito");
            }

            int costoTotal = 0;

            foreach (Producto producto in listaCompra)
            {
                costoTotal += producto.Precio;
            }

            foreach (Producto producto in listaCompra)
            {
                if (CoincideMarca(listaCompra, producto))
                {
                    int menorProducto = int.MaxValue;
                    int cantidadProductos = 0;

                    foreach (Producto prod in listaCompra)
                    {
                        if (producto.Marca == prod.Marca)
                        {
                            cantidadProductos++;
                            menorProducto = Math.Min(menorProducto, prod.Precio);
                        }
                    }

                    if (cantidadProductos >= 3 && producto.Precio == menorProducto)
                    {
                        costoTotal -= menorProducto;
                    }
                }
            }

            return costoTotal;
        }

        private static bool CoincideMarca(List<Producto> listaCompra, Producto prod)
        {
            int cantMarca = 0;
            foreach (Producto p in listaCompra)
            {
                if (prod.Marca == p.Marca)
                {
                    cantMarca++;
                }
            }
            return cantMarca >= 3;
        }

        public bool AplicarPromo(List<Producto> carrito)
        {
            return carrito != null && carrito.Count >= 3;
        }
    }
}

