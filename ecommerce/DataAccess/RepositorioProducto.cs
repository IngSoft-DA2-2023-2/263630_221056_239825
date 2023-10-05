using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class RepositorioProducto : IRepositorioProducto
{
    protected readonly DbContext Contexto;
    public RepositorioProducto(DbContext contexto)
    {
        Contexto = contexto;
    }
    public List<Producto> RetornarLista(QueryProducto filtro)
    {
        IQueryable<Producto> query = Contexto.Set<Producto>().AsQueryable();

        if (!string.IsNullOrEmpty(filtro.Nombre))
        {
            query = query.Where(p => p.Nombre.Contains(filtro.Nombre));
        }
        
        if (filtro.Precio is not null)
        {
            query = query.Where(p => p.Precio == filtro.Precio);
        }
        
        if (filtro.MarcaId is not null)
        {
            query = query.Where(p => p.MarcaId == filtro.MarcaId);
        }
        
        if (filtro.CategoriaId is not null)
        {
            query = query.Where(p => p.CategoriaId == filtro.CategoriaId);
        }

        return query
            .Include(p => p.Categoria)
            .Include(p => p.Marca)
            .Include(p => p.Colores)
            .ToList();
    }

    public Producto? EncontrarProductoPorId(int id)
    {
        return Contexto.Set<Producto>()
            .Include(p => p.Categoria)
            .Include(p => p.Marca)
            .Include(p => p.Colores)
            .FirstOrDefault(p => p.Id == id);
    }
    
    public void AgregarProducto(Producto productoAgregado)
    {
        Contexto.Entry(productoAgregado).State = EntityState.Added;
        productoAgregado.Colores = ConseguirColores(productoAgregado.Colores);
    }
    
    public void EliminarProducto(Producto productoABorrar)
    {
        Contexto.Set<Producto>().Remove(productoABorrar);
    }
    
    public void GuardarCambios()
    {
        Contexto.SaveChanges();
    }
    
    public void ModificarProducto(Producto productoNuevo)
    {
        Contexto.Set<Producto>().Update(productoNuevo);
        productoNuevo.Colores = ConseguirColores(productoNuevo.Colores);
    }

    private List<Color> ConseguirColores(List<Color> listaIds)
    {
        return listaIds
            .Select(color => Contexto.Set<Color>().FirstOrDefault(c => c.Id == color.Id))
            .Where(colorDB => colorDB is not null)
            .ToList();
    }

}