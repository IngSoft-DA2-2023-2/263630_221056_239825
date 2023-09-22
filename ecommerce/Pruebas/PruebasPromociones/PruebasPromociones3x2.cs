using System.Collections.Generic;
using Dominio;
using Dominio.Usuario;
using Moq;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Promociones;

namespace Pruebas.PruebasPromociones;
    [TestClass]
    public class PruebasPromociones3x2
	{
        private Mock<IPromocionStrategy>? mock;
        private Promocion3x2? promocion3x2;
        private Producto? producto;
        private Producto? productoVacio;
        private Producto? producto2;
        private Producto? producto3;
        private List<Producto>? carrito;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPromocionStrategy>();
            promocion3x2 = new Promocion3x2();

            Marca marca = new Marca();
            Categoria categoria = new();
            List<Color> colores = new();
            producto = new Producto("Camisa", 1000, "Larga", marca, categoria, colores);

            productoVacio = null;

            Marca marca2 = new Marca();
            List<Color> color2 = new() ;
            producto2 = new Producto("Buzo", 800, "Bordado", marca2, categoria, color2);

            List<Color> color3 = new();
            producto3 = new Producto("Campera", 3500, "Impermeable", marca, categoria, color3);

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
            mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(4500);
            int costoTotal = promocion3x2!.AplicarPromocion(carrito!);
            // Assert
            Assert.AreEqual(4500, costoTotal);
        }

        [TestMethod]
        public void NombrePromocion()
        {
            //Act
            mock!.Setup(x => x.NombrePromocion()).Returns("El producto de menor valor va de regalo");
            string nombre = promocion3x2!.NombrePromocion();
            //Assert
            Assert.AreEqual("El producto de menor valor va de regalo", nombre);
        }

        [TestMethod]
        public void AplicaPromoOk()
        {
            //Act
            mock!.Setup(x => x.AplicarPromo(It.IsAny<List<Producto>>())).Returns(true);
            bool aplica = promocion3x2!.AplicarPromo(It.IsAny<List<Producto>>());
            //Assert
            Assert.AreEqual(true, aplica);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AplicarPromocionError()
        {
            //Act
            mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(4500);
            carrito!.Remove(producto!);
            Marca marca = new();
            Categoria categoriaNueva = new();
            List<Color> colores = new();
            Producto? productoDistintaCat = new("Cartera", 5000, "Bandolera", marca, categoriaNueva, colores);
            carrito!.Add(productoDistintaCat);
            int costoTotal = promocion3x2!.AplicarPromocion(carrito!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AplicarPromocionErrorNulo()
        {
            //Act
            mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(4500);
            carrito!.Remove(producto2!);
            carrito!.Add(productoVacio!);
            int costoTotal = promocion3x2!.AplicarPromocion(carrito!);
        }

        [TestMethod]
        public void AplicaPromoErrorNulo()
        {
            //Act
            mock!.Setup(x => x.AplicarPromo(It.IsAny<List<Producto>>())).Returns(false);
            bool aplica = promocion3x2!.AplicarPromo(It.IsAny<List<Producto>>());
            //Assert
            Assert.AreEqual(false, aplica);
        }

        [TestMethod]
        public void AplicaPromoError()
        {
            //Act
            mock!.Setup(x => x.AplicarPromo(It.IsAny<List<Producto>>())).Returns(false);
            carrito!.Remove(productoVacio!);
            bool aplica = promocion3x2!.AplicarPromo(It.IsAny<List<Producto>>());
            //Assert
            Assert.AreEqual(false, aplica);
        }
    }


