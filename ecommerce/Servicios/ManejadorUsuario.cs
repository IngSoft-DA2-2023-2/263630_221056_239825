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

        private bool ValidarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("El usuario no puede ser nulo");
            }
            if (usuario.DireccionEntrega == null || usuario.DireccionEntrega == "")
            {
                throw new ArgumentException("La direccion de entrega no puede ser nula o vacia");
            }
            if (CheckearMail(usuario))
            {
                throw new ArgumentException("El email es incorrecto");
            } 
            if(usuario.Contrasena.Length < 8)
            {
                throw new ArgumentException("Contraseña no valida");
            }
            if(usuario.Rol.Count == 0)
            {
                usuario.Rol.Add(CategoriaRol.Cliente);
            }
            return true;
        }

        private bool CheckearMail(Usuario usuario)
        {
            try
            {
                if (usuario.CorreoElectronico == null || usuario.CorreoElectronico == "" || !usuario.CorreoElectronico.Contains('@'))
                {
                    return true;
                }
                else if (repositorioUsuario.ObtenerUsuarios().First(x => x.CorreoElectronico == usuario.CorreoElectronico) != null)
                {
                    return true;
                }
            } catch (Exception)
            {
                return false;
            }
            return false;
        }

        public void ActualizarUsuario(int id, string direccionEntrega)
        {
            Usuario usuarioObtenido = repositorioUsuario.ObtenerUsuario(id);
            usuarioObtenido.DireccionEntrega = direccionEntrega;
            repositorioUsuario.ActualizarUsuario(usuarioObtenido);
        }

        public void AgregarCompraAlUsuario(int id, Compra compra)
        {
            if (ValidarCompra(compra))
            {
                Usuario usuarioObtenido = repositorioUsuario.ObtenerUsuario(id);
                usuarioObtenido.Compras.Add(compra);
                repositorioUsuario.ActualizarUsuario(usuarioObtenido);
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
            Usuario usuarioObtenido = repositorioUsuario.ObtenerUsuario(id);
            return usuarioObtenido.Compras;
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