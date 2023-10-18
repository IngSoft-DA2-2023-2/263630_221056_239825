using System.Security.Claims;
using Api.Filtros;
using Dominio;
using Dominio.Usuario;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using Servicios.Interfaces;

namespace Api.Dtos;

[ApiController]
[Route("api/v1/compras")]
public class ControladorCompra : ControllerBase
{
    private IServicioCompra _servicio;

    public ControladorCompra(IServicioCompra servicio)
    {
        _servicio = servicio;
    }

    [FiltroAutorizacionRol(RoleNeeded = CategoriaRol.Administrador, SecondaryRole = CategoriaRol.ClienteAdministrador)]
    [HttpGet]
    public IActionResult RetornarTodas()
    {
        List<Compra> listaCompras = _servicio.RetornarTodas();
        return Ok(listaCompras.Select(c => new CompraModelo(c)));
    }
}