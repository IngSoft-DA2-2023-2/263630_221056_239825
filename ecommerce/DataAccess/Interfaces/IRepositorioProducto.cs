using System.Linq.Expressions;
using Dominio;

namespace DataAccess;

public interface IRepositorioProducto
{
    public List<Producto> RetornarLista(QueryProducto filtro);
    public Producto? EncontrarProductoPorId(int id);
    public void AgregarProducto(Producto productoAgregado);
    public void EliminarProducto(Producto productoAgregado);
    public void ModificarProducto(Producto productoNuevo);
    public void GuardarCambios();
}