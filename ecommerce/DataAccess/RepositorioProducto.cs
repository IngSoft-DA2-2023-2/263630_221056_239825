using System.Data;
using System.Data.Common;
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
    public List<Producto> RetornarLista()
    {
        return Contexto.Set<Producto>().ToList();
    }

    public Producto? EncontrarProductoPorId(int id)
    {
        return Contexto.Set<Producto>().First(p => p.Id == id);
    }
    
    public void AgregarProducto(Producto productoAgregado)
    {
        Contexto.Set<Producto>().Add(productoAgregado);
    }
    
    public void EliminarProducto(Producto productoABorrar)
    {
        Contexto.Set<Producto>().Remove(productoABorrar);
    }
    
    public void ModificarProducto(Producto productoNuevo, Producto productoAModificar)
    {
        Contexto.Entry(productoAModificar).CurrentValues.SetValues(productoNuevo);
    }
}