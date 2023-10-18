using Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Servicios.Interfaces;
using System.Security.Claims;

namespace Api.Filtros
{
    public class FiltroAutorizacionRol : Attribute, IAuthorizationFilter
    {
        public CategoriaRol RoleNeeded { get; set; }
        public CategoriaRol SecondaryRole { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User.Identity as ClaimsIdentity;


            if (user is null)
            {
                context.Result = new ObjectResult(new { Message = "Authorization header is missing" })
                {
                    StatusCode = 401
                };
            }
            try
            {
                IManejadorUsuario _manejadorUsuario = GetSessionService(context);
                Usuario usuario = ValidarToken(user!, _manejadorUsuario);
                if (!(RoleNeeded == usuario.Rol))
                {
                    if (SecondaryRole.Equals(default(CategoriaRol)))
                    {
                        context.Result = new ObjectResult(new { Message = "No se puede ejecutar la acción" })
                        {
                            StatusCode = 403
                        };
                    }
                    if(!(SecondaryRole == usuario.Rol))
                    {
                        context.Result = new ObjectResult(new { Message = "No se puede ejecutar la acción" })
                        {
                            StatusCode = 403
                        };
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                context.Result = new ObjectResult(new { Message = "No Autorizado" })
                {
                    StatusCode = 401
                };
            }   
        }

        private Usuario ValidarToken(ClaimsIdentity identity, IManejadorUsuario _manejadorUsuario)
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
        protected IManejadorUsuario GetSessionService(AuthorizationFilterContext context)
        {
            var sessionManagerObject = context.HttpContext.RequestServices.GetService(typeof(IManejadorUsuario));
            var sessionService = sessionManagerObject as IManejadorUsuario;

            return sessionService!;
        }
    }
}
