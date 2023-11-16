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
        private Promocion20OffPrueba _promocion20Off;
        private Producto productoColor1;
        private Producto productoCategoria1;
        private List<Producto> carrito;

        [TestInitialize]
        public void InitTest()
        {
            _promocion20Off = new Promocion20OffPrueba();

            Marca marca1 = new();
            
            Categoria categoria1 = new();
            Categoria categoria2 = new();
            
            Color color1 = new();
            Color color2 = new();
            
            productoColor1 = new Producto("Blusa", 100, "Manga larga", 1, 1, 6, true, 1) { Marca = marca1, Categoria = categoria1, Color = color2};

            productoCategoria1 = new Producto("Remera", 200, "Manga larga", 1, 2, 6, true, 1) { Marca = marca1, Categoria = categoria2, Color = color1};
        }

        [TestMethod]
        public void AplicarPromocion20OffOk()
        {
            //Arrange
            carrito = new List<Producto> {productoCategoria1, productoColor1};

            //Act
            int costoTotal = _promocion20Off.AplicarPromocion(carrito);
            
            // Assert
            Assert.AreEqual(260, costoTotal);
        }
        
        [TestMethod]
        public void AplicarPromocionPocos20OffItems()
        {
            //Arrange
            carrito = new List<Producto> {productoColor1};

            //Act
            int costoTotal = _promocion20Off.AplicarPromocion(carrito);
            
            // Assert
            Assert.AreEqual(9999999, costoTotal);
        }

    }
}

