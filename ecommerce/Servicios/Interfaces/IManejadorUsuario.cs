﻿using Dominio;
using Dominio.Usuario;

namespace Servicios.Interfaces
{
    public interface IManejadorUsuario
    {
        Usuario ObtenerUsuario(int id);
        Usuario RegistrarUsuario(Usuario usuario);
        List<Usuario> ObtenerUsuarios();
        void ActualizarUsuario(int id, string direccionEntrega);
        List<Compra> ObtenerComprasDelUsuario(int id);
        void AgregarCompraAlUsuario(int id, Compra compra);
        void EliminarUsuario(Usuario usuario);
    }
}
