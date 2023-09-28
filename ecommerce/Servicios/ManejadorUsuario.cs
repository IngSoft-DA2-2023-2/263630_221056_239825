using Dominio;
using Dominio.Usuario;
using DataAccess.Interfaces;
using Servicios.Interfaces;

namespace Servicios
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
            if (ValidarUsuario(usuario)) {
                usuario = repositorioUsuario.AgregarUsuario(usuario);
            }
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

        public void ActualizarUsuario(int id, string direccionEntrega)
        {
            repositorioUsuario.ActualizarUsuario(id, direccionEntrega);
        }

        public void AgregarCompraAlUsuario(int id, Compra compra)
        {
            if (ValidarCompra(compra))
            {
                repositorioUsuario.AgregarCompraAlUsuario(id, compra);
            }
        }

        private bool ValidarCompra(Compra compra)
        {
            if (compra == null)
            {
                throw new ArgumentNullException("La compra no puede ser nula");
            }
            if (compra.Productos == null || compra.Productos.Count == 0)
            {
                throw new ArgumentException("La compra debe tener al menos un producto");
            }
            return true;
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

        public void EliminarUsuario(Usuario usuario)
        {
            repositorioUsuario.EliminarUsuario(usuario);
        }
    }
}