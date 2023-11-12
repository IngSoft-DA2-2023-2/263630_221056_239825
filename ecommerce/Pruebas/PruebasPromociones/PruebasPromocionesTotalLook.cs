using System.Collections.Generic;
using Dominio;
using Dominio.Usuario;
using Moq;
using Servicios;
using Servicios.Interfaces;
using Servicios.Promociones;

namespace Pruebas.PruebasPromociones
{
    [TestClass]
    public class PruebasPromocionesTotalLook
    {
        private PromocionTotalLook _promocionTotalLook;
        private Producto? productoColor1;
        private Producto? productoColor2;
        private Producto? productoColor3;
        private Producto? productoNoAplica;
        private List<Producto> carrito;

        [TestInitialize]
        public void InitTest()
        {
            _promocionTotalLook = new PromocionTotalLook();

            Marca marca1 = new();
            Marca marca2 = new();
            Marca marca3 = new();
            
            Categoria categoria1 = new();
            Categoria categoria2 = new();
            Categoria categoria3 = new();
            
            Color color2 = new();
            
            productoColor1 = new Producto("Blusa", 100, "Manga larga", 1, 1, 6, true, 1) { Marca = marca1, Categoria = categoria1, Color = color2};
            productoColor2 = new Producto("Blusa", 200, "Manga larga", 2, 2, 6, true, 1) { Marca = marca2, Categoria = categoria2, Color = color2};
            productoColor3 = new Producto("Blusa", 300, "Manga larga", 3, 3, 6, true, 1) { Marca = marca3, Categoria = categoria3, Color = color2};
            productoNoAplica = new Producto("Blusa", 400, "Manga larga", 3, 3, 6, false, 1) { Marca = marca3, Categoria = categoria3, Color = color2};
        }

        [TestMethod]
        public void AplicarPromocionTotalLookExitosa()
        {
            //Arrange
            carrito = new List<Producto> {productoColor1, productoColor2, productoColor3};

            //Act
            int costoTotal = _promocionTotalLook.AplicarPromocion(carrito);
            
            // Assert
            Assert.AreEqual(450, costoTotal);
        }
        
        [TestMethod]
        public void AplicarPromocionTotalLookPocosItems()
        {
            //Arrange
            carrito = new List<Producto> {productoColor1};

            //Act
            int costoTotal = _promocionTotalLook.AplicarPromocion(carrito);
            
            // Assert
            Assert.AreEqual(9999999, costoTotal);
        }
        
        [TestMethod]
        public void AplicarPromocionTotalLookNoAplica()
        {
            //Arrange
            carrito = new List<Producto> {productoColor1, productoNoAplica, productoNoAplica};

            //Act
            int costoTotal = _promocionTotalLook.AplicarPromocion(carrito);
            
            // Assert
            Assert.AreEqual(100, costoTotal);
        }
    }
}