using Dominio;
using Dominio.Usuario;

namespace DataAccess.Interfaces
{
    public interface IRepositorioUsuario
    {
        Usuario AgregarUsuario(Usuario usuario);
        Usuario ObtenerUsuario(int id);
        List<Usuario> ObtenerUsuarios();
        void ActualizarUsuario(Usuario usuario);
        void EliminarUsuario(Usuario usuario);
    }
}
