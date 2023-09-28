﻿using System.Collections.Generic;
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
    private IPromocionStrategy? promocion3x2;
    private Producto? producto1;
    private Producto? productoVacio;
    private Producto? producto2;
    private Producto? producto3;
    private List<Producto>? carrito;

    [TestInitialize]
    public void InitTest()
    {
        promocion3x2 = new Promocion3x2();

        Marca marca = new Marca();
        Categoria categoria = new Categoria()
        {
            Id = 0,
            Nombre = "Nike"
        };
        List<Color> colores = new();
        producto1 = new Producto("Camisa", 1000, "Larga", marca, categoria, colores);

        productoVacio = null;

        Marca marca2 = new Marca();
        List<Color> color2 = new();
        producto2 = new Producto("Buzo", 800, "Bordado", marca2, categoria, color2);

        List<Color> color3 = new();
        producto3 = new Producto("Campera", 3500, "Impermeable", marca, categoria, color3);

        carrito = new List<Producto>
        {
            producto1,
            producto2,
            producto3
        };
    }

    [TestMethod]
    public void AplicarPromocionOk()
    {
        int costoTotal = promocion3x2!.AplicarPromocion(carrito!);
        Assert.AreEqual(2900, costoTotal);
    }

    [TestMethod]
    public void AplicaPromoOk()
    {
        List<Producto> carritoConPromo = new(){ producto1!, producto2!, producto3!};
        bool aplica = promocion3x2!.AplicarPromo(carritoConPromo);
        Assert.AreEqual(true, aplica);
    }

    [TestMethod]
    //[ExpectedException(typeof(ArgumentException))]
    public void AplicarPromocionError()
    {
        carrito!.Remove(producto1!);
        Marca marca = new();
        Categoria categoriaNueva = new();
        List<Color> colores = new();
        Producto? productoDistintaCat = new("Cartera", 5000, "Bandolera", marca, categoriaNueva, colores);
        carrito!.Add(productoDistintaCat);
        int costoTotal = promocion3x2!.AplicarPromocion(carrito!);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public void AplicarPromocionErrorNulo()
    {
        carrito!.Remove(producto2!);
        carrito!.Add(productoVacio!);
        int costoTotal = promocion3x2!.AplicarPromocion(carrito!);
    }

    [TestMethod]
    public void AplicaPromoErrorNulo()
    {
        bool aplica = promocion3x2!.AplicarPromo(It.IsAny<List<Producto>>());
        Assert.AreEqual(false, aplica);
    }

    [TestMethod]
    public void AplicaPromoError()
    {
        carrito!.Remove(productoVacio!);
        bool aplica = promocion3x2!.AplicarPromo(It.IsAny<List<Producto>>());
        Assert.AreEqual(false, aplica);
    }
}


