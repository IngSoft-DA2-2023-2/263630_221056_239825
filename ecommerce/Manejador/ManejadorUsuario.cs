using Dominio;
using Dominio.Usuario;
using Manejador.Interfaces;
using Repositorio.Interfaces;

namespace Manejador
{
    public class ManejadorUsuario : IManejadorUsuario
    {
        private readonly IRepositorioUsuario repositorioUsuario;
        public ManejadorUsuario(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario;
        }

        public Usuario RegistrarUsuario(Usuario usuario)
        {
            return repositorioUsuario.AgregarUsuario(usuario);
        }

        public void ActualizarUsuario(int id, string direccionEntrega)
        {
            repositorioUsuario.ActualizarUsuario(id, direccionEntrega);
        }

        public void AgregarCompraAlUsuario(int id, Compra compra)
        {
            repositorioUsuario.AgregarCompraAlUsuario(id, compra);
        }

        public List<Compra> ObtenerComprasDelUsuario(int id)
        {
            return repositorioUsuario.ObtenerComprasDelUsuario(id);
        }

        public Usuario ObtenerUsuario(int id)
        {
            return repositorioUsuario.ObtenerUsuario(id);
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return repositorioUsuario.ObtenerUsuarios();
        }
    }
}