using Dominio;

namespace Servicios.Interfaces;

public interface IServicioCompra
{
    List<Compra> RetornarTodas();
    List<Compra> RetornarPorId(int id); 

    void DefinirMejorPrecio(Compra compra);
}