﻿using System;
using Dominio;

namespace DataAccess.Promociones
{
    public class Promocion3x2 : IPromocionStrategy
    {
        public string NombrePromocion { get; set; } = "El producto de menor valor va de regalo";

        public Promocion3x2()
        {
        }

        public int AplicarPromocion(List<Producto> listaCompra)
        {
            if (!AplicarPromo(listaCompra))
            {
                throw new NullReferenceException("La promoción se aplica si hay, al menos, 3 productos en el carrito");
            }

            int costoTotal = 0;
            //bool promoAplicada = false;

            //foreach (Producto p in listaCompra)
            //{
            //    costoTotal += p.Precio;
            //}

            Dictionary<Categoria, List<Producto>> categorias = new Dictionary<Categoria, List<Producto>>();

            foreach (Producto prod in listaCompra)
            {
                costoTotal += prod.Precio;

                if (!categorias.ContainsKey(prod.Categoria))
                {
                    categorias[prod.Categoria] = new List<Producto>();
                }
                categorias[prod.Categoria].Add(prod);
            }

            foreach (var categProd in categorias.Values)
            {
                if (categProd.Count >= 3)
                {
                    int menorPrecio = categProd.Min(p => p.Precio);
                    costoTotal -= menorPrecio;
                }
            }
            return costoTotal;
        }

        private static bool CoincideCategoria(Producto prod, Producto p)
        {
            return prod.Categoria == p.Categoria;
        }

        public bool AplicarPromo(List<Producto> carrito)
        {
            if (carrito == null || carrito.Count < 3 || carrito.Any(p => p == null))
            {
                return false;
            }
            return true;
        }


    }
}

