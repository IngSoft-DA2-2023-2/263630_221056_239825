using Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Servicios.Interfaces;
using System.Security.Claims;

public class JwtAuthorizationFilter : Attribute, IAuthorizationFilter
{
    private readonly IManejadorUsuario _manejadorUsuario;

    public JwtAuthorizationFilter(IManejadorUsuario manejadorUsuario)
    {
        _manejadorUsuario = manejadorUsuario;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User.Identity as ClaimsIdentity;

        if (user == null || !user.IsAuthenticated)
        {
            context.Result = new ObjectResult(new { Message = "No Autorizado" })
            {
                StatusCode = 403
            };
            return;
        }

        try
        {
            Usuario usuario = ValidarToken(user);
        }
        catch (UnauthorizedAccessException e)
        {
            context.Result = new ObjectResult(new { Message = e.Message })
            {
                StatusCode = 403
            };
        }
    }

    private Usuario ValidarToken(ClaimsIdentity identity)
    {
        try
        {
            if (identity == null) throw new ArgumentNullException(nameof(identity));
            string id = identity.Claims.FirstOrDefault(x => x.Type == "id")?.Value!;

            if (string.IsNullOrEmpty(id))
            {
                throw new UnauthorizedAccessException("No Autorizado");
            }

            Usuario usuario = _manejadorUsuario.ObtenerUsuario(int.Parse(id));

            if (usuario == null)
            {
                throw new UnauthorizedAccessException("No Autorizado");
            }

            return usuario;
        }
        catch (Exception)
        {
            throw new UnauthorizedAccessException("No Autorizado");
        }
    }
}
