using Dominio;
using DataAccess;

public class ServicioProducto
{
    private IRepositorioProducto _repositorioProductos;

    public ServicioProducto(IRepositorioProducto repoLocal)
    {
        _repositorioProductos = repoLocal;
    }

    public void AgregarProducto(Producto productoAgregado)
    {
        _repositorioProductos.AgregarProducto(productoAgregado);
    }

    public void EliminarProducto(Producto productoAgregado)
    {
        _repositorioProductos.EliminarProducto(productoAgregado);
    }
    
    public void ModificarProducto(Producto productoNuevo, Producto productoAModificar)
    {
        _repositorioProductos.ModificarProducto(productoNuevo, productoAModificar);
    }
}