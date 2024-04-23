using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public interface IParametrosRepository
{
    Task<bool> CreateParametro(Parametros parametro);   

    Task<Parametros> GetParametroByCodigo(string codigoActividad, string descripcionLeyenda);

    Task<bool> DisableAllParametros();

    Task<bool> EnableParametro(Parametros parametro);  
}
