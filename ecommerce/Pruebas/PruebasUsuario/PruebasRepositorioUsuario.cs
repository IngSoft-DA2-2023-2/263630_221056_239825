//using Moq;
//using DataAccess.Interfaces;
//using DataAccess;
//using Dominio.Usuario;
//using Dominio;
//using System.Linq.Expressions;

//namespace Pruebas.PruebasUsuario
//{
//    [TestClass]
//    public class PruebasRepositorioUsuario
//    {
//        private Mock<IRepositorioUsuario>? mock;
//        private RepositorioUsuario? repositorioUsuario;
//        private Cliente? cliente;
//        private List<Usuario>? listaClientes;
//        private Cliente? clienteSinDireccion;
//        private Cliente? clienteSinMail;
//        private Cliente? clienteNulo;
//        private Compra? compra;
//        [TestInitialize]
//        public void InitTest()
//        {
//            mock = new Mock<IRepositorioUsuario>();
//            repositorioUsuario = new RepositorioUsuario(mock.Object);
//            cliente = new Cliente("martin@edelman.com.uy", "Zorrilla 124");
//            cliente.Id = 1;
//            listaClientes = new List<Usuario>();
//            listaClientes.Add(cliente);
//            clienteSinDireccion = new Cliente("Martín@Edelman","");
//            clienteSinMail = new Cliente("Martin Edelman", "Zorrilla 124");
//            clienteNulo = null;
//            compra = new Compra();
//        }

//        [TestMethod]
//        public void RegistrarUsuarioOk()
//        {
//            // Act
//            mock!.Setup(x => x.AgregarUsuario(It.IsAny<Usuario>())).Returns(cliente!);
//            var resultado = repositorioUsuario!.AgregarUsuario(cliente!);

//            // Assert
//            Assert.AreEqual(cliente, resultado);
//        }

//        [TestMethod]
//        public void ObtenerUsuarioOk()
//        {
//            // Act
//            mock!.Setup(x => x.AgregarUsuario(It.IsAny<Usuario>())).Returns(cliente!);
//            mock!.Setup(x => x.ObtenerUsuario(It.IsAny<int>())).Returns(cliente!);
//            repositorioUsuario!.AgregarUsuario(cliente!);
//            var resultado = repositorioUsuario!.ObtenerUsuario(1);

//            // Assert
//            Assert.AreEqual(cliente, resultado);
//        }
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public void ObtenerUsuarioNoExiste()
//        {
//            mock!.Setup(x => x.AgregarUsuario(It.IsAny<Usuario>())).Returns(cliente!);
//            mock!.Setup(x => x.ObtenerUsuario(It.IsAny<int>())).Returns(cliente!);
//            repositorioUsuario!.AgregarUsuario(cliente!);
//            var resultado = repositorioUsuario!.ObtenerUsuario(2);
//        }
//        [TestMethod]
//        public void ObtenerUsuariosOk()
//        {
//            mock!.Setup(x => x.AgregarUsuario(It.IsAny<Usuario>())).Returns(cliente!);
//            mock!.Setup(x => x.ObtenerUsuarios()).Returns(listaClientes!);
//            repositorioUsuario!.AgregarUsuario(cliente!);
//            var resultado = repositorioUsuario!.ObtenerUsuarios();

//            // Assert
//            Assert.AreEqual(listaClientes!.Count, resultado.Count);
//            Assert.AreEqual(listaClientes[0], resultado[0]);
//        }

//        [TestMethod]
//        public void ActualizarUsuariosOk()
//        {
//            // Act
//            mock!.Setup(x => x.AgregarUsuario(It.IsAny<Usuario>())).Returns(cliente!);
//            mock!.Setup(x => x.ObtenerUsuario(It.IsAny<int>())).Returns(cliente!);
//            mock!.Setup(x => x.ActualizarUsuario(It.IsAny<int>(), It.IsAny<string>()));
//            repositorioUsuario!.AgregarUsuario(cliente!);
//            repositorioUsuario!.ActualizarUsuario(1, "Julio Cesar 1247");
//            cliente!.DireccionEntrega = "Julio Cesar 1247";
//            var resultado = repositorioUsuario!.ObtenerUsuario(1);

//            // Assert
//            Assert.AreEqual(cliente.DireccionEntrega, resultado.DireccionEntrega);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public void ActualizarUsuarioNoExiste()
//        {
//            mock!.Setup(x => x.AgregarUsuario(It.IsAny<Usuario>())).Returns(cliente!);
//            mock!.Setup(x => x.ObtenerUsuario(It.IsAny<int>())).Returns(cliente!);
//            mock!.Setup(x => x.ActualizarUsuario(It.IsAny<int>(), It.IsAny<string>()));
//            repositorioUsuario!.AgregarUsuario(cliente!);
//            repositorioUsuario!.ActualizarUsuario(2, "Julio Cesar 1247");
//        }

//        [TestMethod]
//        public void AgregarCompraAlUsuarioOk()
//        {
//            mock!.Setup(x => x.AgregarUsuario(It.IsAny<Usuario>())).Returns(cliente!);
//            mock!.Setup(x => x.AgregarCompraAlUsuario(It.IsAny<int>(), It.IsAny<Compra>()));
//            repositorioUsuario!.AgregarUsuario(cliente!);
//            repositorioUsuario!.AgregarCompraAlUsuario(1, compra!);
//            cliente!.Compras.Add(compra!);
//            var resultado = repositorioUsuario!.ObtenerUsuario(1);

//            // Assert
//            Assert.AreEqual(cliente.Compras, resultado.Compras);
//        }
//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public void AgregarCompraAlUsuarioNoExiste()
//        {
//            mock!.Setup(x => x.AgregarUsuario(It.IsAny<Usuario>())).Returns(cliente!);
//            mock!.Setup(x => x.AgregarCompraAlUsuario(It.IsAny<int>(), It.IsAny<Compra>()));
//            repositorioUsuario!.AgregarUsuario(cliente!);
//            repositorioUsuario!.AgregarCompraAlUsuario(2, compra!);
//        }

//        [TestMethod]
//        public void ObtenerComprasDelUsuarioOk()
//        {
//            mock!.Setup(x => x.AgregarUsuario(It.IsAny<Usuario>())).Returns(cliente!);
//            mock!.Setup(x => x.AgregarCompraAlUsuario(It.IsAny<int>(), It.IsAny<Compra>()));
//            mock!.Setup(x => x.ObtenerComprasDelUsuario(It.IsAny<int>()));
//            repositorioUsuario!.AgregarUsuario(cliente!);
//            repositorioUsuario!.AgregarCompraAlUsuario(1, compra!);
//            var resultado = repositorioUsuario!.ObtenerComprasDelUsuario(1);
//            cliente!.Compras.Add(compra!);

//            // Assert
//            Assert.AreEqual(cliente!.Compras, resultado);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public void ObtenerComprasDelUsuarioNoExiste()
//        {
//            mock!.Setup(x => x.AgregarUsuario(It.IsAny<Usuario>())).Returns(cliente!);
//            mock!.Setup(x => x.AgregarCompraAlUsuario(It.IsAny<int>(), It.IsAny<Compra>()));
//            mock!.Setup(x => x.ObtenerComprasDelUsuario(It.IsAny<int>()));
//            repositorioUsuario!.AgregarUsuario(cliente!);
//            repositorioUsuario!.AgregarCompraAlUsuario(1, compra!);
//            var resultado = repositorioUsuario!.ObtenerComprasDelUsuario(2);
//        }

//        [TestMethod]
//        public void EliminarUsuarioCorrecto()
//        {
//            mock!.Setup(x => x.AgregarUsuario(cliente!)).Returns(cliente!);
//            mock!.Setup(x => x.EliminarUsuario(cliente!));
//            repositorioUsuario!.AgregarUsuario(cliente!);
//            repositorioUsuario!.EliminarUsuario(cliente!);
//            List<Usuario> usuarios = repositorioUsuario!.ObtenerUsuarios();

//            Assert.IsTrue(usuarios.Count() == 0);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public void EliminarUsuarioIncorrecto()
//        {
//            Cliente nuevoCliente = new Cliente("martin@edelman.com.uy", "Zorrilla 142");
//            mock!.Setup(x => x.EliminarUsuario(nuevoCliente!));
//            repositorioUsuario!.EliminarUsuario(nuevoCliente!);
//        }
//    }
//}
