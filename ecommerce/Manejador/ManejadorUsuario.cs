using Dominio;
using Dominio.Usuario;
using Manejador.Interfaces;

namespace Manejador
{
    public class ManejadorUsuario : IManejadorUsuario
    {
        private readonly IManejadorUsuario manejadorUsuario;
        public ManejadorUsuario(IManejadorUsuario manejadorUsuario)
        {
            this.manejadorUsuario = manejadorUsuario;
        }

        public Usuario RegistrarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void ActualizarUsuario(int id, string direccionEntrega)
        {
            throw new NotImplementedException();
        }

        public void AgregarCompraAlUsuario(int id, Compra compra)
        {
            throw new NotImplementedException();
        }

        public List<Compra> ObtenerComprasDelUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario ObtenerUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> ObtenerUsuarios()
        {
            throw new NotImplementedException();
        }
    }
}