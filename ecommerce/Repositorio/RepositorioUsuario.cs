using Dominio;
using Dominio.Usuario;
using Repositorio.Interfaces;

namespace Repositorio
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private readonly IRepositorioUsuario repositorioUsuario;
        public RepositorioUsuario(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario;
        }

        public void ActualizarUsuario(int id, string direccionEntrega)
        {
            throw new NotImplementedException();
        }

        public void AgregarCompraAlUsuario(int id, Compra compra)
        {
            throw new NotImplementedException();
        }

        public Usuario AgregarUsuario(Usuario usuario)
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