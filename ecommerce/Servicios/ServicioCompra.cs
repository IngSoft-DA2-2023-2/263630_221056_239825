using DataAccess.Interfaces;
using Dominio;
using Servicios.Interfaces;

namespace Servicios;

public class ServicioCompra : IServicioCompra
{
    private IRepositorioCompra _repositorio;

    public ServicioCompra(IRepositorioCompra repositorio)
    {
        _repositorio = repositorio;
    }

    public List<Compra> RetornarTodas() => _repositorio.RetornarTodas();
    public List<Compra> RetornarPorId(int id) => _repositorio.RetornarPorId(id);
}

