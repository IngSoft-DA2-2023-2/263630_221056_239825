using Dominio;

namespace DataAccess;

public class RepositorioProducto : IRepositorioProducto
{
    private List<Producto> listaProductos = new List<Producto>();
    
    public void AgregarProducto(Producto productoAgregado)
    {
        listaProductos.Add(productoAgregado);
    }
    
    public void EliminarProducto(Producto productoABorrar)
    {
        listaProductos.Remove(productoABorrar);
    }
}