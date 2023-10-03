using Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;

namespace Api.Controladores
{
    [ApiController]
    [Route("api/v1/authentication")]
    public class ControladorLogin : ControllerBase
    {
        private IManejadorUsuario _manejadorUsuario;
        public ControladorLogin(IManejadorUsuario manejadorUsuario)
        {
            _manejadorUsuario = manejadorUsuario;
        }
        [HttpPost]
        public IActionResult RegistrarSesion([FromBody] string mail, string password)
        {
            //Usuario usuario = _manejadorUsuario.login(mail, password);
            //return Created("", usuario);
            throw new NotImplementedException();
        }
        [HttpDelete]
        public IActionResult EliminarSesion([FromHeader(Name = "Authorization")] string authorizationHeader) { 
            return Ok();
        }
    }
}
