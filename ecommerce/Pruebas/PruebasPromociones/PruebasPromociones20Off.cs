using System.Collections.Generic;
using Dominio;
using Dominio.Usuario;
using Moq;
using DataAccess;
using Servicios.Interfaces;
using Servicios.Promociones;

namespace Pruebas.PruebasPromociones
{

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
            Marca marca = new();
            Categoria categoria = new();
            List<Color> color = new();
            producto = new Producto("Jean", 2000, "Largo y blanco", marca, categoria, color);
            productoVacio = null;
            Marca marca2 = new();
            Categoria categoria2 = new();
            List<Color> color2 = new();
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
        [ExpectedException(typeof(InvalidOperationException))]
        public void AplicarPromocionError()
        {
            // Act
            mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(3490);
            carrito!.Remove(producto2!);
            int costoTotal = promocion20!.AplicarPromocion(carrito!);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AplicarPromocionErrorNulo()
        {
            // Act
            mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(3490);
            carrito!.Remove(producto2!);
            carrito!.Add(productoVacio!);
            int costoTotal = promocion20!.AplicarPromocion(carrito!);
        }

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

}