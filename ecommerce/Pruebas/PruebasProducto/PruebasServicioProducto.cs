using DataAccess;
using Dominio;
using Moq;

namespace Pruebas.PruebasProducto;

[TestClass]
public class PruebasServicioProducto
{
    private Mock<IRepositorioProducto> _mockRepositorio;
    private IServicioProducto _servicio;
    private Producto _productoDePrueba;

    [TestInitialize]
    public void Initialize()
    {
        _mockRepositorio = new Mock<IRepositorioProducto>();
        _servicio = new ServicioProducto(_mockRepositorio.Object);
        _productoDePrueba = GenerarProductoDummy();
    }
    
    private Producto GenerarProductoDummy()
    {
        var marcaDummy = new Marca() { Id = 1, Nombre = "Colgate" };
        var categoriaDummy = new Categoria() { Id = 1, Nombre = "Pasta dental" };
        var coloresDummies = new List<Color>() { new() { Id = 1, Nombre = "Azul" }, new() { Id = 2, Nombre = "Rojo" } };
        return new Producto("Pasta ProWhite", 99, "Esto es una pasta de dientes pro", marcaDummy.Id, categoriaDummy.Id, coloresDummies)
        {
            Categoria = categoriaDummy,
            Marca = marcaDummy,
        };
    }
    
     [TestMethod]
    public void AgregarProductoTest()
    {
        // Act
        _servicio.AgregarProducto(_productoDePrueba);

        // Assert
        _mockRepositorio.Verify(r => r.AgregarProducto(_productoDePrueba), Times.Once);
        _mockRepositorio.Verify(r => r.GuardarCambios(), Times.Once);
    }

    [TestMethod]
    public void EliminarProductoTest()
    {
        // Act
        _servicio.EliminarProducto(_productoDePrueba);

        // Assert
        _mockRepositorio.Verify(r => r.EliminarProducto(_productoDePrueba), Times.Once);
        _mockRepositorio.Verify(r => r.GuardarCambios(), Times.Once);
    }

    [TestMethod]
    public void ModificarProductoTest()
    {
        // Arrange
        var productoActualizado = new Producto()
        {
            Id = _productoDePrueba.Id,
            Nombre = "Nuevo actualizado",
            Precio = 45,
            Descripcion = "Nueva desc",
            CategoriaId = 5,
            MarcaId = 5,
            Colores = new List<Color>() { new() { Id = 3, Nombre = "Verde" } }
        };
        _mockRepositorio.Setup(r => r.EncontrarProductoPorId(_productoDePrueba.Id)).Returns(_productoDePrueba);

        // Act
        _servicio.ModificarProducto(_productoDePrueba.Id, productoActualizado);

        // Assert
        _mockRepositorio.Verify(r => r.ModificarProducto(It.Is<Producto>(p => p.Nombre == productoActualizado.Nombre)), Times.Once);
        _mockRepositorio.Verify(r => r.GuardarCambios(), Times.Once);
    }

    [TestMethod]
    public void EncontrarPorIdTest()
    {
        // Arrange
        var id = 1;
        var productoEsperado = new Producto { Id = id };
        _mockRepositorio.Setup(r => r.EncontrarProductoPorId(id)).Returns(productoEsperado);

        // Act
        var resultado = _servicio.EncontrarPorId(id);

        // Assert
        Assert.AreEqual(productoEsperado, resultado);
    }

    [TestMethod]
    public void RetornarLista_Test()
    {
        // Arrange
        var query = new QueryProducto();
        var productosEsperados = new List<Producto>
        {
            new () { Id = 1 },
            new () { Id = 2 },
            new () { Id = 3 }
        };

        _mockRepositorio.Setup(r => r.RetornarLista(query)).Returns(productosEsperados);

        // Act
        var resultado = _servicio.RetornarLista(query);

        // Assert
        CollectionAssert.AreEqual(productosEsperados, resultado);
    }
}