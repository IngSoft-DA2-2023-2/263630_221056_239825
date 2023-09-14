using System.Collections.Generic;
using Dominio;
using Dominio.Usuario;
using Moq;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Promociones;
/*
namespace Pruebas.PruebasPromociones
{
    [TestClass]
    public class PruebasPromocionesTotalLook
	{
        private Mock<IPromocionStrategy>? mock;
        private PromocionTotalLook? promocionTotalLook;
        private Producto? producto;
        private Producto? productoVacio;
        private Producto? producto2;
        private Producto? producto3;
        private List<Producto>? carrito;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IPromocionStrategy>();
            promocionTotalLook = new PromocionTotalLook();

            List<string> marcas = new() { "Zara" };
            List<string> categorias = new() { "Abrigos" };
            List<string> colores = new() { "beige" };
            producto = new Producto("Camisa", 1000, "Larga", marcas, categorias, colores);

            productoVacio = null;

            List<string> marcas2 = new() { "Levis" };
            List<string> categorias2 = new() { "Calzados" };
            producto2 = new Producto("Botas", 2000, "Con taco", marcas2, categorias2, colores);

            producto3 = new Producto("Campera", 3500, "Impermeable", marcas, categorias, colores);

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
            mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(4750);
            int costoTotal = promocionTotalLook!.AplicarPromocion(carrito!);
            // Assert
            Assert.AreEqual(4750, costoTotal);
        }

        [TestMethod]
        public void NombrePromocion()
        {
            //Act
            mock!.Setup(x => x.NombrePromocion()).Returns("50% de descuento en el producto de mayor valor");
            string nombre = promocionTotalLook!.NombrePromocion();
            //Assert
            Assert.AreEqual("50% de descuento en el producto de mayor valor", nombre);
        }

        [TestMethod]
        public void AplicaPromoOk()
        {
            //Act
            mock!.Setup(x => x.AplicarPromo(It.IsAny<List<Producto>>())).Returns(true);
            bool aplica = promocionTotalLook!.AplicarPromo(It.IsAny<List<Producto>>());
            //Assert
            Assert.AreEqual(true, aplica);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AplicarPromocionError()
        {
            //Act
            mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(4750);
            carrito!.Remove(producto!);
            List<string> marca = new() { "Zara" };
            List<string> categoria = new() { "Abrigos" };
            List<string> colorNuevo = new() { "Amarillo" };
            Producto? productoDistintaCat = new("Cartera", 5000, "Bandolera", marca, categoria, colorNuevo);
            carrito!.Add(productoDistintaCat);
            int costoTotal = promocionTotalLook!.AplicarPromocion(carrito!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AplicarPromocionErrorNulo()
        {
            //Act
            mock!.Setup(x => x.AplicarPromocion(It.IsAny<List<Producto>>())).Returns(4750);
            carrito!.Remove(producto2!);
            carrito!.Add(productoVacio!);
            int costoTotal = promocionTotalLook!.AplicarPromocion(carrito!);
        }

        [TestMethod]
        public void AplicaPromoErrorNulo()
        {
            //Act
            mock!.Setup(x => x.AplicarPromo(It.IsAny<List<Producto>>())).Returns(false);
            bool aplica = promocionTotalLook!.AplicarPromo(It.IsAny<List<Producto>>());
            //Assert
            Assert.AreEqual(false, aplica);
        }

        [TestMethod]
        public void AplicaPromoError()
        {
            //Act
            mock!.Setup(x => x.AplicarPromo(It.IsAny<List<Producto>>())).Returns(false);
            carrito!.Remove(productoVacio!);
            bool aplica = promocionTotalLook!.AplicarPromo(It.IsAny<List<Producto>>());
            //Assert
            Assert.AreEqual(false, aplica);
        }
    }
}

*/