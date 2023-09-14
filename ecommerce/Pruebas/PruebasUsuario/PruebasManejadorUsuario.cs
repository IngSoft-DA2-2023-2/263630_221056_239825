using Moq;
using Servicios;
using Dominio.Usuario;
using Dominio;
using DataAccess.Interfaces;
using DataAccess;

namespace Pruebas.PruebasUsuario
{
    [TestClass]
    public class PruebasManejadorUsuario
    {
        private Mock<IRepositorioUsuario>? mock;
        private ManejadorUsuario? manejadorUsuario;
        private Administrador? cliente;
        private List<Usuario>? listaClientes;
        private Cliente? clienteSinDireccion;
        private Cliente? clienteSinMail;
        private Cliente? clienteNulo;
        private Compra? compra;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IRepositorioUsuario>(MockBehavior.Strict);
            manejadorUsuario = new ManejadorUsuario(mock.Object);
            cliente = new Administrador("martin@edelman.com.uy", "Zorrilla 124");
            cliente.Id = 1;
            listaClientes = new List<Usuario>();
            listaClientes.Add(cliente);
            clienteSinDireccion = new Cliente("Martín@Edelman", "");
            clienteSinMail = new Cliente("Martin Edelman", "Zorrilla 124");
            clienteNulo = null;
            compra = new Compra();
            compra.Id = 1;
            List<Color> colorList = new List<Color>();
            colorList.Add(new Color());
            compra.Productos.Add(new Producto("prod", 123, "", new Marca(), new Categoria(), colorList));
        }

        [TestMethod]
        public void RegistrarUsuarioOk()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(cliente!)).Returns(cliente!);
            var resultado = manejadorUsuario!.RegistrarUsuario(cliente!);

            //Assert
            Assert.AreEqual(cliente, resultado);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegistrarUsuarioIncorrectoSinDireccion()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(clienteSinDireccion!));
            manejadorUsuario!.RegistrarUsuario(clienteSinDireccion!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegistrarUsuarioIncorrectoSinMail()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(clienteSinMail!));
            manejadorUsuario!.RegistrarUsuario(clienteSinMail!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegistrarUsuarioNulo()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(clienteNulo!));
            manejadorUsuario!.RegistrarUsuario(clienteNulo!);
        }

        [TestMethod]
        public void ObtenerUsuarioOk()
        {
            // Act
            mock!.Setup(x => x.ObtenerUsuario(1)).Returns(cliente!);
            var resultado = manejadorUsuario!.ObtenerUsuario(1);

            //Assert
            Assert.AreEqual(cliente, resultado);
        }
        
        [TestMethod]
        public void ObtenerUsuariosOk()
        {
            mock!.Setup(x => x.AgregarUsuario(cliente!)).Returns(cliente!);
            mock!.Setup(x => x.ObtenerUsuarios()).Returns(listaClientes!);
            manejadorUsuario!.RegistrarUsuario(cliente!);
            var resultado = manejadorUsuario!.ObtenerUsuarios();

            // Assert
            Assert.AreEqual(listaClientes!.Count, resultado.Count);
            Assert.AreEqual(listaClientes[0], resultado[0]);
        }
        
        [TestMethod]
        public void ActualizarUsuariosOk()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(cliente!)).Returns(cliente!);
            mock!.Setup(x => x.ObtenerUsuario(1)).Returns(cliente!);
            mock!.Setup(x => x.ActualizarUsuario(1, "Julio Cesar 1247"));
            manejadorUsuario!.RegistrarUsuario(cliente!);
            manejadorUsuario!.ActualizarUsuario(1, "Julio Cesar 1247");
            cliente!.DireccionEntrega = "Julio Cesar 1247";
            var resultado = manejadorUsuario!.ObtenerUsuario(1);

            // Assert
            Assert.AreEqual(cliente.DireccionEntrega, resultado.DireccionEntrega);
        }

        [TestMethod]
        public void AgregarCompraAlUsuarioOk()
        {
            mock!.Setup(x => x.AgregarCompraAlUsuario(1, compra!));
            mock!.Setup(x => x.ObtenerUsuario(1)).Returns(cliente!);
            manejadorUsuario!.AgregarCompraAlUsuario(1, compra!);
            cliente!.Compras.Add(compra!);
            var resultado = manejadorUsuario!.ObtenerUsuario(1);

            // Assert
            Assert.AreEqual(cliente.Compras, resultado.Compras);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AgregarCompraAlUsuarioSinProductos()
        {
            Compra compraVacia = new Compra();
            mock!.Setup(x => x.AgregarCompraAlUsuario(1, compraVacia!));
            manejadorUsuario!.AgregarCompraAlUsuario(1, compraVacia!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarCompraAlUsuarioNula()
        {
            Compra? compraNula = null;
            mock!.Setup(x => x.AgregarCompraAlUsuario(1, compraNula!));
            manejadorUsuario!.AgregarCompraAlUsuario(1, compraNula!);
        }

        [TestMethod]
        public void ObtenerComprasDelUsuarioOk()
        {
            cliente!.Compras.Add(compra!);
            mock!.Setup(x => x.AgregarCompraAlUsuario(1, compra!));
            mock!.Setup(x => x.ObtenerComprasDelUsuario(1)).Returns(cliente!.Compras);
            manejadorUsuario!.AgregarCompraAlUsuario(1, compra!);
            var resultado = manejadorUsuario!.ObtenerComprasDelUsuario(1);

            // Assert
            Assert.AreEqual(cliente!.Compras, resultado);
        }

        [TestMethod]
        public void EliminarUsuario()
        {
            mock!.Setup(x => x.EliminarUsuario(cliente!));
            manejadorUsuario!.EliminarUsuario(cliente!);

            mock!.VerifyAll();
        }
    }
}