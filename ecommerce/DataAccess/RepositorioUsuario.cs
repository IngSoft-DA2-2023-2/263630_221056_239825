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

        public void AgregarCompra(Compra compra)
        {
            Contexto.Entry(compra).State = EntityState.Added;
            compra.Productos = ConseguirProductos(compra.Productos);
            Contexto.SaveChanges();
        }

        private List<Producto> ConseguirProductos(List<Producto> listaIds)
        {
            return listaIds
                .Select(producto => Contexto.Set<Producto>().FirstOrDefault(p => p.Id == producto.Id))
                .Where(ProductoDB => ProductoDB is not null)
                .ToList()!;
        }

        public Usuario AgregarUsuario(Usuario usuario)
        {
            Contexto.Entry(usuario).State = EntityState.Added;
            usuario.Compras = ConseguirCompras(usuario.Compras);
            Contexto.SaveChanges();
            return usuario;
        }
        private List<Compra> ConseguirCompras(List<Compra> listaIds)
        {
            return listaIds
                .Select(compra => Contexto.Set<Compra>().FirstOrDefault(c => c.Id == compra.Id))
                .Where(CompraDB => CompraDB is not null)
                .ToList()!;
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            Contexto.Set<Usuario>().Update(usuario);
            Contexto.SaveChanges();
        }

        public Usuario ObtenerUsuario(Expression<Func<Usuario, bool>> criterio)
        {
            return Contexto.Set<Usuario>()
                .Include(u => u.Compras)
                .First(criterio);
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return Contexto.Set<Usuario>()
                .Include(u => u.Compras)
                .ToList();
        }

        public void EliminarUsuario(Usuario usuario)
        {
            Contexto.Set<Usuario>().Remove(usuario);
            Contexto.SaveChanges();
        }
    }
}