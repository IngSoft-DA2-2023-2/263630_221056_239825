using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controladores
{
    [ApiController]
    [Route("v1/ECommerce/Producto")]
    public class ControladorProductos : ControllerBase
    {
        private readonly ILogger<ControladorProductos> _logger;

        public ControladorProductos(ILogger<ControladorProductos> logger)
        {
            _logger = logger;
        }

        [HttpGet("{producto-id}")]
        public Producto BuscarPorId()
        {
            //logica de busqueda
            var marca = new Marca();
            var categoria = new Categoria();
            var colores = new List<Color>();
            var productoBuscado = new Producto("a", 2,"",marca, categoria, colores);
            return productoBuscado;
        }
        
        [HttpGet("{producto-todos}")]
        public List<Producto> BuscarTodos()
        {
            //logica de busqueda
            var listaProductos = new List<Producto>();
            return listaProductos;
        }
    }
}