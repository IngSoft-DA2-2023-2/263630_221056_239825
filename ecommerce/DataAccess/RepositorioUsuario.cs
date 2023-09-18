using DataAccess.Interfaces;
using Dominio;
using Dominio.Usuario;

namespace DataAccess
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private readonly IRepositorioUsuario repositorioUsuario;
        private List<Usuario> usuarios;
        public RepositorioUsuario(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario;
            usuarios = new List<Usuario>();
        }
        public Usuario AgregarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
            return usuario;
        }

        public void ActualizarUsuario(int id, string direccionEntrega)
        {
            try
            {
                Usuario usuarioEncontrado = usuarios.First(u => u.Id == id);
                EliminarUsuario(usuarioEncontrado);
                usuarioEncontrado.DireccionEntrega = direccionEntrega;
                AgregarUsuario(usuarioEncontrado);
            }
            catch (Exception)
            {
                throw new ArgumentException("El usuario no existe");
            }
        }

        public void AgregarCompraAlUsuario(int id, Compra compra)
        {
            try
            {
                Usuario usuarioEncontrado = usuarios.First(u => u.Id == id);
                EliminarUsuario(usuarioEncontrado);
                usuarioEncontrado.Compras.Add(compra);
                AgregarUsuario(usuarioEncontrado);
            }
            catch (Exception) { throw new ArgumentException("El usuario no existe"); }
        }


        public List<Compra> ObtenerComprasDelUsuario(int id)
        {
            try
            {
                Usuario usuarioEncontrado = usuarios.First(u => u.Id == id);
                return (usuarioEncontrado.Compras);
            }
            catch (Exception)
            {
                throw new ArgumentException("El usuario no existe");
            }
        }

        public Usuario ObtenerUsuario(int id)
        {
            try
            {
                Usuario usuarioEncontrado = usuarios.First(u => u.Id == id);
                return (usuarioEncontrado);
            }
            catch (Exception)
            {
                throw new ArgumentException("El usuario no existe");
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return usuarios;
        }

        public void EliminarUsuario(Usuario usuario)
        {
            bool esEliminado = usuarios.Remove(usuario);
            if(!esEliminado)
            {
                throw new ArgumentException("El usuario a eliminar no existe");
            }
        }
    }
}