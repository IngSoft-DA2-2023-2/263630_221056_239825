using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Servicios.Promociones
{
    public interface IPromocionStrategy
    {
        public int AplicarPromocion(int cantidadGratis, List<Producto> listaCompra);
        public string NombrePromocion { get;}
    }
}
