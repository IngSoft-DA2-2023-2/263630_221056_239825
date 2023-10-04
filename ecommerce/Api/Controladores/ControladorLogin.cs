using Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using Api.Dtos;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Controladores
{
    [ApiController]
    [Route("api/v1/authentication")]
    public class ControladorLogin : ControllerBase
    {
        private IManejadorUsuario _manejadorUsuario;
        private IConfiguration _configuration;
        public ControladorLogin(IManejadorUsuario manejadorUsuario, IConfiguration configuration)
        {
            _manejadorUsuario = manejadorUsuario;
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult RegistrarSesion([FromBody] CredencialesControlador credenciales)
        {
            Usuario usuario = _manejadorUsuario.Login(credenciales.CorreoElectronico, credenciales.Contrasena);
            JwtModelo jwt = _configuration.GetSection("Jwt").Get<JwtModelo>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", usuario.Id.ToString()),
                new Claim("correoElectronico", usuario.CorreoElectronico)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: signIn
            );
            return Created("", new JwtSecurityTokenHandler().WriteToken(token));  
        }

        [HttpDelete]
        public IActionResult EliminarSesion([FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            return Ok();
        }
    }
}
