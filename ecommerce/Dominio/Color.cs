using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Color
    {
        public int Id { get; set; }
        public int Nombre { get; set; }
        public List<Producto> Productos { get; set; }
    }
}
