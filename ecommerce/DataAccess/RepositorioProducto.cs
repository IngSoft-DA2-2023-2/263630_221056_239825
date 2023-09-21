using System.Data;
using System.Data.Common;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class RepositorioProducto : IRepositorioProducto
{
    public List<Producto> RetornarLista()
    {
        List<Producto> listaDeProductos;
        using (var ctx = new ECommerceContext())
        {
            listaDeProductos = ctx.Productos.ToList();
        }

        return listaDeProductos;
    }

    public Producto? EncontrarProductoPorId(int id)
    {
        Producto productoARetornar;
        using (var ctx = new ECommerceContext())
        {
            productoARetornar = ctx.Productos.FirstOrDefault(p => p.Id == id);
        }

        return productoARetornar;
    }
    
    public void AgregarProducto(Producto productoAgregado)
    {
        try
        {
            using (var ctx = new ECommerceContext())
            {
                
                if (ExisteElProducto(productoAgregado))
                {
                    throw new ArgumentException("El producto ya existe en el sistema.");
                }
                
                ctx.Productos.Attach(productoAgregado);
                ctx.SaveChanges();
            }
        }
        catch (Exception x) when (x is DbException || x is DataException)
        {
            throw new ArgumentException("El contacto con la BD fallo");
        }
    }
    
    public void EliminarProducto(Producto productoABorrar)
    {
        try
        {
            using (var ctx = new ECommerceContext())
            {
                
                if (!ExisteElProducto(productoABorrar))
                {
                    throw new ArgumentException("El producto ya existe en el sistema.");
                }
                
                ctx.Categorias.Remove(productoABorrar.Categoria);
                ctx.Marcas.Remove(productoABorrar.Marca);
                ctx.Productos.Remove(productoABorrar);
                ctx.SaveChanges();
            }
        }
        catch (Exception x) when (x is DbException || x is DataException)
        {
            throw new ArgumentException("El contacto con la BD fallo");
        }
    }
    
    public void ModificarProducto(Producto productoNuevo, Producto productoAModificar)
    {
        var idOriginal = -999;
        if (!ExisteElProducto(productoAModificar))
        {
            throw new ArgumentException("El producto especificado no fue encontrado.");
        }

        idOriginal = productoAModificar.Id;
        productoNuevo.Id = idOriginal;
        EliminarProducto(productoAModificar);
        AgregarProducto(productoNuevo);
    }

    private bool ExisteElProducto(Producto productoAgregado) => EncontrarProductoPorId((productoAgregado.Id)) is null;
}