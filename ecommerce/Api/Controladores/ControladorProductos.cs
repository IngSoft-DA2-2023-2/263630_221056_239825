using DataAccess;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controladores
{
    [ApiController]
    [Route("api/v1/productos")]
    public class ControladorProductos : ControllerBase
    {
        private readonly ServicioProducto _servicioProducto = new ServicioProducto();
        private readonly ILogger<ControladorProductos> _logger;

        public ControladorProductos(ILogger<ControladorProductos> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Producto BuscarPorId(int id)
        {
            var productoBuscado = _servicioProducto.EncontrarPorId(id);

            return productoBuscado;
        }
        
        [HttpGet]
        public List<Producto> BuscarTodos()
        {
            var listaProductos = _servicioProducto.RetornarLista();
            return listaProductos;
        }
        
        [HttpPost("{nombre,precio,descripcion}")]
        public void AgregarProducto(string nombre, int precio, string descripcion)
        {
            var productoNuevo = new Producto(nombre, precio, descripcion);
            _servicioProducto.AgregarProducto(productoNuevo);
        }
        
        
        [HttpPatch("{id}")]
        public void EliminarProducto(int id)
        {
            var productoAEliminar = _servicioProducto.EncontrarPorId(id);
            _servicioProducto.EliminarProducto(productoAEliminar);
        }
        
        [HttpPatch("{id,nombre,precio,descripcion}")]
        public void ModificarProducto(int id, string nombre, int precio, string descripcion)
        {
            var productoNuevo = new Producto(nombre, precio, descripcion);
            var productoAReemplazar = _servicioProducto.EncontrarPorId(id);
            _servicioProducto.ModificarProducto(productoNuevo,productoAReemplazar);
        }
        
    }
}