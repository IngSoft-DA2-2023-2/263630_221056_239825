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
    public class PruebasPromocion3x2
    {
        private Promocion3x2 _promocion3x2;
        private Producto productoCategoria1;
        private Producto productoCategoria2;
        private Producto productoCategoria3;
        private List<Producto> carrito;

        [TestInitialize]
        public void InitTest()
        {
            _promocion3x2 = new Promocion3x2();
            
            Categoria categoria2 = new();
            
            Marca marca1 = new();
            Marca marca2 = new();
            Marca marca3 = new();
            
            Color color1 = new();
            Color color2 = new();
            Color color3 = new();
            
            productoCategoria1 = new Producto("Remera", 200, "Manga larga", 1, 2, 6, true, 1) { Marca = marca1, Categoria = categoria2, Color = color1};
            productoCategoria2 = new Producto("Remera", 300, "Manga larga", 2, 2, 6, true, 1) { Marca = marca2, Categoria = categoria2, Color = color2};
            productoCategoria3 = new Producto("Remera", 400, "Manga larga", 3, 2, 6, true, 1) { Marca = marca3, Categoria = categoria2, Color = color3};
        }

        [TestMethod]
        public void AplicarPromocion3x2Exitosa()
        {
            //Arrange
            carrito = new List<Producto> {productoCategoria1, productoCategoria2, productoCategoria3};

            //Act
            int costoTotal = _promocion3x2!.AplicarPromocion(carrito);
            
            // Assert
            Assert.AreEqual(700, costoTotal);
        }
        
        [TestMethod]
        public void AplicarPromocion3x2PocosItems()
        {
            //Arrange
            carrito = new List<Producto> {productoCategoria1};

            //Act
            int costoTotal = _promocion3x2!.AplicarPromocion(carrito);
            
            // Assert
            Assert.AreEqual(9999999, costoTotal);
        }

    }
}

