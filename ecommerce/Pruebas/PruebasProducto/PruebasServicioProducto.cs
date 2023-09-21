using DataAccess;
using Dominio;

namespace Pruebas.PruebasProducto;

[TestClass]
public class PruebasServicioProducto
{
    private Producto _productoDePrueba;
    private RepositorioProducto _repositorio;

    [TestInitialize]
    public void Init()
    {
        _repositorio = new RepositorioProducto();
        _productoDePrueba = new Producto("Producto 1", 99, "Este es un producto para pruebas");
        _productoDePrueba.Id = 01;
    }

    [TestMethod]
    public void AgregarProductoNuevo()
    {
        //Act
        _repositorio.AgregarProducto(_productoDePrueba);
        //Assert
        Assert.IsTrue(_repositorio.RetornarLista().Count == 1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AgregarProductoExistente()
    {
        //Act
        _repositorio.AgregarProducto(_productoDePrueba);
        _repositorio.AgregarProducto(_productoDePrueba);
    }

    [TestMethod]
    public void EliminarProductoExistente()
    {
        //Act
        _repositorio.AgregarProducto(_productoDePrueba);
        _repositorio.EliminarProducto(_productoDePrueba);
        //Assert
        Assert.IsTrue(_repositorio.RetornarLista().Count == 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void EliminarProductoInexistente()
    {
        //Act
        _repositorio.EliminarProducto(_productoDePrueba);
    }


    [TestMethod]
    public void ModificarProductoExistente()
    {
        //Arrange
        var productoDePrueba2 = new Producto("Producto modificado", 78, "Producto nuevo");
        productoDePrueba2.Id = 02;
        //Act
        _repositorio.AgregarProducto(_productoDePrueba);
        _repositorio.ModificarProducto(productoDePrueba2, _productoDePrueba);
        //Assert
        Assert.AreEqual(_repositorio.RetornarLista().First(), productoDePrueba2);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ModificarProductoInexistente()
    {
        //Arrange
        var productoDePrueba2 = new Producto("Producto modificado", 78, "Producto nuevo");
        productoDePrueba2.Id = 02;
        //Act
        _repositorio.ModificarProducto(productoDePrueba2, _productoDePrueba);
    }
}
