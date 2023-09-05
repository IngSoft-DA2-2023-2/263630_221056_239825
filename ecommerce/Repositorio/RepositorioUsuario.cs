using Dominio;
using Dominio.Usuario;
using Repositorio.Interfaces;

namespace Repositorio
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

        public void ActualizarUsuario(int id, string direccionEntrega)
        {
            Usuario usuarioEncontrado = usuarios.First(u => u.Id == id);
            if (usuarioEncontrado != null)
            {
                usuarios.Remove(usuarioEncontrado);
                usuarioEncontrado.DireccionEntrega = direccionEntrega;
                usuarios.Add(usuarioEncontrado);
            }
            else
            {
                throw new ArgumentException("El usuario no existe");
            }
        }

        public void AgregarCompraAlUsuario(int id, Compra compra)
        {
            // Falta validar la compra
            Usuario usuarioEncontrado = usuarios.First(u => u.Id == id);
            if (usuarioEncontrado != null)
            {
                usuarios.Remove(usuarioEncontrado);
                usuarioEncontrado.Compras.Add(compra);
                usuarios.Add(usuarioEncontrado);
            }
            else
            {
                throw new ArgumentException("El usuario no existe");
            }
        }

        public Usuario AgregarUsuario(Usuario usuario)
        {
            if(ValidarUsuario(usuario)) usuarios.Add(usuario);
            return usuario;
        }

        private static bool ValidarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("El usuario no puede ser nulo");
            }
            if (usuario.DireccionEntrega == null || usuario.DireccionEntrega == "")
            {
                throw new ArgumentException("La direccion de entrega no puede ser nula o vacia");
            }
            if (usuario.CorreoElectronico == null || usuario.CorreoElectronico == "" || !usuario.CorreoElectronico.Contains('@'))
            {
                throw new ArgumentException("El email es incorrecto");
            }
            return true;
        }

        public List<Compra> ObtenerComprasDelUsuario(int id)
        {
            Usuario usuarioEncontrado = usuarios.First(u => u.Id == id);
            if (usuarioEncontrado != null)
            {
                return (usuarioEncontrado.Compras);
            }
            else
            {
                throw new ArgumentException("El usuario no existe");
            }
        }

        public Usuario ObtenerUsuario(int id)
        {
            Usuario usuarioEncontrado = usuarios.First(u => u.Id == id);
            if (usuarioEncontrado != null)
            {
                return (usuarioEncontrado);
            }
            else
            {
                throw new ArgumentException("El usuario no existe");
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return usuarios;
        }
    }
}