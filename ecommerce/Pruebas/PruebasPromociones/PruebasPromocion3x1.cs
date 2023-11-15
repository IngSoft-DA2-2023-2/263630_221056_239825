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
    public class PruebasPromocion3x1
    {
        private Promocion3x1Prueba _promocion3x1;
        private Producto productoMarca1;
        private Producto productoMarca2;
        private Producto productoMarca3;
        private List<Producto> carrito;

        [TestInitialize]
        public void InitTest()
        {
            _promocion3x1 = new Promocion3x1Prueba();

            Marca marca1 = new();
            
            Categoria categoria1 = new();
            Categoria categoria2 = new();
            Categoria categoria3 = new();
            
            Color color1 = new();
            Color color2 = new();
            Color color3 = new();
            
            productoMarca1 = new Producto("Jean1", 200, "Largo y blanco", 1, 1, 3, true, 1) { Marca = marca1, Categoria = categoria1, Color = color1};
            productoMarca2 = new Producto("Jean2", 800, "Largo y blanco", 1, 2, 4, true, 2) { Marca = marca1, Categoria = categoria2, Color = color2};
            productoMarca3 = new Producto("Jean3", 500, "Largo y blanco", 1, 3, 2, true, 3) { Marca = marca1, Categoria = categoria3, Color = color3};
        }

        [TestMethod]
        public void AplicarPromocion3x1Exitosa()
        {
            //Arrange
            carrito = new List<Producto> {productoMarca1, productoMarca2, productoMarca3};

            //Act
            int costoTotal = _promocion3x1.AplicarPromocion(carrito);
            
            // Assert
            Assert.AreEqual(800, costoTotal);
        }
        
        [TestMethod]
        public void AplicarPromocion3x1PocosItems()
        {
            //Arrange
            carrito = new List<Producto> {productoMarca3};

            //Act
            int costoTotal = _promocion3x1!.AplicarPromocion(carrito);
            
            // Assert
            Assert.AreEqual(9999999, costoTotal);
        }
        
    }
}

