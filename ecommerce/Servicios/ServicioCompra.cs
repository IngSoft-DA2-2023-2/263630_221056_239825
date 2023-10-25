using DataAccess.Interfaces;
using Dominio;
using Servicios.Interfaces;
using Servicios.Promociones;

namespace Servicios;

public class ServicioCompra : IServicioCompra
{
    private IRepositorioCompra _repositorio;
    private Promocion3xModelable _promocion3XModelable;
    private Promocion20Off _promocion20Off;
    private PromocionTotalLook _promocionTotalLook;

    public string NombrePromocion { get; set; }
    public int PrecioFinal { get; set; }

    public ServicioCompra(IRepositorioCompra repositorio)
    {
        _repositorio = repositorio;
    }

    public List<Compra> RetornarTodas() => _repositorio.RetornarTodas();
    public List<Compra> RetornarPorId(int id) => _repositorio.RetornarPorId(id);
    

    
    private void CalcularPrecioBase(List<Producto> listaProductos)
    {
        int precio = 0;
        foreach (Producto producto in listaProductos)
        {
            precio += producto.Precio;
        }

        PrecioFinal = precio;
        NombrePromocion = "Precio sin Descuentos";
    }
    

    public void DefinirMejorPrecio(List<Producto> productos)
    {
        int cantidad3x1 = 1;
        int cantidad3x2 = 2;
        int cantidad20Off = 20;
        int cantidadTotalLook = 50;
        CalcularPrecioBase(productos);
        CompararPrecio(_promocion3XModelable, productos, cantidad3x1);
        CompararPrecio(_promocion3XModelable, productos, cantidad3x2);
        CompararPrecio(_promocion20Off, productos, cantidad20Off);
        CompararPrecio(_promocionTotalLook, productos, cantidadTotalLook);
    }

    private void CompararPrecio(IPromocionStrategy promocionUsada, List<Producto> productos, int cantidadGratis)
    {
        int precioNuevo = promocionUsada.AplicarPromocion(cantidadGratis, productos);
        if (precioNuevo < PrecioFinal)
        {
            PrecioFinal = precioNuevo;
            NombrePromocion = promocionUsada.NombrePromocion;
        }
    }
}

