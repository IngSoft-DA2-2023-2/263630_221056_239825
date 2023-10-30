using System;
using Dominio;

namespace Servicios.Promociones
{
    public class Promocion20Off : IPromocionStrategy
    {
        public string NombrePromocion { get; set; }
        public int costoTotal { get; set; }

        public int AplicarPromocion(int cantidadgratis, List<Producto> listaCompra)
        {
            NombrePromocion = $"Promocion de Fidelidad {cantidadgratis}% Off";
            costoTotal = 9999999;
            if (listaCompra.Count >= 2)
            {
                costoTotal = CalcularPrecioFinal(listaCompra);
            }
            return costoTotal;
        }
        
        private int CalcularPrecioFinal(List<Producto> listaCompra)
        {
            int precioTotal = 0;
            List<Producto> listaPorPrecio = listaCompra.OrderBy(p => p.Precio).ToList();

            listaPorPrecio = ExcluirProductosNoHabilitados(listaPorPrecio);
            if (listaPorPrecio.Count == 0)
            {
                return 9999999;
            }
            Producto productoMasCaro = listaPorPrecio.Last();
            int precioReducido = (80 * productoMasCaro.Precio) / 100;
            listaPorPrecio.Remove(productoMasCaro);
            foreach (Producto producto in listaPorPrecio)
            {
                precioTotal += producto.Precio;
            }

            precioTotal += precioReducido;
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

