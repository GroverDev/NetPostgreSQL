using Facturacion.Domain;

namespace Facturacion.Infrastructure;

public interface IParametrosRepository
{
    Task<bool> CreateParametro(Parametros parametro);   

    Task<Parametros> GetParametroByCodigo(int CodigoClasificador, string Descripcion, ParametrosEnum parametrosEnum);

    Task<bool> DisableAllParametros(ParametrosEnum parametrosEnum);

    Task<bool> EnableParametro(Parametros parametro);  
}
