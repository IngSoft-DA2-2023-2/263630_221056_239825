using Dominio.Usuario;
using System.Security.Claims;

namespace Api.Dtos
{
    public class JwtModelo
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
