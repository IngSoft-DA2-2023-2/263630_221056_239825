using Dominio;
using Dominio.Usuario;

namespace Manejador.Interfaces
{
    public interface IManejadorUsuario
    {
        Usuario ObtenerUsuario(int id);
        void RegistrarUsuario(string correoElectronico, string direccionEntrega);
        List<Usuario> ObtenerUsuarios();
        void ActualizarUsuario(int id, string direccionEntrega);
        List<Compra> ObtenerComprasDelUsuario(int id);
        void AgregarCompraAlUsuario(int id, Compra compra);
    }
}
