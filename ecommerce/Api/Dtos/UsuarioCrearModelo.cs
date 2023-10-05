using Dominio;
using Dominio.Usuario;

namespace Api.Dtos
{
    public class UsuarioCrearModelo
    {
        public string CorreoElectronico { get; set; }
        public string DireccionEntrega { get; set; }
        public CategoriaRol Rol { get; set; }
        public string contrasena { get; set; }

        public Usuario ToEntity()
        {
            return new Usuario(this.CorreoElectronico, this.DireccionEntrega, this.contrasena)
            {
                Compras = new List<Compra>(),
                Rol = this.Rol,
            };
        }
    }
}
