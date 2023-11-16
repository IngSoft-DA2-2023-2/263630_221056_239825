using System;
using Dominio;

namespace Servicios.Promociones
{
    public class PromocionTotalLookPrueba
    {
        public string NombrePromocion { get; set; }
        public int costoTotal { get; set; }

        public int AplicarPromocion(List<Producto> listaCompra)
        {
            NombrePromocion = $"Promocion TotalLook";
            costoTotal = 9999999;
            if (listaCompra.Count >= 3)
            {
                costoTotal = CalcularPrecioFinal(listaCompra);
            }

            return costoTotal;
        }

        private List<Producto> CrearListaPorColor(int colorId, List<Producto> listaCompra)
        {
            List<Producto> productosMismoColor = new List<Producto>();
            foreach (Producto productoCompararColor in listaCompra)
            {
                if (productoCompararColor.ColorId == colorId)
                {
                    productosMismoColor.Add(productoCompararColor);
                }
            }

            return productosMismoColor;
        }

        private List<List<Producto>> EliminarDuplicados(List<List<Producto>> listasPorColor)
        {
            int idActual = -1;
            List<List<Producto>> listaDeReferencia = listasPorColor;
            int cantidadPorColor = 0;
            for (var i = 0; i < listasPorColor.Count(); i++)
            {
                idActual = listasPorColor[i][0].ColorId;
                foreach (List<Producto> listaParalela in listaDeReferencia)
                {
                    if (listaParalela[0].ColorId == idActual)
                    {
                        cantidadPorColor++;
                    }
                }

                if (cantidadPorColor > 1)
                {
                    listaDeReferencia.Remove(listasPorColor[i]);
                }

                cantidadPorColor = 0;
            }

            return listaDeReferencia;
        }

        private int CalcularPrecioFinal(List<Producto> listaCompra)
        {
            int precioTotal = 0;
            List<List<Producto>> listasPorColor = new List<List<Producto>>();
            int indiceSumaPrecios = 0;
            foreach (Producto producto in listaCompra)
            {
                listasPorColor.Add(CrearListaPorColor(producto.ColorId, listaCompra));
            }

            listasPorColor = EliminarDuplicados(listasPorColor);
            
            for (int i = 0; i < listasPorColor.Count; i++)
            {
                listasPorColor[i] = ExcluirProductosNoHabilitados(listasPorColor[i]);
            }

            foreach (List<Producto> listaDeCadaColor in listasPorColor)
            {
                List<Producto> listaPorPrecio = listaDeCadaColor.OrderBy(producto => producto.Precio).ToList();

                indiceSumaPrecios = 0;

                if (listaPorPrecio.Count >= 3)
                {
                    Producto productoMasCaro = listaPorPrecio.Last();
                    listaPorPrecio.Remove(productoMasCaro);
                    precioTotal += productoMasCaro.Precio / 2;
                }


                while (indiceSumaPrecios <= listaPorPrecio.Count() - 1)
                {
                    precioTotal += listaPorPrecio[indiceSumaPrecios].Precio;
                    indiceSumaPrecios++;
                }
            }

            return precioTotal;
        }

        private List<Producto> ExcluirProductosNoHabilitados(List<Producto> listaProcesada)
        {
            List<Producto> listaFinal = new List<Producto>();
            foreach (Producto productoEnLista in listaProcesada)
            {
                if (productoEnLista.AplicaParaPromociones)
                {
                    listaFinal.Add(productoEnLista);
                }
            }
            return listaFinal;
        }
    }
}

