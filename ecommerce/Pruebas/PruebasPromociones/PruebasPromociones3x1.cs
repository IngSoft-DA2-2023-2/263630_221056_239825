using System.Collections.Generic;
using Dominio;
using Dominio.Usuario;
using Moq;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Promociones;

namespace Pruebas.PruebasPromociones
{
    [TestClass]
    public class PruebasPromociones3x1
    {
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

            Marca marca = new();
            Categoria categoria = new();
            List<Color> colores = new();
            producto = new Producto("Jean", 2000, "Largo y blanco", marca, categoria, colores);

            productoVacio = null;

            Categoria categoria2 = new();
            List<Color> colores2 = new();
            producto2 = new Producto("Blusa", 800, "Manga larga", marca, categoria2, colores2);

            Categoria categoria3 = new();
            List<Color> colores3 = new();
            producto3 = new Producto("Campera", 3500, "Impermeable", marca, categoria3, colores3);

            carrito = new List<Producto> { producto, producto2, producto3 };
        }

        [TestMethod]
        public void AplicarPromocionOk()
        {
            //Act
            int costoTotal = promocion3x1!.AplicarPromocion(carrito!);
            // Assert
            Assert.AreEqual(3500, costoTotal);
        }

        [TestMethod]
        public void AplicaPromoOk()
        {
            List<Producto> carrito = new List<Producto> {producto!, producto2!,producto3! };
            // Act
            bool aplica = promocion3x1!.AplicarPromo(carrito);
            // Assert
            Assert.IsTrue(aplica);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void AplicarPromocionError()
        {
            //Act
            //mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(3500);
            carrito!.Remove(producto!);
            Marca marcaNueva = new();
            Categoria categoria4 = new();
            List<Color> colores = new();
            Producto? productoDistintaMarca = new Producto("Cartera", 5000, "Bandolera", marcaNueva, categoria4, colores);
            carrito!.Add(productoDistintaMarca);
            int costoTotal = promocion3x1!.AplicarPromocion(carrito!);

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
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
        }
    }
}

