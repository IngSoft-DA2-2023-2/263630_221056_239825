using System.Collections.Generic;
using Dominio;
using Dominio.Usuario;
using Moq;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Promociones;

namespace Pruebas.PruebasPromociones;

[TestClass]
public class PruebasPromociones3x1
{/*
    private Mock<IPromocionStrategy>? mock;
    private Promocion3x1? promocion3x1;
    private Producto? producto;
    private Producto? productoVacio;
    private Producto? producto2;
    private Producto? producto3;
    private List<Producto>? carrito;

    [TestInitialize]
    public void InitTest()
    {
        mock = new Mock<IPromocionStrategy>();
        promocion3x1 = new Promocion3x1();

        List<string> marcas = new List<string> { "Zara" };
        List<string> categorias = new List<string> { "pantalones" };
        List<string> colores = new List<string> { "blanco" };
        producto = new Producto("Jean", 2000, "Largo y blanco", marcas, categorias, colores);

        productoVacio = null;

        List<string> categorias2 = new List<string> { "Blusas" };
        List<string> colores2 = new List<string> { "Lila" };
        producto2 = new Producto("Blusa", 800, "Manga larga", marcas, categorias, colores);

        List<string> categorias3 = new List<string> { "Abrigos" };
        List<string> colores3 = new List<string> { "Negro" };
        producto3 = new Producto("Campera", 3500, "Impermeable", marcas, categorias3, colores3);

        carrito = new List<Producto>
        {
            producto,
            producto2,
            producto3
        };
    }

    [TestMethod]
    public void AplicarPromocionOk()
    {
        //Act
        mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(3500);
        int costoTotal = promocion3x1!.AplicarPromocion(carrito!);
        // Assert
        Assert.AreEqual(3500, costoTotal);
    }

    [TestMethod]
    public void NombrePromocion()
    {
        //Act
        mock!.Setup(x => x.NombrePromocion()).Returns("Los dos productos de menor valor son gratis");
        string nombre = promocion3x1!.NombrePromocion();
        //Assert
        Assert.AreEqual("Los dos productos de menor valor son gratis", nombre);
    }

    [TestMethod]
    public void AplicaPromoOk()
    {
        //Act
        mock!.Setup(x => x.AplicarPromo(It.IsAny<List<Producto>>())).Returns(true);
        bool aplica = promocion3x1!.AplicarPromo(It.IsAny<List<Producto>>());
        //Assert
        Assert.AreEqual(true, aplica);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AplicarPromocionError()
    {
        //Act
        mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(3500);
        carrito!.Remove(producto!);
        List<string> marcaNueva = new List<string> { "Prune" };
        List<string> categoria4 = new List<string> { "Bolsos" };
        List<string> colores = new List<string> { "Azul" };
        Producto? productoDistintaMarca = new Producto("Cartera", 5000, "Bandolera", marcaNueva, categoria4, colores);
        carrito!.Add(productoDistintaMarca);
        int costoTotal = promocion3x1!.AplicarPromocion(carrito!);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void AplicarPromocionErrorNulo()
    {
        //Act
        mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(3500);
        carrito!.Remove(producto2!);
        carrito!.Add(productoVacio!);
        int costoTotal = promocion3x1!.AplicarPromocion(carrito!);
    }

    [TestMethod]
    public void AplicaPromoErrorNulo()
    {
        //Act
        mock!.Setup(x => x.AplicarPromo(It.IsAny<List<Producto>>())).Returns(false);
        bool aplica = promocion3x1!.AplicarPromo(It.IsAny<List<Producto>>());
        //Assert
        Assert.AreEqual(false, aplica);
    }

    [TestMethod]
    public void AplicaPromoError()
    {
        //Act
        mock!.Setup(x => x.AplicarPromo(It.IsAny<List<Producto>>())).Returns(false);
        carrito!.Remove(productoVacio!);
        bool aplica = promocion3x1!.AplicarPromo(It.IsAny<List<Producto>>());
        //Assert
        Assert.AreEqual(false, aplica);
    }*/
}

