using Dominio;

public interface IServicioProducto
{
    int AgregarProducto(Producto productoAgregado);

    void EliminarProducto(Producto productoAgregado);

    void ModificarProducto(int id, Producto productoNuevo);

    List<Producto> RetornarLista(QueryProducto queryProducto);
    List<Categoria> RetornarCategorias();
    List<Color> RetornarColores();
    List<Marca> RetornarMarcas();

    Producto EncontrarPorId(int idProducto);
}