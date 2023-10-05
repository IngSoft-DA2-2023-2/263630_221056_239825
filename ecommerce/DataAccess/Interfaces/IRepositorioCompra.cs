using Dominio;

namespace DataAccess.Interfaces;

public interface IRepositorioCompra
{
    List<Compra> RetornarTodas();
}