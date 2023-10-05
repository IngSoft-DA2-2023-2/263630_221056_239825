using DataAccess.Interfaces;
using Dominio;
using Dominio.Usuario;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        protected readonly DbContext Contexto;
        public RepositorioUsuario(DbContext contexto)
        {
            Contexto = contexto;
        }

        public Usuario AgregarUsuario(Usuario usuario)
        {
            Contexto.Set<Usuario>().Add(usuario);
            Contexto.SaveChanges();
            return usuario;
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            Contexto.Set<Usuario>().Update(usuario);
            Contexto.SaveChanges();
        }

        public Usuario ObtenerUsuario(Expression<Func<Usuario, bool>> criterio)
        {
            return Contexto.Set<Usuario>().First(criterio);
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return Contexto.Set<Usuario>().ToList();
        }

        public void EliminarUsuario(Usuario usuario)
        {
            Contexto.Set<Usuario>().Remove(usuario);
            Contexto.SaveChanges();
        }
    }
}