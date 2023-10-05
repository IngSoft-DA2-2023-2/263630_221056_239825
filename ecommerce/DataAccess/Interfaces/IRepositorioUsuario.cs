using Dominio;
using Dominio.Usuario;
using System.Linq.Expressions;

namespace DataAccess.Interfaces
{
    public interface IRepositorioUsuario
    {
        Usuario AgregarUsuario(Usuario usuario);
        Usuario ObtenerUsuario(Expression<Func<Usuario, bool>> criterio);
        List<Usuario> ObtenerUsuarios();
        void ActualizarUsuario(Usuario usuario);
        void EliminarUsuario(Usuario usuario);
    }
}
