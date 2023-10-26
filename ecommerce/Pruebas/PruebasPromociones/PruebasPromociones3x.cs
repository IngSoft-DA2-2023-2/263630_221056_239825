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
    public class PruebasPromociones3x
    {
        private Promocion3xModelable _promocion3xModelable;
        private Producto productoMarca1;
        private Producto productoMarca2;
        private Producto productoMarca3;
        private Producto productoCategoria1;
        private Producto productoCategoria2;
        private Producto productoCategoria3;
        private List<Producto> carrito;

        [TestInitialize]
        public void InitTest()
        {
            _promocion3xModelable = new Promocion3xModelable();

            Marca marca1 = new();
            Marca marca2 = new();
            Marca marca3 = new();
            
            Categoria categoria1 = new();
            Categoria categoria2 = new();
            Categoria categoria3 = new();
            
            Color color1 = new();
            Color color2 = new();
            Color color3 = new();
            
            productoMarca1 = new Producto("Jean1", 200, "Largo y blanco", 1, 1, 3, true, 1) { Marca = marca1, Categoria = categoria1, Color = color1};
            productoMarca2 = new Producto("Jean2", 800, "Largo y blanco", 1, 2, 4, true, 2) { Marca = marca1, Categoria = categoria2, Color = color2};
            productoMarca3 = new Producto("Jean3", 500, "Largo y blanco", 1, 3, 2, true, 3) { Marca = marca1, Categoria = categoria3, Color = color3};

            productoCategoria1 = new Producto("Remera", 200, "Manga larga", 1, 2, 6, true, 1) { Marca = marca1, Categoria = categoria2, Color = color1};
            productoCategoria2 = new Producto("Remera", 300, "Manga larga", 2, 2, 6, true, 1) { Marca = marca2, Categoria = categoria2, Color = color2};
            productoCategoria3 = new Producto("Remera", 400, "Manga larga", 3, 2, 6, true, 1) { Marca = marca3, Categoria = categoria2, Color = color3};
        }

        [TestMethod]
        public void AplicarPromocion3x1Exitosa()
        {
            //Arrange
            carrito = new List<Producto> {productoMarca1, productoMarca2, productoMarca3};

            //Act
            int costoTotal = _promocion3xModelable.AplicarPromocion(1, carrito);
            
            // Assert
            Assert.AreEqual(800, costoTotal);
        }
        
        [TestMethod]
        public void AplicarPromocion3x1PocosItems()
        {
            //Arrange
            carrito = new List<Producto> {productoMarca3};

            //Act
            int costoTotal = _promocion3xModelable!.AplicarPromocion(1, carrito);
            
            // Assert
            Assert.AreEqual(9999999, costoTotal);
        }
        
        [TestMethod]
        public void AplicarPromocion3x2Exitosa()
        {
            //Arrange
            carrito = new List<Producto> {productoCategoria1, productoCategoria2, productoCategoria3};

            //Act
            int costoTotal = _promocion3xModelable!.AplicarPromocion(2, carrito);
            
            // Assert
            Assert.AreEqual(700, costoTotal);
        }
        
        [TestMethod]
        public void AplicarPromocion3x2PocosItems()
        {
            //Arrange
            carrito = new List<Producto> {productoCategoria1};

            //Act
            int costoTotal = _promocion3xModelable!.AplicarPromocion(2, carrito);
            
            // Assert
            Assert.AreEqual(9999999, costoTotal);
        }

    }
}

