using Dominio;
using Dominio.Usuario;

namespace Manejador.Interfaces
{
    public interface IManejadorUsuario
    {
        Usuario ObtenerUsuario(int id);
        // Crea el usuario y devuelve el id
        Usuario RegistrarUsuario(Usuario usuario);
        List<Usuario> ObtenerUsuarios();
        void ActualizarUsuario(int id, string direccionEntrega);
        List<Compra> ObtenerComprasDelUsuario(int id);
        void AgregarCompraAlUsuario(int id, Compra compra);
    }
}
