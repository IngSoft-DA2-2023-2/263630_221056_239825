using Dominio;
using Dominio.Usuario;

namespace Api.Dtos
{
    public class UsuarioCrearModelo
    {
        public string CorreoElectronico { get; set; }
        public string DireccionEntrega { get; set; }
        public CategoriaRol Rol { get; set; }
        public string Contrasena { get; set; }

        public Usuario ToEntity()
        {
            return new Usuario(this.CorreoElectronico, this.DireccionEntrega, this.Contrasena)
            {
                Compras = new List<Compra>(),
                Rol = this.Rol,
            };
        }
    }
}
