using Dominio;
using Dominio.Usuario;

namespace Api.Dtos
{
    public class UsuarioDTO
    {
        public string? CorreoElectronico { get; set; }
        public string? DireccionEntrega { get; set; }
        public CategoriaRol Rol { get; set; }

        public UsuarioDTO ToEntity(Usuario u)
        {
            return new UsuarioDTO()
            {
                CorreoElectronico = u.CorreoElectronico,
                DireccionEntrega = u.DireccionEntrega,
                Rol = u.Rol,
            };
        }
    }
}
