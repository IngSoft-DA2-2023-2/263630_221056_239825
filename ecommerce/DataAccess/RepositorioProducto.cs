using Dominio;

namespace DataAccess;

public class RepositorioProducto : IRepositorioProducto
{
    private List<Producto> _listaProductos = new List<Producto>();

    public List<Producto> RetornarLista() => _listaProductos;

    public Producto? EncontrarProductoPorId(int id) => _listaProductos.FirstOrDefault(p => p.Id == id);
    
    public void AgregarProducto(Producto productoAgregado)
    {
        if (ExisteElProducto(productoAgregado))
        {
            throw new ArgumentException("El producto ya existe en el sistema.");
        }
        _listaProductos.Add(productoAgregado);

    }
    
    public void EliminarProducto(Producto productoABorrar)
    {

        if (!ExisteElProducto(productoABorrar))
        {
            throw new ArgumentException("El producto ya existe en el sistema.");
        }
        _listaProductos.Remove(productoABorrar);
    }
    
    public void ModificarProducto(Producto productoNuevo, Producto productoAModificar)
    {
        if (!ExisteElProducto(productoAModificar))
        {
            throw new ArgumentException("El producto especificado no fue encontrado.");
        }
        EliminarProducto(productoAModificar);
        AgregarProducto(productoNuevo);
    }

    private bool ExisteElProducto(Producto productoAgregado) => _listaProductos.FirstOrDefault(p => p.Id == productoAgregado.Id) is not null;
}