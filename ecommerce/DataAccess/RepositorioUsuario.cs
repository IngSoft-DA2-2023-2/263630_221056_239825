using DataAccess.Interfaces;
using Dominio;
using Dominio.Usuario;
using Microsoft.EntityFrameworkCore;

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
            return usuario;
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            Contexto.Entry(usuario).State = EntityState.Modified;
        }

        public Usuario ObtenerUsuario(int id)
        {
            return Contexto.Set<Usuario>().First(u => u.Id == id);
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return Contexto.Set<Usuario>().ToList();
        }

        public void EliminarUsuario(Usuario usuario)
        {
            Contexto.Set<Usuario>().Remove(usuario);
        }
    }
}