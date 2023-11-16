using System.Reflection;
using DataAccess.Interfaces;
using Dominio;
using Servicios.Interfaces;
using Servicios.Promociones;

namespace Servicios;

public class ServicioCompraPruebas : IServicioCompra
{
    private IRepositorioCompra _repositorio;
    private string _nombrePromocionAplicada;
    private int _precioCalculado;
    
    public ServicioCompraPruebas(IRepositorioCompra repositorio)
    {
        _repositorio = repositorio;
        
    }

    public List<Compra> RetornarTodas() => _repositorio.RetornarTodas();
    public List<Compra> RetornarPorId(int id) => _repositorio.RetornarPorId(id);

    public List<IPromocionStrategy> CargarPromociones()
    {
        List<IPromocionStrategy> _promocionesEncontradas = new List<IPromocionStrategy>();
        string pathApi = Directory.GetCurrentDirectory();
        string pathRelativo = Path.Combine("..\\..\\..\\..\\", "Promociones");
        string pathFinal = Path.GetFullPath(Path.Combine(pathApi, pathRelativo));

        string[] todosLosPaths = Directory.GetFiles(pathFinal);

        foreach (string pathEncontrado in todosLosPaths)
        {
            if (pathEncontrado.EndsWith(".dll"))
            {
                FileInfo informacionArchivo = new FileInfo(pathEncontrado);
                Assembly assembly = Assembly.LoadFile(informacionArchivo.FullName);

                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IPromocionStrategy).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        IPromocionStrategy promocion = (IPromocionStrategy)Activator.CreateInstance(type);
                        if (promocion != null)
                            _promocionesEncontradas.Add(promocion);
                    }
                }
            }
        }
        return _promocionesEncontradas;
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
        List<IPromocionStrategy> promocionesEncontradas = CargarPromociones();
        foreach (var clase in promocionesEncontradas)
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

