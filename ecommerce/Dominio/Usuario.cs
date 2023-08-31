using Dominio.Enum;

namespace Dominio
{
    public class Usuario
    {
        public string CorreoElectronico { get; set; }
        public Roles Rol { get; set; }
        public string DireccionEntrega { get; set; }
        public List<Compra> Compras { get; set; }
        public bool EstaLogueado { get; set; }

        public Usuario(string correoElectronico, Roles rol, string direccionEntrega)
        {
            CorreoElectronico = correoElectronico;
            Rol = rol;
            DireccionEntrega = direccionEntrega;
            Compras = new List<Compra>();
            EstaLogueado = false;
        }
    }
}
