using Dominio.Usuario;
using Moq;
using Servicios.Interfaces;
using Api.Controladores;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using Dominio;

namespace Pruebas.PruebasUsuario
{
    [TestClass]
    public class PruebasControladorUsuario
    {
        private Mock<IManejadorUsuario> usuarioLogicMock = new Mock<IManejadorUsuario>();
        private Mock<IServicioProducto> productoLogicMock = new Mock<IServicioProducto>();
        private Mock<IServicioCompra> compraLogicMock = new Mock<IServicioCompra>();
        private Usuario usuario = new("martin@edelman.com.uy", "Julio Cesar 123", "12345678")
        {
            Id = 1,
            Rol = CategoriaRol.Cliente
        };
        private ControladorUsuario? controladorUsuario;
        private List<Usuario>? usuarios;
        [TestInitialize]
        public void InitTest()
        {
            usuarios = new List<Usuario>()
            {
                usuario
            };
            usuarioLogicMock.Setup(logic => logic.ObtenerUsuario(1)).Returns(usuario);
            usuarioLogicMock.Setup(logic => logic.ObtenerUsuarios()).Returns(usuarios);
            controladorUsuario = new ControladorUsuario(usuarioLogicMock.Object, productoLogicMock.Object, compraLogicMock.Object);
        }

        [TestMethod]
        public void ObtenerTodosLosUsuariosOK()
        {
            // Arrange
            OkObjectResult expected = new OkObjectResult(new List<UsuarioCrearModelo>()
                { new UsuarioCrearModelo(){
                    CorreoElectronico = usuario.CorreoElectronico,
                    DireccionEntrega = usuario.DireccionEntrega,
                    Contrasena = usuario.Contrasena
                }});
            List<UsuarioCrearModelo>? ObjetoEsperado = expected.Value as List<UsuarioCrearModelo>;
            // Act
            OkObjectResult? resultado = controladorUsuario!.BuscarTodos() as OkObjectResult;
            List<Usuario>? objetoResultado = resultado!.Value as List<Usuario>;
            string mailEsperado = ObjetoEsperado![0].CorreoElectronico;
            string mailResultado = objetoResultado![0].CorreoElectronico;
            // Assert
            Assert.AreEqual(expected.StatusCode, resultado.StatusCode);
        }

        [TestMethod]
        public void ObtenerUnUsuarioPorSuIdOk()
        {
            // Arrange
            OkObjectResult expected = new OkObjectResult(new UsuarioCrearModelo()
            {
                CorreoElectronico = usuario.CorreoElectronico,
                DireccionEntrega = usuario.DireccionEntrega,
                Contrasena = usuario.Contrasena,
                Rol = CategoriaRol.Cliente
            });
            UsuarioCrearModelo? objetoEsperado = expected.Value as UsuarioCrearModelo;
            // Act
            OkObjectResult? resultado = controladorUsuario!.BuscarPorId(1) as OkObjectResult;
            Usuario? objetoResultado = resultado!.Value as Usuario;
            string mailEsperado = objetoEsperado!.CorreoElectronico;
            string mailResultado = objetoResultado!.CorreoElectronico;
            // Assert
            Assert.AreEqual(expected.StatusCode, resultado.StatusCode);
        }

        [TestMethod]
        public void PostUsuarioClienteOk()
        {
            UsuarioCrearModelo usuarioEsperado = new UsuarioCrearModelo()
            {
                CorreoElectronico = "correo@example.com",
                DireccionEntrega = "Dirección de entrega",
                Contrasena = "contrasena"
            };

            usuarioLogicMock.Setup(logic => logic.RegistrarUsuario(usuario)).Returns(usuario);
            CreatedResult? expected = new CreatedResult("", usuarioEsperado);
            CreatedResult? resultado = controladorUsuario!.RegistrarUsuario(usuarioEsperado) as CreatedResult;
            UsuarioCrearModelo? objetoResultado = resultado!.Value as UsuarioCrearModelo;

            Assert.AreEqual(expected.StatusCode, resultado.StatusCode);
        }

        [TestMethod]
        public void PutActualizarDireccion()
        {
            // Arrange
            UsuarioCrearModelo expected = new UsuarioCrearModelo()
            {
                CorreoElectronico = "correo@example.com",
                DireccionEntrega = "Dirección de entrega NUEVA",
                Contrasena = "contrasena",
                Rol = CategoriaRol.Administrador
            };
            usuarioLogicMock.Setup(logic => logic.ActualizarUsuario(1, usuario));
            OkResult esperado = new OkResult();
            OkResult? resultado = controladorUsuario!.ModificarUsuario(1, expected) as OkResult;

            Assert.AreEqual(esperado.StatusCode, resultado!.StatusCode);
        }

        [TestMethod]
        public void DeleteUsuario()
        {
            // Arrange
            usuarioLogicMock.Setup(logic => logic.EliminarUsuario(usuario));
            OkResult esperado = new OkResult();
            OkResult? resultado = controladorUsuario!.EliminarUsuario(1) as OkResult;
            Assert.AreEqual(esperado.StatusCode, resultado!.StatusCode);
        }

        [TestMethod]
        public void BuscarComprasOk()
        {
            List<Compra> comprasEsperadas = new List<Compra>()
            {
                new Compra()
                {
                    Id = 1,
                    UsuarioId = 1,
                }
            };
            compraLogicMock.Setup(logic => logic.RetornarPorId(1)).Returns(comprasEsperadas);
            OkObjectResult esperado = new OkObjectResult(comprasEsperadas);
            OkObjectResult? resultado = controladorUsuario!.BuscarCompras(1) as OkObjectResult;

            Assert.AreEqual(esperado.StatusCode, resultado!.StatusCode);
        }

        [TestMethod]
        public void RealizarCompraOk()
        {
            CompraCrearModelo esperado = new CompraCrearModelo()
            {
                idProductos = new int[]{ 1, 2 }
            };
            productoLogicMock.Setup(l => l.EncontrarPorId(1)).Returns(new Producto());
            productoLogicMock.Setup(l => l.EncontrarPorId(2)).Returns(new Producto());
            usuarioLogicMock.Setup(l => l.AgregarCompraAlUsuario(1, new Compra()));

            CreatedResult expected = new CreatedResult("", esperado);
            CreatedResult? resultado = controladorUsuario!.RealizarCompra(1, esperado) as CreatedResult;

            Assert.AreEqual(expected.StatusCode, resultado!.StatusCode);
        }
    }
}
