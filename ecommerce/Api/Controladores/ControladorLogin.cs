using Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using Api.Dtos;

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
        public IActionResult RegistrarSesion([FromBody] CredencialesControlador credenciales)
        {
            //Usuario usuario = _manejadorUsuario.login(credenciales.CorreoElectronico, credenciales.Contrasena);
            //return Created("", usuario);  
            throw new NotImplementedException();
        }
        [HttpDelete]
        public IActionResult EliminarSesion([FromHeader(Name = "Authorization")] string authorizationHeader) { 
            return Ok();
        }
    }
}
