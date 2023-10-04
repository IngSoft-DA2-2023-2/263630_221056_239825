using Api.Dtos;
using Dominio;
using Microsoft.AspNetCore.Mvc;

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
        
        [HttpPost]
        public IActionResult AgregarProducto([FromBody] ProductoUpsertModelo productoNuevo)
        {
            int productoCreadoId = _servicioProducto.AgregarProducto(productoNuevo.AEntidad());
            Producto productoCreado = _servicioProducto.EncontrarPorId(productoCreadoId);
            return CreatedAtRoute(nameof(BuscarPorId), new { id = productoCreadoId }, new ProductoModelo(productoCreado));
        }
        
        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(int id)
        {
            Producto productoAEliminar = _servicioProducto.EncontrarPorId(id);
            _servicioProducto.EliminarProducto(productoAEliminar);
            return NoContent();
        }
        
        [HttpPut("{id}")]
        public IActionResult ModificarProducto(int id, [FromBody] ProductoUpsertModelo productoNuevo)
        {
            _servicioProducto.ModificarProducto(id, productoNuevo.AEntidad());
            Producto productoActualizado = _servicioProducto.EncontrarPorId(id);
            return Ok(new ProductoModelo(productoActualizado));
        }
    }
}