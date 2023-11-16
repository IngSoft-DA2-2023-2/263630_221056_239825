using DataAccess;
using DataAccess.Interfaces;
using Dominio;
using Moq;
using Servicios;
using Servicios.Interfaces;

namespace Pruebas.PruebasCompra;

[TestClass]
public class PruebasServicioCompra
{
    private IServicioCompra _servicio;
    private Mock<IRepositorioCompra> _repositorio;
    
    [TestInitialize]
    public void Init()
    {
        _repositorio = new Mock<IRepositorioCompra>(MockBehavior.Strict);
        _servicio = new ServicioCompraPruebas(_repositorio.Object);
    }

    private List<Producto> GenerarProductosMismaCategoria() => new List<Producto>()
    {
        new Producto() {Id = 1, Nombre = "P1", Precio = 100, Descripcion = "Desc1", MarcaId = 1, CategoriaId = 1, ColorId = 1, AplicaParaPromociones = true, Stock = 4},
        new Producto() {Id = 2, Nombre = "P2", Precio = 200, Descripcion = "Desc2", MarcaId = 2, CategoriaId = 1, ColorId = 2, AplicaParaPromociones = true, Stock = 4},
        new Producto() {Id = 3, Nombre = "P2", Precio = 300, Descripcion = "Desc2", MarcaId = 2, CategoriaId = 1, ColorId = 2, AplicaParaPromociones = true, Stock = 4},
    };
    
    private List<Producto> GenerarProductosMismaMarca() => new List<Producto>()
    {
        new Producto() {Id = 4, Nombre = "P4", Precio = 400, Descripcion = "Desc1", MarcaId = 1, CategoriaId = 1, ColorId = 1, AplicaParaPromociones = true, Stock = 4},
        new Producto() {Id = 5, Nombre = "P5", Precio = 500, Descripcion = "Desc2", MarcaId = 1, CategoriaId = 2, ColorId = 2, AplicaParaPromociones = true, Stock = 4},
        new Producto() {Id = 6, Nombre = "P6", Precio = 600, Descripcion = "Desc2", MarcaId = 1, CategoriaId = 2, ColorId = 2, AplicaParaPromociones = true, Stock = 4},
    };
    
    private List<Producto> GenerarProductosMismoColor() => new List<Producto>()
    {
        new Producto() {Id = 7, Nombre = "P7", Precio = 700, Descripcion = "Desc1", MarcaId = 1, CategoriaId = 1, ColorId = 1, AplicaParaPromociones = true, Stock = 4},
        new Producto() {Id = 8, Nombre = "P8", Precio = 800, Descripcion = "Desc2", MarcaId = 2, CategoriaId = 2, ColorId = 1, AplicaParaPromociones = true, Stock = 4},
        new Producto() {Id = 9, Nombre = "P9", Precio = 900, Descripcion = "Desc2", MarcaId = 2, CategoriaId = 2, ColorId = 1, AplicaParaPromociones = true, Stock = 4},
    };
    
    private List<Producto> GenerarProductosNoPromo() => new List<Producto>()
    {
        new Producto() {Id = 10, Nombre = "P10", Precio = 110, Descripcion = "Desc2", MarcaId = 2, CategoriaId = 2, ColorId = 1, AplicaParaPromociones = false, Stock = 4},
        new Producto() {Id = 11, Nombre = "P11", Precio = 110, Descripcion = "Desc2", MarcaId = 2, CategoriaId = 2, ColorId = 1, AplicaParaPromociones = false, Stock = 4},
    };
    
    
    private Compra GenerarCompras3x1() 
    {
        var productosMarca = GenerarProductosMismaMarca();
        var compra = new Compra() { Id = 1, Productos = productosMarca };
        compra.MetodoDePago = MetodoDePago.Paypal;
        return compra;
    }
    
    private Compra GenerarCompras3x2()
    {
        var productosCategoria = GenerarProductosMismaCategoria();
        var compra = new Compra() { Id = 1, Productos = productosCategoria };
        compra.MetodoDePago = MetodoDePago.Paypal;
        return compra;
    }

    
    private Compra GenerarCompras20Off()
    {
        var productos20Off = new List<Producto>();
        productos20Off.Add(GenerarProductosMismaCategoria().First());
        productos20Off.Add(GenerarProductosMismoColor().First());
        var compra = new Compra() { Id = 1, Productos = productos20Off };
        compra.MetodoDePago = MetodoDePago.Paypal;
        return compra;
    }
    
    private Compra GenerarComprasTotalLook()
    {
        var productosColor = GenerarProductosMismoColor();
        var compra = new Compra() { Id = 1, Productos = productosColor };
        compra.MetodoDePago = MetodoDePago.Paypal;
        return compra;
    }
    
    private Compra GenerarComprasBase()
    {
        var productosBase = new List<Producto>();
        productosBase.Add(GenerarProductosNoPromo().First());
        productosBase.Add(GenerarProductosNoPromo().Last());
        var compra = new Compra() { Id = 1, Productos = productosBase };
        compra.MetodoDePago = MetodoDePago.Paypal;
        return compra;
    }

    
    [TestMethod]
    public void DefinirMejorPrecio3x1()
    {
        //Arrange
        var compraDePrueba = GenerarCompras3x1();

        //Act
        _servicio.DefinirMejorPrecio(compraDePrueba);

        //Assert
        Assert.AreEqual(compraDePrueba.Precio, 600);
    }
    
    [TestMethod]
    public void DefinirMejorPrecio3x2()
    {
        //Arrange
        var compraDePrueba = GenerarCompras3x2();

        //Act
        _servicio.DefinirMejorPrecio(compraDePrueba);

        //Assert
        Assert.AreEqual(compraDePrueba.Precio, 500);
    }

    [TestMethod]
    public void DefinirMejorPrecioTotalLook()
    {
        //Arrange
        var compraDePrueba = GenerarComprasTotalLook();

        //Act
        _servicio.DefinirMejorPrecio(compraDePrueba);

        //Assert
        Assert.AreEqual(compraDePrueba.Precio, 1950);
    }
    
    [TestMethod]
    public void DefinirMejorPrecio200ff()
    {
        //Arrange
        var compraDePrueba = GenerarCompras20Off();

        //Act
        _servicio.DefinirMejorPrecio(compraDePrueba);

        //Assert
        Assert.AreEqual(compraDePrueba.Precio, 660);
    }
    
    [TestMethod]
    public void DefinirMejorPrecioBase()
    {
        //Arrange
        var compraDePrueba = GenerarComprasBase();

        //Act
        _servicio.DefinirMejorPrecio(compraDePrueba);

        //Assert
        Assert.AreEqual(compraDePrueba.Precio, 220);
    }
    
    [TestMethod]
    public void DefinirMejorPrecioPaganza()
    {
        //Arrange
        var compraDePrueba = GenerarComprasBase();
        compraDePrueba.MetodoDePago = MetodoDePago.Paganza;
        //Act
        _servicio.DefinirMejorPrecio(compraDePrueba);

        //Assert
        Assert.AreEqual(compraDePrueba.Precio, 198);
    }
    
}