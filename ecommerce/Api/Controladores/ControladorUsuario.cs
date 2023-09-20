using Api.Dtos;
using Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using Servicios.Interfaces;

namespace Api.Controladores
{
    [ApiController]
    [Route("api/v1/usuarios")]
    public class ControladorUsuario : ControllerBase
    {
        private IManejadorUsuario _manejadorUsuario;
        public ControladorUsuario(IManejadorUsuario manejadorUsuario)
        {
            _manejadorUsuario = manejadorUsuario;
        }

        [HttpPost]
        public IActionResult RegistrarUsuario([FromBody] UsuarioCrearModelo nuevoUsuario)
        {
            _manejadorUsuario.RegistrarUsuario(nuevoUsuario.ToEntity());
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_manejadorUsuario.ObtenerUsuario(id));
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            return Ok(_manejadorUsuario.ObtenerUsuarios());
        }
    }
}
