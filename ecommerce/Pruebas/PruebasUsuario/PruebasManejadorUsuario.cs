using Moq;
using Servicios;
using Dominio.Usuario;
using Dominio;
using DataAccess.Interfaces;
using DataAccess;
using System.Security.Cryptography;
using System.Text;

namespace Pruebas.PruebasUsuario
{
    [TestClass]
    public class PruebasManejadorUsuario
    {
        private Mock<IRepositorioUsuario>? mock;
        private ManejadorUsuario? manejadorUsuario;
        private Usuario? cliente;
        private Usuario? clienteHasheado;
        private List<Usuario>? listaClientes;
        private Usuario? clienteSinDireccion;
        private Usuario? clienteSinMail;
        private Usuario? clienteNulo;
        private Compra? compra;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IRepositorioUsuario>(MockBehavior.Strict);
            manejadorUsuario = new ManejadorUsuario(mock.Object);
            cliente = new Usuario("martin@edelman.com.uy", "Zorrilla 124", "Password1");
            cliente.Id = 1;
            clienteHasheado = cliente;
            clienteHasheado.Contrasena = HashPasword(cliente.Contrasena, Salting(cliente.CorreoElectronico));
            listaClientes = new List<Usuario>();
            listaClientes.Add(cliente);
            clienteSinDireccion = new Usuario("Martin@Edelman", "", "Password1");
            clienteSinMail = new Usuario("Martin Edelman", "Zorrilla 124", "Password1");
            clienteNulo = null;
            compra = new Compra();
            compra.Id = 1;
            List<Color> colorList = new List<Color>();
            colorList.Add(new Color());
            compra.Productos.Add(new Producto("prod", 123, "", 1, 1, colorList));
        }

        private byte[] Salting(string correoElectronico)
        {
            return new byte[correoElectronico.Length];
        }

        private string HashPasword(string password, byte[] salt)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                350000,
                HashAlgorithmName.SHA512,
                64);
            return Convert.ToHexString(hash);
        }

        [TestMethod]
        public void RegistrarUsuarioOk()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(cliente!)).Returns(cliente!);
            Usuario resultado = manejadorUsuario!.RegistrarUsuario(cliente!);

            //Assert
            Assert.AreEqual(cliente, resultado);
        }

        [TestMethod]
        public void IniciarSesionOK()
        {
            mock!.Setup(x => x.ObtenerUsuario(u => u.CorreoElectronico == "martin@edelman.com.uy")).Returns(clienteHasheado!);
            Usuario usuarioLoggeado = manejadorUsuario!.Login("martin@edelman.com.uy", "Password1");

            Assert.AreEqual(cliente, usuarioLoggeado);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void IniciarSesionError()
        {
            mock!.Setup(x => x.ObtenerUsuario(u => u.CorreoElectronico == "martin@edelman.com.uy")).Returns(cliente!);
            Usuario usuarioLoggeado = manejadorUsuario!.Login("martin@edelman.com.uy", "Password2");
            Console.WriteLine(clienteHasheado!.Contrasena + " - " + usuarioLoggeado.Contrasena);
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
        [ExpectedException(typeof(ArgumentException))]
        public void RegistrarUsuarioIncorrectoContrasena()
        {
            clienteSinMail!.CorreoElectronico = "tati@gmail.com";
            clienteSinMail!.Contrasena = "abc";
            // Act
            mock!.Setup(x => x.AgregarUsuario(clienteSinMail!));
            manejadorUsuario!.RegistrarUsuario(clienteSinMail!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RegistrarUsuarioIncorrectoRepetido()
        {
            // Act
            mock!.Setup(x => x.ObtenerUsuarios()).Returns(listaClientes!);

            mock!.Setup(x => x.AgregarUsuario(cliente!));
            manejadorUsuario!.RegistrarUsuario(cliente!);
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
            mock!.Setup(x => x.ObtenerUsuario(u => u.Id == 1)).Returns(cliente!);
            Usuario resultado = manejadorUsuario!.ObtenerUsuario(1);

            //Assert
            Assert.AreEqual(cliente, resultado);
        }

        [TestMethod]
        public void ObtenerUsuariosOk()
        {
            mock!.Setup(x => x.AgregarUsuario(cliente!)).Returns(cliente!);
            mock!.Setup(x => x.ObtenerUsuarios()).Returns(listaClientes!);
            List<Usuario> resultado = manejadorUsuario!.ObtenerUsuarios();

            // Assert
            Assert.AreEqual(listaClientes!.Count, resultado.Count);
            Assert.AreEqual(listaClientes[0], resultado[0]);
        }

        [TestMethod]
        public void ActualizarUsuariosOk()
        {
            // Act
            mock!.Setup(x => x.AgregarUsuario(cliente!)).Returns(cliente!);
            mock!.Setup(x => x.ObtenerUsuario(u => u.Id == 1)).Returns(cliente!);
            mock!.Setup(x => x.ActualizarUsuario(cliente!));
            manejadorUsuario!.RegistrarUsuario(cliente!);
            manejadorUsuario!.ActualizarUsuario(1, cliente!);
            cliente!.DireccionEntrega = "Julio Cesar 1247";
            Usuario resultado = manejadorUsuario!.ObtenerUsuario(1);

            // Assert
            Assert.AreEqual(cliente.DireccionEntrega, resultado.DireccionEntrega);
        }

        [TestMethod]
        public void AgregarCompraAlUsuarioOk()
        {
            mock!.Setup(x => x.ActualizarUsuario(cliente!));
            mock!.Setup(x => x.ObtenerUsuario(u => u.Id == 1)).Returns(cliente!);
            manejadorUsuario!.AgregarCompraAlUsuario(1, compra!);
            cliente!.Compras.Add(compra!);
            Usuario resultado = manejadorUsuario!.ObtenerUsuario(1);

            // Assert
            Assert.AreEqual(cliente.Compras, resultado.Compras);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AgregarCompraAlUsuarioSinProductos()
        {
            Compra compraVacia = new Compra();
            mock!.Setup(x => x.ActualizarUsuario(cliente!));
            manejadorUsuario!.AgregarCompraAlUsuario(1, compraVacia!);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarCompraAlUsuarioNula()
        {
            Compra? compraNula = null;
            mock!.Setup(x => x.ActualizarUsuario(cliente!));
            manejadorUsuario!.AgregarCompraAlUsuario(1, compraNula!);
        }

        [TestMethod]
        public void ObtenerComprasDelUsuarioOk()
        {
            mock!.Setup(x => x.ObtenerUsuario(c => c.Id == cliente!.Id)).Returns(cliente!);
            List<Compra> resultado = manejadorUsuario!.ObtenerComprasDelUsuario(1);
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
