using Dominio;
using DataAccess;

public class ManejadorProducto
{
    private IRepositorioProducto _repositorioProductos;

    public ManejadorProducto(IRepositorioProducto repoLocal)
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
}