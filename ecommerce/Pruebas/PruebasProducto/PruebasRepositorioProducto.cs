using DataAccess;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Pruebas.PruebasProducto;

[TestClass]
public class PruebasRepositorioProducto
{
    private Producto _productoDePrueba;
    private ECommerceContext _contexto;
    private IRepositorioProducto _repositorio;
    private ECommerceContextFactory _contextFactory = new InMemoryECommerceContextFactory();

    [TestInitialize]
    public void Init()
    {
        _contexto = _contextFactory.CreateDbContext();
        _repositorio = new RepositorioProducto(_contexto);
        _productoDePrueba = GenerarProductoDummy();
        _productoDePrueba.Id = 1;
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

    [TestCleanup]
    public void Cleanup()
    {
        _contexto.Database.EnsureDeleted();
    }


    [TestMethod]
    public void RetornarLista_RetornaProductosFiltradosTest()
    {
        // Arrange
        var filtro = new QueryProducto
        {
            Nombre = _productoDePrueba.Nombre,
            Precio = _productoDePrueba.Precio,
            MarcaId = _productoDePrueba.MarcaId,
            CategoriaId = _productoDePrueba.CategoriaId
        };

        _contexto.Productos.AddRange(
            _productoDePrueba,
            new Producto
            {
                Id = 2,
                Nombre = "Producto2",
                Precio = 20,
                Descripcion = "Desc2",
                CategoriaId = _productoDePrueba.CategoriaId,
                Categoria = _productoDePrueba.Categoria,
                MarcaId = _productoDePrueba.MarcaId,
                Marca = _productoDePrueba.Marca,
                Colores = new List<Color>(),
            }
        );
        _contexto.SaveChanges();

        //Act
        var result = _repositorio.RetornarLista(filtro);
            
        // Assert
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(result.First(), _productoDePrueba);
    }
    
    [TestMethod]
    public void RetornarLista_RetornaTodosLosProductosTest()
    {
        // Arrange
        var filtroVacio = new QueryProducto();

        var listaProductos = new List<Producto>
        {
            _productoDePrueba,
            new ()
            {
                Id = 2,
                Nombre = "Producto2",
                Precio = 20,
                Descripcion = "Desc2",
                CategoriaId = _productoDePrueba.CategoriaId,
                Categoria = _productoDePrueba.Categoria,
                MarcaId = _productoDePrueba.MarcaId,
                Marca = _productoDePrueba.Marca,
                Colores = new List<Color>(),
            }
        };
        
        _contexto.Productos.AddRange(listaProductos);
        _contexto.SaveChanges();

        //Act
        var result = _repositorio.RetornarLista(filtroVacio);
            
        // Assert
        Assert.AreEqual(result.Count, 2);
        CollectionAssert.AreEqual(result, listaProductos);
    }

    [TestMethod]
    public void AgregarProductoNuevoTest()
    {
        //Act
        _repositorio.AgregarProducto(_productoDePrueba);
        _contexto.SaveChanges();
        
        //Assert
        var productoEnDb = _contexto.Productos.Find(_productoDePrueba.Id);
        Assert.AreEqual(productoEnDb, _productoDePrueba);
    }

    [TestMethod]
    public void EliminarProductoExistenteTest()
    {
        //Arrange
        _contexto.Productos.Add(_productoDePrueba);
        _contexto.SaveChanges();
        
        //Act
        _repositorio.EliminarProducto(_productoDePrueba);

        //Assert
        var productoEnDb = _contexto.Productos.Find(_productoDePrueba.Id);
        Assert.IsNotNull(productoEnDb);
    }

    [TestMethod]
    public void ModificarProductoExistenteTest()
    {
        //Arrange
        _contexto.Productos.Add(_productoDePrueba);
        _contexto.SaveChanges();

        var marcaColgateId = 2;
        var unaCategoria = 2;
        _productoDePrueba.Nombre = "PastaDentalXL";
        _productoDePrueba.MarcaId = marcaColgateId;
        _productoDePrueba.Colores = new List<Color>() { new () { Id = 3, Nombre = "Verde"} };
        
        //Act
        _repositorio.ModificarProducto(_productoDePrueba);
        
        //Assert
        var productoEnDb = _contexto.Productos.Include(p => p.Colores).FirstOrDefault(p => p.Id == _productoDePrueba.Id);
        Assert.AreEqual(productoEnDb!.Nombre, _productoDePrueba.Nombre);
        Assert.AreEqual(productoEnDb.Descripcion, _productoDePrueba.Descripcion);
        Assert.AreEqual(productoEnDb.Precio, _productoDePrueba.Precio);
        Assert.AreEqual(productoEnDb.MarcaId, _productoDePrueba.MarcaId);
        Assert.AreEqual(productoEnDb.CategoriaId, _productoDePrueba.CategoriaId);
        CollectionAssert.AreEqual(productoEnDb.Colores, _productoDePrueba.Colores);
    }
    
    [TestMethod]
    public void EncontrarProductoPorIdTest()
    {
        //Arrange
        _contexto.Productos.Add(_productoDePrueba);
        _contexto.SaveChanges();
        var prod = _contexto.Productos.FirstOrDefault(p => p.Id == _productoDePrueba.Id);
        
        //Act
        var productoEnDb = _repositorio.EncontrarProductoPorId(_productoDePrueba.Id);
        
        //Assert
        Assert.AreEqual(_productoDePrueba, productoEnDb);
    }
}
