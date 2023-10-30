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

    private string _nombrePromocionAplicada;
    private int _precioCalculado;

    public ServicioCompra(IRepositorioCompra repositorio)
    {
        _repositorio = repositorio;
        _promocion3XModelable = new Promocion3xModelable();
        _promocion20Off = new Promocion20Off();
        _promocionTotalLook = new PromocionTotalLook();
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

        _precioCalculado = precio;
        _nombrePromocionAplicada = "Precio sin Descuentos";
    }
    
    public void DefinirMejorPrecio(Compra compra)
    {
        _precioCalculado = 0;
        int cantidad3x1 = 1;
        int cantidad3x2 = 2;
        int cantidad20Off = 20;
        int cantidadTotalLook = 50;
        CalcularPrecioBase(compra.Productos);
        CompararPrecio(_promocion3XModelable, compra.Productos, cantidad3x1);
        CompararPrecio(_promocion3XModelable, compra.Productos, cantidad3x2);
        CompararPrecio(_promocion20Off, compra.Productos, cantidad20Off);
        CompararPrecio(_promocionTotalLook, compra.Productos, cantidadTotalLook);
        compra.Precio = _precioCalculado;
        compra.NombrePromo = _nombrePromocionAplicada;
        AplicarDescuentoPaganza(compra);
    }

    private void AplicarDescuentoPaganza(Compra compra)
    {
        if (compra.MetodoDePago == MetodoDePago.Paganza)
        {
            compra.Precio = (compra.Precio / 10) * 9;
        }
    }
    
    private void CompararPrecio(IPromocionStrategy promocionUsada, List<Producto> productos, int cantidadGratis)
    {
        int precioNuevo = promocionUsada.AplicarPromocion(cantidadGratis, productos);
        if (precioNuevo < _precioCalculado)
        {
            _precioCalculado = precioNuevo;
            _nombrePromocionAplicada = promocionUsada.NombrePromocion;
        }
    }
}

