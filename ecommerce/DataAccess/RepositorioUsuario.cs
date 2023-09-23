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
            if (usuario is Cliente)
            {
                Contexto.Set<Cliente>().Add((Cliente) usuario);
            }
            else if (usuario is Administrador) 
            {
                Contexto.Set<Administrador>().Add((Administrador) usuario);
            }
            Contexto.SaveChanges();
            return usuario;
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            Contexto.Entry(usuario).State = EntityState.Modified;
            Contexto.SaveChanges();
        }

        public Usuario ObtenerUsuario(int id)
        {
            try
            {
                return Contexto.Set<Cliente>().First(u => u.Id == id);
            } catch (Exception)
            {
                return Contexto.Set<Administrador>().First(u => u.Id == id);
            }
        }

        public List<Cliente> ObtenerClientes()
        {
            return Contexto.Set<Cliente>().ToList();
        }

        public List<Administrador> ObtenerAdministradores()
        {
            return Contexto.Set<Administrador>().ToList();
        }

        public void EliminarUsuario(Usuario usuario)
        {
            if(usuario is Cliente)
            {
                Contexto.Set<Cliente>().Remove((Cliente) usuario);
            } else if(usuario is Administrador)
            {
                Contexto.Set<Administrador>().Remove((Administrador) usuario);
            }
            Contexto.SaveChanges();
        }
    }
}