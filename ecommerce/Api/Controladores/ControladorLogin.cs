using Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using Api.Dtos;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Principal;

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
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.CorreoElectronico),
                new Claim("id", usuario.Id.ToString()),
                new Claim("rol", usuario.Rol.ToString()),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: signIn
            );
            return Created("", new JwtSecurityTokenHandler().WriteToken(token));  
        }
    }
}
