using Dominio;
using DataAccess;

public class ServicioProducto : IServicioProducto
{
    private IRepositorioProducto _repositorioProductos;

    public ServicioProducto(IRepositorioProducto repositorioProductos)
    {
        _repositorioProductos = repositorioProductos;
    }
    
    public int AgregarProducto(Producto productoAgregado)
    {
        _repositorioProductos.AgregarProducto(productoAgregado);
        GuardarCambios();
        return productoAgregado.Id;
    }

    public void EliminarProducto(Producto productoAgregado)
    {
        _repositorioProductos.EliminarProducto(productoAgregado);
        GuardarCambios();
    }
    
    public void ModificarProducto(int id, Producto productoNuevo)
    {
        var productoViejo = EncontrarPorId(id);
        if (productoViejo is null)
        {
            throw new KeyNotFoundException($"El producto de id = {id} no se encuentra");
        }

        productoViejo.Nombre = productoNuevo.Nombre;
        productoViejo.Precio = productoNuevo.Precio;
        productoViejo.Descripcion = productoNuevo.Descripcion;
        productoViejo.CategoriaId = productoNuevo.CategoriaId;
        productoViejo.MarcaId = productoNuevo.MarcaId;
        productoViejo.Colores = productoNuevo.Colores;
        
        _repositorioProductos.ModificarProducto(productoViejo);
        GuardarCambios();
    }

    public List<Producto> RetornarLista(QueryProducto queryProducto)
    {
        var listaDeProductos = _repositorioProductos.RetornarLista(queryProducto);

        return listaDeProductos;
    }
    
    public void GuardarCambios()
    {
        _repositorioProductos.GuardarCambios();
    }
    
    public Producto EncontrarPorId(int idProducto)
    {
        var productoEncontrado = _repositorioProductos.EncontrarProductoPorId(idProducto);
        if (productoEncontrado is null)
        {
            throw new KeyNotFoundException("El producto con el id dado no se encontro.");
        }

        return productoEncontrado;
    }
}