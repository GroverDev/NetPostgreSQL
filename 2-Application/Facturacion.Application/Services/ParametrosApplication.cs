using AutoMapper;
using Common.Utilities;
using Common.Utilities.Exceptions;
using Facturacion.Domain;
using Facturacion.Infrastructure;
using Siat.Application;

namespace Facturacion.Application;

public class ParametrosApplication(
    IParametrosRepository _parametrosRepository,
    ISincronizacionApplication _sincronizacionApplication,
    IMapper _mapper
) : IParametrosApplication
{
    public async Task<Response<bool>> UpdateParametros(int createdBy)
    {
        var response = new Response<bool>();
        int codigoPutnoDeVenta = 0;
        int codigoSucursal = 5;
        string codigoCuis = "E8465CDD";
        try
        {
            var resp = await _sincronizacionApplication.GetParametricasMensajesServicios(codigoPutnoDeVenta, codigoSucursal, codigoCuis);
            if (resp.Ok)
            {
                if (await _parametrosRepository.DisableAllParametros())
                {
                    foreach (var parametrosSiat in resp.Data)
                    {
                        var parametroDB = await _parametrosRepository.GetParametroByCodigo("", "");
                        if (parametroDB.CodigoClasificador == "")
                        {
                            var parametroNuevo = _mapper.Map<Parametros>(parametrosSiat);
                            parametroNuevo.Id = Guid.NewGuid();
                            parametroNuevo.Created = parametroNuevo.Modified = DateTime.Now;
                            parametroNuevo.CreatedBy = parametroNuevo.ModifiedBy = createdBy;
                            parametroNuevo.State = true;
                            await _parametrosRepository.CreateParametro(parametroNuevo);
                        }
                        else
                        {
                            parametroDB.Modified = DateTime.Now;
                            parametroDB.ModifiedBy = createdBy;
                            parametroDB.State = true;
                            await _parametrosRepository.EnableParametro(parametroDB);
                        }
                    }
                    response.Ok = response.Data = true;
                }
                else throw new CustomException("No se pudo deshabilitar las leyendas");
            }
        }
        catch (CustomException ex) { response.SetMessage(MessageTypes.Warning, ex.Message); }
        catch (Exception ex) { response.SetLogMessage(MessageTypes.Error, "Ocurrio un error, por favor comuniquese con Sistemas.", ex); }

        return response;

    }
}
