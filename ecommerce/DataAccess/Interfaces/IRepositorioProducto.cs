using Dominio;

namespace DataAccess;

public interface IRepositorioProducto
{
    public void AgregarProducto(Producto productoAgregado);
    public void EliminarProducto(Producto productoAgregado);
    public void ModificarProducto(Producto productoNuevo, Producto productoAModificar);
}