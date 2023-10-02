namespace Dominio.Usuario
{
    public class Usuario
    {
        public int Id { get; set; }
        public string CorreoElectronico { get; set; }
        public string DireccionEntrega { get; set; }
        public List<Compra> Compras { get; set; }
        public List<CategoriaRol> Roles { get; set; }
        public string Contrasena { get; set; }
 
        public Usuario(string correoElectronico, string direccionEntrega, string contrasena)
        {
            CorreoElectronico = correoElectronico;
            DireccionEntrega = direccionEntrega;
            Compras = new List<Compra>();
            Roles = new List<CategoriaRol>();
            Contrasena = contrasena;
        }
    }
}
