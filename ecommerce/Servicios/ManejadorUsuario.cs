using Dominio;
using Dominio.Usuario;
using DataAccess.Interfaces;
using Servicios.Interfaces;
using Servicios.Promociones;
using System.Security.Cryptography;
using System.Text;

namespace Servicios
{
    public class ManejadorUsuario : IManejadorUsuario
    {
        private readonly IRepositorioUsuario repositorioUsuario;
        private readonly IServicioCompra servicioCompra;
        public ManejadorUsuario(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario;
        }

        public Usuario RegistrarUsuario(Usuario usuario)
        {
            if (ValidarUsuario(usuario, true)) {
                string contrasenaHasheada = HashPasword(usuario.Contrasena, Salting(usuario.CorreoElectronico));
                usuario.Contrasena = contrasenaHasheada;
                usuario = repositorioUsuario.AgregarUsuario(usuario);
            }
            return usuario;
        }

        private byte[] Salting(string correoElectronico)
        {
            return new byte[correoElectronico.Length];
        }

        private string HashPasword(string password, byte[] salt)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                350000,
                HashAlgorithmName.SHA512,
                64);
            return Convert.ToHexString(hash);
        }

        private bool ValidarUsuario(Usuario usuario, bool esNuevo)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("El usuario no puede ser nulo");
            }
            if (usuario.DireccionEntrega == null || usuario.DireccionEntrega == "")
            {
                throw new ArgumentException("La direccion de entrega no puede ser nula o vacia");
            }
            if (CheckearMail(usuario, esNuevo))
            {
                throw new ArgumentException("El email es incorrecto");
            }
            if (usuario.Contrasena.Length < 8)
            {
                throw new ArgumentException("Contraseña no valida");
            }

            return true;
        }

        private bool CheckearMail(Usuario usuario, bool esNuevo)
        {
            try
            {
                if (usuario.CorreoElectronico == null || usuario.CorreoElectronico == "" || !usuario.CorreoElectronico.Contains('@'))
                {
                    return true;
                }
                else if (esNuevo && repositorioUsuario.ObtenerUsuarios().FirstOrDefault(x => x.CorreoElectronico == usuario.CorreoElectronico) != null)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public void ActualizarUsuario(int id, Usuario usuario)
        {
            Usuario usuarioObtenido = repositorioUsuario.ObtenerUsuario(u => u.Id == id);
            if (ValidarUsuario(usuario, false))
            {
                usuarioObtenido.CorreoElectronico = usuario.CorreoElectronico;
                usuarioObtenido.DireccionEntrega = usuario.DireccionEntrega;
                usuarioObtenido.Contrasena = usuario.Contrasena;
                usuarioObtenido.Rol = usuario.Rol;
            }
            repositorioUsuario.ActualizarUsuario(usuarioObtenido);
        }

        public void AgregarCompraAlUsuario(int id, Compra compra)
        {
            if (ValidarCompra(compra))
            {
                servicioCompra.DefinirMejorPrecio(compra);
                Usuario usuarioObtenido = repositorioUsuario.ObtenerUsuario(u => u.Id == id);
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
            Usuario usuarioObtenido = repositorioUsuario.ObtenerUsuario(u => u.Id == id);
            return usuarioObtenido.Compras;
        }

        public Usuario ObtenerUsuario(int id)
        {
            try
            {
                return repositorioUsuario.ObtenerUsuario(u => u.Id == id);
            } catch (Exception)
            {
                throw new KeyNotFoundException("No existe el usuario con la id dada");
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return repositorioUsuario.ObtenerUsuarios();
        }

        public void EliminarUsuario(Usuario usuario)
        {
            repositorioUsuario.EliminarUsuario(usuario);
        }

        public Usuario Login(string correoElectronico, string contrasena)
        {
            try
            {
                Usuario usuario = repositorioUsuario.ObtenerUsuario(u => u.CorreoElectronico == correoElectronico);
                if(VerifyPassword(contrasena, usuario.Contrasena, Salting(correoElectronico)))
                {
                    return usuario;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            } catch (Exception)
            {
                throw new KeyNotFoundException("Credenciales incorrectas");
            }
        }

        private bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, 350000, HashAlgorithmName.SHA512, 64);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}