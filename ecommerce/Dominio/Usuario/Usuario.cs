namespace Dominio.Usuario
{
    public class Usuario
    {
        public int Id { get; set; }
        public string CorreoElectronico { get; set; }
        public string DireccionEntrega { get; set; }
        public List<Compra> Compras { get; set; }
        public CategoriaRol Rol { get; set; }
        public string Contrasena { get; set; }
 
        public Usuario(string correoElectronico, string direccionEntrega, string contrasena)
        {
            CorreoElectronico = correoElectronico;
            DireccionEntrega = direccionEntrega;
            Compras = new List<Compra>();
            Contrasena = contrasena;
        }
    }
}
