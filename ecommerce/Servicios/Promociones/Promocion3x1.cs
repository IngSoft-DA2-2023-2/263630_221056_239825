using System;
using Dominio;

namespace DataAccess.Promociones
{
    public class Promocion3x1 : IPromocionStrategy
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

            if (CoincideMarca(listaCompra))
            {
                var precioProds = listaCompra.OrderBy(p => p.Precio).ToList();

                for (int i = 2; i < listaCompra.Count; i++)
                {
                    costoTotal += precioProds[i].Precio;
                }
            }
            else
            {
                foreach (Producto producto in listaCompra)
                {
                    costoTotal += producto.Precio;
                }
            }

            return costoTotal;
        }

        private static bool CoincideMarca(List<Producto> listaCompra)
        {
            foreach (Producto p in listaCompra)
            {
                int cantProd = 0;

                foreach (Producto prod in listaCompra)
                {
                    if (p.Marca == prod.Marca)
                    {
                        cantProd++;
                    }

                    if (cantProd >= 3)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool AplicarPromo(List<Producto> carrito)
        {
            return carrito != null && carrito.Count >= 3;
        }
    }
}

