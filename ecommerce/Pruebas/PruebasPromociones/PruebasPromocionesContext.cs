using System;
using Dominio;
using Servicios.Promociones;
using Servicios;
using Moq;

namespace Pruebas.PruebasPromociones
{
	[TestClass]
	public class PruebasPromocionesContext
	{
        private PromocionContext? promocionContext;
        private Promocion20Off? promocion20;
        private Producto? producto;
        private Producto? productoVacio;
        private Producto? producto2;
        private List<Producto>? carrito;

        [TestInitialize]
        public void InitTest()
        {
            promocionContext = new PromocionContext();
            promocion20 = new Promocion20Off();
            promocionContext.promocionStrategy = promocion20;
            Marca marca = new();
            Categoria categoria = new();
            List<Color> color = new();
            producto = new Producto("Jean", 2000, "Largo y blanco", 1, 1, color) { Marca = marca, Categoria = categoria };
            productoVacio = null;
            Marca marca2 = new();
            Categoria categoria2 = new();
            List<Color> color2 = new();
            producto2 = new Producto("Blusa", 1890, "Manga larga", 2, 2, color2) { Marca = marca2, Categoria = categoria2 };
            carrito = new List<Producto> { producto, producto2 };
        }


        [TestMethod]
        public void AplicarStrategyOk()
        {
            //Act
            int costoTotal = promocionContext!.AplicarStrategy(carrito!);
            // Assert
            Assert.AreEqual(3490, costoTotal);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AplicarStrategyError()
        {
            carrito!.Remove(producto!);
            int costoTotal = promocionContext!.AplicarStrategy(carrito!);
        }

        [TestMethod]
        public void NombrePromocion()
        {
            Assert.AreEqual(promocionContext!.NombrePromocion(), "Se aplico un 20% de descuento en el producto de mayor valor");
        }

        [TestMethod]
        public void AplicarStrategyPromoOk()
        {
            bool aplica = promocionContext!.AplicarStrategyPromo(carrito!);
            Assert.AreEqual(true, aplica);
        }

        [TestMethod]
        public void AplicarStrategyPromoError()
        {
            carrito!.Remove(producto2!);
            bool aplica = promocionContext!.AplicarStrategyPromo(It.IsAny<List<Producto>>());
            Assert.AreEqual(false, aplica);
        }
    }
}


