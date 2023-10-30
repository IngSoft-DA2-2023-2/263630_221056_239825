using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Dominio
{
    public class Compra
    {
        public int Id { get; set; }
        public List<Producto> Productos { get; set; }
        public int Precio { get; set; }
        public string NombrePromo { get; set; }
        public DateTime FechaCompra { get; set; } = DateTime.Now;
        public int UsuarioId { get; set; }
        public MetodoDePago MetodoDePago { get; set; }
    }
}
