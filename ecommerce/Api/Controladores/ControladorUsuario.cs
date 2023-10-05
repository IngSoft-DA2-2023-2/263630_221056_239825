using Api.Dtos;
using Dominio;
using Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using System.Security.Claims;

namespace Api.Controladores
{
    [ApiController]
    [Route("api/v1/usuarios")]
    public class ControladorUsuario : ControllerBase
    {
        private IManejadorUsuario _manejadorUsuario;
        private IServicioProducto _manejadorProducto;
        public ControladorUsuario(IManejadorUsuario manejadorUsuario, IServicioProducto manejadorProducto)
        {
            _manejadorUsuario = manejadorUsuario;
            _manejadorProducto = manejadorProducto;
        }

        [HttpPost]
        public IActionResult RegistrarUsuario([FromBody] UsuarioCrearModelo nuevoUsuario)
        {
            return Created("", _manejadorUsuario.RegistrarUsuario(nuevoUsuario.ToEntity()));
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            Usuario usuario = ValidarToken(HttpContext.User.Identity as ClaimsIdentity);
            if (usuario.Rol == CategoriaRol.Administrador || usuario.Rol == CategoriaRol.ClienteAdministrador || usuario.Id == id)
            {
                return Ok(_manejadorUsuario.ObtenerUsuario(id));
            }
            return Unauthorized();
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            Usuario usuario = ValidarToken(HttpContext.User.Identity as ClaimsIdentity);
            if (usuario.Rol == CategoriaRol.Administrador || usuario.Rol == CategoriaRol.ClienteAdministrador)
            {
                return Ok(_manejadorUsuario.ObtenerUsuarios());
            }
            return Unauthorized();
        }

        [HttpPut("{id}")]
        public IActionResult ModificarUsuario(int id, [FromBody] UsuarioCrearModelo usuario)
        {
            Usuario usuarioLogeado = ValidarToken(HttpContext.User.Identity as ClaimsIdentity);
            if (usuarioLogeado.Rol == CategoriaRol.Administrador || usuarioLogeado.Rol == CategoriaRol.ClienteAdministrador || usuarioLogeado.Id==id)
            {
                _manejadorUsuario.ActualizarUsuario(id, usuario.ToEntity());
                return Ok();
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            Usuario usuario = ValidarToken(HttpContext.User.Identity as ClaimsIdentity);
            if (usuario.Rol == CategoriaRol.Administrador || usuario.Rol == CategoriaRol.ClienteAdministrador)
            {
                Usuario usuarioAEliminar = _manejadorUsuario.ObtenerUsuario(id);
                _manejadorUsuario.EliminarUsuario(usuarioAEliminar);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpGet("{id}/compras")]
        public IActionResult BuscarCompras(int id)
        {
            Usuario usuario = ValidarToken(HttpContext.User.Identity as ClaimsIdentity);
            if (usuario.Id == id)
            {
                return Ok(_manejadorUsuario.ObtenerComprasDelUsuario(id));
            }
            return Unauthorized();
        }

        [HttpPost("{id}/compras")]
        public IActionResult RealizarCompra(int id, [FromBody] CompraCrearModelo compraCrearModelo)
        {
            
            Usuario usuario = ValidarToken(HttpContext.User.Identity as ClaimsIdentity);
            
            if (usuario.Id != id) return Unauthorized();
            
            Compra compra = new Compra()
            {
                UsuarioId = id,
                Productos = crearListaProductos(compraCrearModelo),
            };
            
            _manejadorUsuario.AgregarCompraAlUsuario(id, compra);
            return Created("", compraCrearModelo);
        }

        private List<Producto> crearListaProductos(CompraCrearModelo compraCrear)
        {
            List<Producto> resultado = new List<Producto>();
            foreach (int id in compraCrear.idProductos)
            {
                Producto producto = _manejadorProducto.EncontrarPorId(id);
                resultado.Add(producto);
            }
            return resultado;
        }

        private Usuario ValidarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity == null) throw new ArgumentNullException(nameof(identity));
                string id = identity.Claims.FirstOrDefault(x => x.Type == "id")!.Value;
                Usuario usuario = _manejadorUsuario.ObtenerUsuario(int.Parse(id));
                return usuario;
            } catch (Exception)
            {
                throw new UnauthorizedAccessException("No Autorizado");
            }
        }
    }
}
