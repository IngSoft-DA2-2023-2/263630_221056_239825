using Api.Dtos;
using Dominio;
using Dominio.Usuario;
using Microsoft.AspNetCore.JsonPatch;
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

        // Endpoint solo admin si id es distinta a la suya, si no, comprar token con su id y es necesario logearse
        [HttpPatch("{id}")]
        public IActionResult ModificarUsuario(int id, JsonPatchDocument<Usuario> usuarioModificado)
        {
            Usuario usuarioLogeado = ValidarToken(HttpContext.User.Identity as ClaimsIdentity);
            if (usuarioLogeado.Rol == CategoriaRol.Administrador || usuarioLogeado.Rol == CategoriaRol.ClienteAdministrador || usuarioLogeado.Id==id)
            {
                Usuario original = _manejadorUsuario.ObtenerUsuario(id);
                Usuario usuarioAModificar = original;
                usuarioModificado.ApplyTo(usuarioAModificar, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter) ModelState);
                //Falta modificar el usuario en el manejador
                return Ok();
            }
            return Ok(); // TODO Ver errores con manejador usuario
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
        public IActionResult RealizarCompra(int id, [FromBody] CompraModelo compraModelo)
        {
            
            Usuario usuario = ValidarToken(HttpContext.User.Identity as ClaimsIdentity);
            if(usuario.Id == id)
            {
                Compra compra = new Compra()
                {
                    Id = id,
                    Productos = crearListaProductos(compraModelo),
                };
                _manejadorUsuario.AgregarCompraAlUsuario(id, compra);
                return Created("", compraModelo);
            }
            return Unauthorized();
        }

        private List<Producto> crearListaProductos(CompraModelo compra)
        {
            List<Producto> resultado = new List<Producto>();
            foreach (int id in compra.idProductos)
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
