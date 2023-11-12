using System.Reflection;
using DataAccess.Interfaces;
using Dominio;
using Servicios.Interfaces;
using Servicios.Promociones;

namespace Servicios;

public class ServicioCompra : IServicioCompra
{
    private IRepositorioCompra _repositorio;
    private List<IPromocionStrategy> _promocionesEncontradas;

    private string _nombrePromocionAplicada;
    private int _precioCalculado;

    public ServicioCompra(IRepositorioCompra repositorio)
    {
        _repositorio = repositorio;
        
    }

    public List<Compra> RetornarTodas() => _repositorio.RetornarTodas();
    public List<Compra> RetornarPorId(int id) => _repositorio.RetornarPorId(id);

    public void CargarPromociones()
    {
        if (!File.Exists("Promociones"))
        {
            throw new ArgumentException("The DLL file does not exist at the specified path.");
        }
        Assembly loadedAssembly;
        try
        {
            loadedAssembly = Assembly.LoadFrom("Promociones");
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Error loading the assembly: {ex.Message}");
        }
        
        Type[] types = loadedAssembly.GetTypes();

        foreach (Type type in types)
        {
            if (type.IsClass && typeof(IPromocionStrategy).IsAssignableFrom(type))
            {
                object instance = Activator.CreateInstance(type);
                IPromocionStrategy claseObtenida = (IPromocionStrategy)instance;
                _promocionesEncontradas.Add(claseObtenida);
                
            }
        }
    }
    
    
    
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
        CargarPromociones();
        foreach (var clase in _promocionesEncontradas)
        {
            CompararPrecio(clase, compra.Productos);
        }
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
    
    private void CompararPrecio(IPromocionStrategy promocionUsada, List<Producto> productos)
    {
        int precioNuevo = promocionUsada.AplicarPromocion(productos);
        if (precioNuevo < _precioCalculado)
        {
            _precioCalculado = precioNuevo;
            _nombrePromocionAplicada = promocionUsada.NombrePromocion;
        }
    }
}

