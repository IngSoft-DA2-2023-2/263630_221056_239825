using Dominio;
using DataAccess;

public class ServicioProducto : IServicioProducto
{
    private IRepositorioProducto _repositorioProductos;

    public ServicioProducto(IRepositorioProducto repositorioProductos)
    {
        _repositorioProductos = repositorioProductos;
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

    public List<Producto> RetornarLista()
    {
        var listaDeProductos = _repositorioProductos.RetornarLista();

        return listaDeProductos;
    }
    
    public Producto EncontrarPorId(int idProducto)
    {
        var productoEncontrado = _repositorioProductos.EncontrarProductoPorId(idProducto);
        if (productoEncontrado is null)
        {
            throw new ArgumentException("El producto con el id dado no se encontro.");
        }

        return productoEncontrado;
    }
}