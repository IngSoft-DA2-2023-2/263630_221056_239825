using Dominio;

public interface IServicioProducto
{
    void AgregarProducto(Producto productoAgregado);

    void EliminarProducto(Producto productoAgregado);

    void ModificarProducto(Producto productoNuevo, Producto productoAModificar);

    List<Producto> RetornarLista();

    Producto EncontrarPorId(int idProducto);
}