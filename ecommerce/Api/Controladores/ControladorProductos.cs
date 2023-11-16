using Api.Dtos;
using Api.Filtros;
using Dominio;
using Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controladores
{
    [ApiController]
    [Route("api/v1/productos")]
    public class ControladorProductos : ControllerBase
    {
        private readonly IServicioProducto _servicioProducto;

        public ControladorProductos(IServicioProducto servicio)
        {
            _servicioProducto = servicio;
        }

        [HttpGet("{id}", Name = nameof(BuscarPorId))]
        public IActionResult BuscarPorId(int id)
        {
            Producto productoBuscado = _servicioProducto.EncontrarPorId(id);
            return Ok(new ProductoModelo(productoBuscado));
        }
        
        [HttpGet]
        public IActionResult BuscarTodos([FromQuery] QueryProducto query)
        {
            List<Producto> productos = _servicioProducto.RetornarLista(query);
            return Ok(productos.Select(p => new ProductoModelo(p)));
        }
        
        [FiltroAutorizacionRol(RoleNeeded = CategoriaRol.Administrador, SecondaryRole = CategoriaRol.ClienteAdministrador)]
        [HttpPost]
        public IActionResult AgregarProducto([FromBody] ProductoUpsertModelo productoNuevo)
        {
            int productoCreadoId = _servicioProducto.AgregarProducto(productoNuevo.AEntidad());
            Producto productoCreado = _servicioProducto.EncontrarPorId(productoCreadoId);
            return CreatedAtRoute(nameof(BuscarPorId), new { id = productoCreadoId }, new ProductoModelo(productoCreado));
        }
        
        [FiltroAutorizacionRol(RoleNeeded = CategoriaRol.Administrador, SecondaryRole = CategoriaRol.ClienteAdministrador)]
        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(int id)
        {
            Producto productoAEliminar = _servicioProducto.EncontrarPorId(id);
            _servicioProducto.EliminarProducto(productoAEliminar);
            return NoContent();
        }
        
        [FiltroAutorizacionRol(RoleNeeded = CategoriaRol.Administrador, SecondaryRole = CategoriaRol.ClienteAdministrador)]
        [HttpPut("{id}")]
        public IActionResult ModificarProducto(int id, [FromBody] ProductoUpsertModelo productoNuevo)
        {
            _servicioProducto.ModificarProducto(id, productoNuevo.AEntidad());
            Producto productoActualizado = _servicioProducto.EncontrarPorId(id);
            return Ok(new ProductoModelo(productoActualizado));
        }

        [HttpGet("colores")]
        public IActionResult ObtenerColores()
        {
            List<Color> colores = _servicioProducto.RetornarColores();
            return Ok(colores.Select(c => new ColorModelo(c)));
        }

        [HttpGet("categorias")]
        public IActionResult ObtenerCategorias()
        {
            List<Categoria> categorias = _servicioProducto.RetornarCategorias();
            return Ok(categorias.Select(c => new CategoriaModelo(c)));
        }

        [HttpGet("marcas")]
        public IActionResult ObtenerMarcas()
        {
            List<Marca> marcas = _servicioProducto.RetornarMarcas();
            return Ok(marcas.Select(m => new MarcaModelo(m)));
        }
    }
}