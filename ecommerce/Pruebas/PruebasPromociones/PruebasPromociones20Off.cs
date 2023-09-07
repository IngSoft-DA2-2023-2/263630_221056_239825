using System.Collections.Generic;
using Dominio;
using Dominio.Usuario;
using Moq;
using Repositorio;
using Repositorio.Interfaces;
using Repositorio.Promociones;

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
            List<string> marcas = new List<string> { "Zara" };
            List<string> categorias = new List<string> { "pantalones" };
            List<string> colores = new List<string> { "blanco" };
            producto = new Producto("Jean", 2000, "Largo y blanco", marcas, categorias, colores);
            productoVacio = null;
            List<string> marcas2 = new List<string> { "Levis" };
            List<string> categorias2 = new List<string> { "Blusas" };
            List<string> colores2 = new List<string> { "Lila" };
            producto2 = new Producto("Blusa", 1890, "Manga larga", marcas2, categorias2, colores2);
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
