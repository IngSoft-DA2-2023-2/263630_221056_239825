using System;
using Dominio;

namespace Servicios.Promociones
{
    public class Promocion3xModelable : IPromocionStrategy
    {
        public string NombrePromocion { get; set; }
        public int costoTotal { get; set; }

        public int AplicarPromocion(int cantidadGratis, List<Producto> listaCompra)
        {
            NombrePromocion = $"Promocion de Fidelidad 3x{cantidadGratis}";
            costoTotal = 9999999;
            List<List<Producto>> listaPorCriterio = SepararEnListas(cantidadGratis, listaCompra);
            string variante = DefinirVariante(cantidadGratis);
            int cantidadGratisEspecifica = DefinirCantidadGratisEspecifica(cantidadGratis);
            if (listaCompra.Count >= 3)
            {
                costoTotal = CalcularPrecioFinal(cantidadGratisEspecifica, variante, listaPorCriterio);
            }
            return costoTotal;
        }

        private string DefinirVariante(int cantidadGratis)
        {
            string variante = "";
            if (cantidadGratis == 1)
            {
                variante = "3x2";
            }
            else
            {
                variante = "3x1";
            }

            return variante;
        }
        
        private List<List<Producto>> SepararEnListas(int cantidadGratis, List<Producto> listaCompra)
        {
            List<List<Producto>> listasPorCriterio = new List<List<Producto>>();
            if (cantidadGratis == 1)
            {
                foreach (Producto producto in listaCompra)
                {
                    listasPorCriterio.Add(CrearListaPorMarca(producto.MarcaId, listaCompra));
                }
            }
            else
            {
                foreach (Producto producto in listaCompra)
                {
                    listasPorCriterio.Add(CrearListaPorCategoria(producto.CategoriaId, listaCompra));
                }
            }

            return listasPorCriterio;
        }
        
        private int DefinirCantidadGratisEspecifica(int cantidadGratisBase)
        {
            int cantidadAjustada = cantidadGratisBase;
            if (cantidadGratisBase == 1)
            {
                cantidadAjustada++;
            } else if (cantidadGratisBase == 2)
            {
                cantidadAjustada--;
            }

            return cantidadAjustada;
        }
        private List<Producto> CrearListaPorMarca(int marcaId, List<Producto> listaCompra)
        {
            List<Producto> productosMismaMarca = new List<Producto>();
            foreach (Producto productoCompararMarca in listaCompra)
            {
                if (productoCompararMarca.MarcaId == marcaId)
                {
                    productosMismaMarca.Add(productoCompararMarca);
                }
            }

            return productosMismaMarca;
        }
        
        private List<Producto> CrearListaPorCategoria(int categoriaId, List<Producto> listaCompra)
        {
            List<Producto> productosMismaCategoria = new List<Producto>();
            foreach (Producto productoCompararCategoria in listaCompra)
            {
                if (productoCompararCategoria.CategoriaId == categoriaId)
                {
                    productosMismaCategoria.Add(productoCompararCategoria);
                }
            }

            return productosMismaCategoria;
        }

        private List<List<Producto>> EliminarDuplicadosMarca(List<List<Producto>> listasPorCriterio)
        {
            int idActual = -1;
            List<List<Producto>> listaDeReferencia = listasPorCriterio;
            int cantidadPorMarca = 0;
            for (var i = 0; i < listasPorCriterio.Count(); i++)
            {
                idActual = listasPorCriterio[i][0].MarcaId;
                foreach (List<Producto> listaParalela in listaDeReferencia)
                {
                    if (listaParalela[0].MarcaId == idActual)
                    {
                        cantidadPorMarca++;
                    }
                }

                if (cantidadPorMarca > 1)
                {
                    listaDeReferencia.Remove(listasPorCriterio[i]);
                }

                cantidadPorMarca = 0;
            }

            return listaDeReferencia;
        }
        
        private List<List<Producto>> EliminarDuplicadosCategoria(List<List<Producto>> listasPorCriterio)
        {
            int idActual = -1;
            List<List<Producto>> listaDeReferencia = listasPorCriterio;
            int cantidadPorCategoria = 0;
            for (var i = 0; i < listasPorCriterio.Count(); i++)
            {
                idActual = listasPorCriterio[i][0].CategoriaId;
                foreach (List<Producto> listaParalela in listaDeReferencia)
                {
                    if (listaParalela[0].CategoriaId == idActual)
                    {
                        cantidadPorCategoria++;
                    }
                }

                if (cantidadPorCategoria > 1)
                {
                    listaDeReferencia.Remove(listasPorCriterio[i]);
                }

                cantidadPorCategoria = 0;
            }

            return listaDeReferencia;
        }
        
        private int CalcularPrecioFinal(int cantidadGratis, string variante, List<List<Producto>> listasPorCriterio)
        {
            int precioTotal = 0;
            int indiceSumaPrecios = 0;

            listasPorCriterio = EliminarDuplicadosGeneral(variante, listasPorCriterio);

            for (int i = 0; i < listasPorCriterio.Count; i++)
            {
                listasPorCriterio[i] = ExcluirProductosNoHabilitados(listasPorCriterio[i]);
            }


            foreach (List<Producto> listaDeCadaMarca in listasPorCriterio)
            {
                
                indiceSumaPrecios = 0;
                
                if (listaDeCadaMarca.Count() >= 3)
                {
                    indiceSumaPrecios = cantidadGratis;
                } 
                
                List<Producto> listaPorPrecio = listaDeCadaMarca.OrderBy(producto => producto.Precio).ToList();

                
                while (indiceSumaPrecios <= listaPorPrecio.Count() - 1)
                {
                    precioTotal += listaPorPrecio[indiceSumaPrecios].Precio;
                    indiceSumaPrecios++;
                }
            }
            return precioTotal;
        }
        
        private List<Producto>  ExcluirProductosNoHabilitados(List<Producto> listaProcesada)
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

        private List<List<Producto>> EliminarDuplicadosGeneral(string variante, List<List<Producto>> listasPorCriterio)
        {
            if (variante.Equals("3x2"))
            {
                listasPorCriterio = EliminarDuplicadosCategoria(listasPorCriterio);
            }
            else
            {
                listasPorCriterio = EliminarDuplicadosMarca(listasPorCriterio);
            }

            return listasPorCriterio;
        }
        
    }
}

