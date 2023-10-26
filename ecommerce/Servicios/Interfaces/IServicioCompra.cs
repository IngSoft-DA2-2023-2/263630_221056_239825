using Dominio;

namespace Servicios.Interfaces;

public interface IServicioCompra
{
    string NombrePromocion { get; set; } 
    int PrecioFinal { get; set; }
    List<Compra> RetornarTodas();
    List<Compra> RetornarPorId(int id); 

    void DefinirMejorPrecio(List<Producto> productos);
}