using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Usuario
{
    public class Cliente : Usuario
    {
        public Cliente(string correoElectronico, string direccionEntrega) : base(correoElectronico, direccionEntrega)
        {
        }
    }
}
