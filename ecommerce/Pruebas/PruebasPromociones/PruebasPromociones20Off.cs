using System.Collections.Generic;
using Dominio;
using Dominio.Usuario;
using Moq;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Promociones;

<<<<<<< HEAD
namespace Pruebas.PruebasPromociones
{
=======
namespace Pruebas;


[TestClass]
public class PruebasPromociones20Off
{/*
    private Mock<IPromocionStrategy>? mock;
    private Promocion20Off? promocion20;
    private Producto? producto;
    private Producto? productoVacio;
    private Producto? producto2;
    private List<Producto>? carrito;
>>>>>>> dcc2fb3bad0e5450b648dbc40d426feeac86a582

    [TestClass]
    public class PruebasPromociones20Off
    {
        private Mock<IPromocionStrategy>? mock;
        private Promocion20Off? promocion20;
        private Producto? producto;
        private Producto? productoVacio;
        private Producto? producto2;
        private List<Producto>? carrito;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPromocionStrategy>();
            promocion20 = new Promocion20Off();
            Marca marca = new Marca { "Zara" };
            Categoria categoria = new Categoria { "pantalones" };
            List<Color> color = new List<Color> { "blanco" };
            producto = new Producto("Jean", 2000, "Largo y blanco", marca, categoria, color);
            productoVacio = null;
            Marca marca2 = new Marca {"Levis" };
            Categoria categoria2 = new Categoria { "Blusas" };
            List<Color> color2 = new List<Color> { "Lila" };
            producto2 = new Producto("Blusa", 1890, "Manga larga", marca2, categoria2, color2);
            carrito = new List<Producto>();
            carrito.Add(producto);
            carrito.Add(producto2);
        }

        [TestMethod]
        public void AplicarPromocionOk()
        {
            //Act
            mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(3490);
            int costoTotal = promocion20!.AplicarPromocion(carrito!);
            // Assert
            Assert.AreEqual(3490, costoTotal);
        }

        [TestMethod]
        public void NombrePromocion()
        {
            //Act
            mock!.Setup(x => x.NombrePromocion()).Returns("20% de descuento en el producto de mayor valor");
            string nombre = promocion20!.NombrePromocion();
            //Assert
            Assert.AreEqual("20% de descuento en el producto de mayor valor", nombre);
        }

        [TestMethod]
        public void AplicaPromoOk()
        {
            //Act
            mock!.Setup(x => x.AplicarPromo(It.IsAny<List<Producto>>())).Returns(true);
            bool aplica = promocion20!.AplicarPromo(It.IsAny<List<Producto>>());
            //Assert
            Assert.AreEqual(true, aplica);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AplicarPromocionError()
        {
            //Act
            mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(3490);
            carrito!.Remove(producto2!);
            int costoTotal = promocion20!.AplicarPromocion(carrito!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AplicarPromocionErrorNulo()
        {
            //Act
            mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(3490);
            carrito!.Remove(producto2!);
            carrito!.Add(productoVacio!);
            int costoTotal = promocion20!.AplicarPromocion(carrito!);
        }

<<<<<<< HEAD
        [TestMethod]
        public void AplicaPromoErrorNulo()
        {
            //Act
            mock!.Setup(x => x.AplicarPromo(It.IsAny<List<Producto>>())).Returns(false);
            bool aplica = promocion20!.AplicarPromo(It.IsAny<List<Producto>>());
            //Assert
            Assert.AreEqual(false, aplica);
        }

        [TestMethod]
        public void AplicaPromoError()
        {
            //Act
            mock!.Setup(x => x.AplicarPromo(It.IsAny<List<Producto>>())).Returns(false);
            carrito!.Remove(productoVacio!);
            bool aplica = promocion20!.AplicarPromo(It.IsAny<List<Producto>>());
            //Assert
            Assert.AreEqual(false, aplica);
        }

    }
=======
    [TestMethod]
    public void AplicaPromoError()
    {
        //Act
        mock!.Setup(x => x.AplicarPromo(It.IsAny<List<Producto>>())).Returns(false);
        carrito!.Remove(productoVacio!);
        bool aplica = promocion20!.AplicarPromo(It.IsAny<List<Producto>>());
        //Assert
        Assert.AreEqual(false, aplica);
    }*/
    
>>>>>>> dcc2fb3bad0e5450b648dbc40d426feeac86a582
}
