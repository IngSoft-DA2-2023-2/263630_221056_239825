using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controladores
{
    [ApiController]
    [Route("api/v1/productos")]
    public class ControladorProductos : ControllerBase
    {
        private readonly ILogger<ControladorProductos> _logger;

        public ControladorProductos(ILogger<ControladorProductos> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Producto BuscarPorId(int id)
        {
            //logica de busqueda
            var marca = new Marca();
            var categoria = new Categoria();
            var colores = new List<Color>();
            var productoBuscado = new Producto("a", 2,"",marca, categoria, colores);
            return productoBuscado;
        }
        
        [HttpGet]
        public List<Producto> BuscarTodos()
        {
            //logica de busqueda
            var listaProductos = new List<Producto>();
            return listaProductos;
        }
    }
}