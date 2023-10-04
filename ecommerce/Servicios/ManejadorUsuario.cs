using Dominio;
using Dominio.Usuario;
using DataAccess.Interfaces;
using Servicios.Interfaces;
using Servicios.Promociones;

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
            if (ValidarUsuario(usuario))
            {
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
            if (usuario.Contrasena.Length < 8)
            {
                throw new ArgumentException("Contraseña no valida");
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
                else if (repositorioUsuario.ObtenerUsuarios().FirstOrDefault(x => x.CorreoElectronico == usuario.CorreoElectronico) != null)
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

        public void ActualizarUsuario(int id, string direccionEntrega)
        {
            Usuario usuarioObtenido = repositorioUsuario.ObtenerUsuario(u => u.Id == id);
            usuarioObtenido.DireccionEntrega = direccionEntrega;
            repositorioUsuario.ActualizarUsuario(usuarioObtenido);
        }

        public void AgregarCompraAlUsuario(int id, Compra compra)
        {
            if (ValidarCompra(compra))
            {
                int precio = PrecioTotal(compra.Productos);
                string nombrePromo = "";
                PromocionContext promocionContext = new();
                List<IPromocionStrategy> promociones = new()
                {
                    new Promocion20Off(),
                    new Promocion3x1(),
                    new Promocion3x2(),
                    new PromocionTotalLook()
                };

                foreach (IPromocionStrategy promo in promociones)
                {
                    promocionContext.promocionStrategy = promo;
                    int precioConDescuento = promocionContext.AplicarStrategy(compra.Productos);
                    if (precioConDescuento < precio)
                    {
                        precio = precioConDescuento;
                        nombrePromo = promo.NombrePromocion;
                    }
                }
                compra.Precio = precio;
                compra.NombrePromo = nombrePromo;

                Usuario usuarioObtenido = repositorioUsuario.ObtenerUsuario(u => u.Id == id);
                usuarioObtenido.Compras.Add(compra);
                repositorioUsuario.ActualizarUsuario(usuarioObtenido);
            }
        }

        private int PrecioTotal(List<Producto> listaProductos)
        {
            int precio = 0;
            foreach (Producto p in listaProductos)
            {
                precio += p.Precio;
            }
            return precio;
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
            return repositorioUsuario.ObtenerUsuario(u => u.Id == id);
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
            Usuario usuario = repositorioUsuario.ObtenerUsuario(u => u.CorreoElectronico == correoElectronico && u.Contrasena == contrasena);
            return usuario;
        }
    }
}