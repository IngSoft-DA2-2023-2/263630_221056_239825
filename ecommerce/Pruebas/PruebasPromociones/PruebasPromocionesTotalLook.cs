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
    public class PruebasPromocionesTotalLook
    {
        private IPromocionStrategy? promocionTotalLook;
        private Producto? producto0;
        private Producto? producto;
        private Producto? productoVacio;
        private Producto? producto2;
        private Producto? producto3;
        private List<Producto>? carrito;

        [TestInitialize]
        public void InitTest()
        {
            promocionTotalLook = new PromocionTotalLook();
            Marca marca = new();
            Categoria categoria = new();
            Color rojo = new Color()
            {
                Id = 0,
                Nombre = "Rojo"
            };
            Color azul = new Color()
            {
                Id = 1,
                Nombre = "Azul"
            };
            Color verde = new Color()
            {
                Id = 2,
                Nombre = "Verde"
            };
            List<Color> coloresProducto1 = new()
            {
                rojo
            };
            List<Color> coloresProducto2 = new()
            {
                azul, rojo
            };
            List<Color> coloresProducto3 = new()
            {
                azul, rojo, verde
            };
            List<Color> coloresProducto0 = new List<Color>()
            {
                verde
            };
            producto0 = new Producto("aaa", 22, "vvv", marca, categoria, coloresProducto0);
            producto = new Producto("Camisa", 1000, "Larga", marca, categoria, coloresProducto1);
            Marca marca2 = new();
            Categoria categoria2 = new();
            producto2 = new Producto("Botas", 2000, "Con taco", marca2, categoria2, coloresProducto2);
            producto3 = new Producto("Campera", 3500, "Impermeable", marca, categoria, coloresProducto3);
            productoVacio = null;
            carrito = new List<Producto> { producto, producto2, producto3 };
        }

        [TestMethod]
        public void AplicarPromocionOk()
        {
            //Act
            int costoTotal = promocionTotalLook!.AplicarPromocion(carrito!);
            // Assert
            Assert.AreEqual(4750, costoTotal);
        }

        [TestMethod]
        public void AplicaPromoOk()
        {
            //Act
            bool aplica = promocionTotalLook!.AplicarPromo(carrito!);
            //Assert
            Assert.AreEqual(true, aplica);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void AplicarPromocionError()
        {
            //Act
            carrito!.Remove(producto2!);
            Marca marca = new();
            Categoria categoria = new();
            List<Color> colorNuevo = new();
            Producto? productoDistintaCat = new("Cartera", 5000, "Bandolera", marca, categoria, colorNuevo);
            carrito!.Add(productoDistintaCat);
            int costoTotal = promocionTotalLook!.AplicarPromocion(carrito!);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AplicarPromocionErrorNulo()
        {
            //Act
            carrito!.Remove(producto2!);
            carrito!.Add(productoVacio!);
            int costoTotal = promocionTotalLook!.AplicarPromocion(carrito!);
        }

        [TestMethod]
        public void AplicaPromoErrorNulo()
        {
            //Act
            carrito!.Remove(producto2!);
            carrito.Remove(productoVacio!);
            bool aplica = promocionTotalLook!.AplicarPromo(carrito);
        }

        [TestMethod]
        public void AplicaPromoError()
        {
            //Act
            carrito!.Remove(producto2!);
            bool aplica = promocionTotalLook!.AplicarPromo(carrito);
            //Assert
            Assert.AreEqual(false, aplica);
        }
    }
}

