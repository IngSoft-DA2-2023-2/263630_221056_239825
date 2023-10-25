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
        
        if (filtro.PrecioEspecifico is not null)
        {
            if (filtro.RangoPrecio is not null)
            {
                query = query.Where(p => p.Precio >= filtro.PrecioEspecifico - filtro.RangoPrecio && p.Precio <= filtro.PrecioEspecifico + filtro.RangoPrecio);
            }
            else
            {
                query = query.Where(p => p.Precio == filtro.PrecioEspecifico);
            }
        }
        
        if (filtro.MarcaId is not null)
        {
            query = query.Where(p => p.MarcaId == filtro.MarcaId);
        }
        
        if (filtro.CategoriaId is not null)
        {
            query = query.Where(p => p.CategoriaId == filtro.CategoriaId);
        }

        if (filtro.TienePromociones is not null)
        {
            query = query.Where(p => p.AplicaParaPromociones == filtro.TienePromociones);
        }
        
        return query
            .Include(p => p.Categoria)
            .Include(p => p.Marca)
            .Include(p => p.Color)
            .ToList();
    }

    public Producto? EncontrarProductoPorId(int id)
    {
        return Contexto.Set<Producto>()
            .Include(p => p.Categoria)
            .Include(p => p.Marca)
            .Include(p => p.Color)
            .FirstOrDefault(p => p.Id == id);
    }
    
    public void AgregarProducto(Producto productoAgregado)
    {
        Contexto.Entry(productoAgregado).State = EntityState.Added;
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
    }
}