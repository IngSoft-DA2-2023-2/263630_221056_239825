using Dominio;

namespace DataAccess;

public interface IRepositorioProducto
{
    public List<Producto> RetornarLista();
    public Producto? EncontrarProductoPorId(int id);
    public void AgregarProducto(Producto productoAgregado);
    public void EliminarProducto(Producto productoAgregado);
    public void ModificarProducto(Producto productoNuevo, Producto productoAModificar);
}