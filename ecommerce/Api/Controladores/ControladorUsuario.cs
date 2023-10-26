using Api.Dtos;
using Api.Filtros;
using Dominio;
using Dominio.Usuario;
using Microsoft.AspNetCore.Identity;
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
        private IServicioCompra _manejadorCompra;

        public ControladorUsuario(IManejadorUsuario manejadorUsuario, IServicioProducto manejadorProducto, IServicioCompra manejadorCompra)
        {
            _manejadorUsuario = manejadorUsuario;
            _manejadorProducto = manejadorProducto;
            _manejadorCompra = manejadorCompra;
        }

        [HttpPost]
        public IActionResult RegistrarUsuario([FromBody] UsuarioCrearModelo nuevoUsuario)
        {
            return Created("", _manejadorUsuario.RegistrarUsuario(nuevoUsuario.ToEntity()));
        }

        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        // Ver con ALEX
        [FiltroAutorizacionId]
        [HttpGet("{id}")]
        [FiltroAutorizacionRol(RoleNeeded = CategoriaRol.Administrador, SecondaryRole = CategoriaRol.ClienteAdministrador)]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_manejadorUsuario.ObtenerUsuario(id));
        }

        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        [FiltroAutorizacionRol(RoleNeeded = CategoriaRol.Administrador, SecondaryRole = CategoriaRol.ClienteAdministrador)]
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            return Ok(_manejadorUsuario.ObtenerUsuarios());
        }

        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        // Ver con ALEX
        [FiltroAutorizacionId]
        [FiltroAutorizacionRol(RoleNeeded = CategoriaRol.Administrador, SecondaryRole = CategoriaRol.ClienteAdministrador)]
        [HttpPut("{id}")]
        public IActionResult ModificarUsuario(int id, [FromBody] UsuarioCrearModelo usuario)
        {
            _manejadorUsuario.ActualizarUsuario(id, usuario.ToEntity());
            return Ok();
        }

        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        [FiltroAutorizacionRol(RoleNeeded = CategoriaRol.Administrador, SecondaryRole = CategoriaRol.ClienteAdministrador)]
        [HttpDelete("{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            Usuario usuarioAEliminar = _manejadorUsuario.ObtenerUsuario(id);
            _manejadorUsuario.EliminarUsuario(usuarioAEliminar);
            return Ok();
        }

        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        [FiltroAutorizacionId]
        [HttpGet("{id}/compras")]
        public IActionResult BuscarCompras(int id)
        {
            List<Compra> listaCompras = _manejadorCompra.RetornarPorId(id);
            return Ok(listaCompras.Select(c => new CompraModelo(c)));
        }

        [ServiceFilter(typeof(JwtAuthorizationFilter))]
        [HttpPost("{id}/compras")]
        [FiltroAutorizacionRol(RoleNeeded = CategoriaRol.Cliente, SecondaryRole = CategoriaRol.ClienteAdministrador)]
        [FiltroAutorizacionId]
        public IActionResult RealizarCompra(int id, [FromBody] CompraCrearModelo compraCrearModelo)
        {
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
                if (producto.Stock > 0)
                {
                    resultado.Add(producto);
                }
            }
            return resultado;
        }
    }
}
