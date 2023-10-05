﻿using System.Security.Claims;
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

    [HttpGet]
    public IActionResult RetornarTodas()
    {
        if (!EsAdministrador(HttpContext.User.Identity as ClaimsIdentity))
        {
            return Unauthorized();
        }
        List<Compra> listaCompras = _servicio.RetornarTodas();
        return Ok(listaCompras.Select(c => new CompraModelo(c)));
    }

    private bool EsAdministrador(ClaimsIdentity identity)
    {
        try
        { 
            string nombreRol = identity!.Claims.FirstOrDefault(x => x.Type == "rol")!.Value;
            CategoriaRol rolUsuario = Enum.Parse<CategoriaRol>(nombreRol);
            if (rolUsuario != CategoriaRol.Administrador && rolUsuario != CategoriaRol.ClienteAdministrador)
            {
                return false;
            }
            return true;
        }
        catch (Exception)
        {
            throw new UnauthorizedAccessException();
        }
    }
}