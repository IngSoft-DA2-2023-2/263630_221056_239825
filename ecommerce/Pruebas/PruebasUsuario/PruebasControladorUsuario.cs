using Dominio.Usuario;
using Moq;
using Servicios.Interfaces;
using Api.Controladores;
using Microsoft.AspNetCore.Mvc;
using Api.Dtos;

namespace Pruebas.PruebasUsuario
{
    [TestClass]
    public class PruebasControladorUsuario
    {
        [TestMethod]
        public void ObtenerTodosLosUsuariosOK()
        {
            // Arrange
            Usuario usuario = new("martin@edelman.com.uy", "Julio Cesar 123", "12345678");
            List<Usuario> usuarios = new List<Usuario>()
            {
                usuario
            };
            Mock<IManejadorUsuario> usuarioLogicMock = new Mock<IManejadorUsuario>();
            usuarioLogicMock.Setup(logic => logic.ObtenerUsuarios()).Returns(usuarios);

            ControladorUsuario controladorUsuario = new ControladorUsuario(usuarioLogicMock.Object);
            OkObjectResult expected = new OkObjectResult(new List<UsuarioCrearModelo>()
                { new UsuarioCrearModelo(){
                    CorreoElectronico = usuario.CorreoElectronico,
                    DireccionEntrega = usuario.DireccionEntrega,
                    contrasena = usuario.Contrasena
                }});
            List<UsuarioCrearModelo>? ObjetoEsperado = expected.Value as List<UsuarioCrearModelo>;
            // Act
            OkObjectResult? resultado = controladorUsuario.BuscarTodos() as OkObjectResult;
            List<Usuario>? objetoResultado = resultado!.Value as List<Usuario>;
            string mailEsperado = ObjetoEsperado![0].CorreoElectronico;
            string mailResultado = objetoResultado![0].CorreoElectronico;
            // Assert
            Assert.AreEqual(expected.StatusCode, resultado.StatusCode);
            Assert.AreEqual(mailEsperado, mailResultado);
        }
    }
}
