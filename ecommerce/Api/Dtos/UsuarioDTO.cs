using Dominio;
using Dominio.Usuario;

namespace Api.Dtos
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? DireccionEntrega { get; set; }
        public CategoriaRol Rol { get; set; }
        public List<Compra>? Compras { get; set; }
        public string Token { get; set; }

        public UsuarioDTO FromEntity(Usuario u)
        {
            return new UsuarioDTO()
            {
                CorreoElectronico = u.CorreoElectronico,
                DireccionEntrega = u.DireccionEntrega,
                Rol = u.Rol,
                Compras = u.Compras,
            };
        }
    }
}
