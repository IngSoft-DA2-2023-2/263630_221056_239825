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
            return Created("", _manejadorUsuario.RegistrarUsuario(nuevoUsuario.ToEntity()));
        }

        // Endpoint solo admin
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_manejadorUsuario.ObtenerUsuario(id));
        }

        // Endpoint solo admin
        [HttpGet]
        public IActionResult BuscarTodos()
        {
            return Ok(_manejadorUsuario.ObtenerUsuarios());
        }

        // Endpoint solo admin si id es distinta a la suya y es necesario logearse
        [HttpPatch("{id}")]
        public IActionResult ModificarUsuario([FromBody] UsuarioCrearModelo usuario)
        {
            throw new NotImplementedException();
        }

        // Endpoint solo admin
        [HttpDelete("{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            throw new NotImplementedException();
        }
    }
}
