using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Repositorio.Promociones
{
    public interface IPromocionStrategy
    {
        public int AplicarPromocion(List<Producto> listaCompra);

        public string NombrePromocion();

        bool AplicarPromo(List<Producto> carrito);
    }
}
