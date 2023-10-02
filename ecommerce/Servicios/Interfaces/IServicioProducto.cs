using Dominio;

public interface IServicioProducto
{
    int AgregarProducto(Producto productoAgregado);

    void EliminarProducto(Producto productoAgregado);

    void ModificarProducto(int id, Producto productoNuevo);

    List<Producto> RetornarLista(QueryProducto queryProducto);

    Producto EncontrarPorId(int idProducto);
}