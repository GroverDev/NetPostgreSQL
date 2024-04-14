using System.ServiceModel;
using Common.Utilities;
using Common.Utilities.Bases;
using Siat.Sincronizacion;
using static Common.Utilities.Message;

namespace Siat.Application;

public class SincronizacionApplication: ISincronizacionApplication
{
    public async Task<Response<List<actividadesDto>>> GetActividades(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<actividadesDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using(new OperationContextScope(client.InnerChannel)){
                SoapHeader.Create();
                var siat = new SiatParameters();
                var solicitud = new solicitudSincronizacion{
                    nit = siat.nit,
                    codigoAmbiente = siat.codigoAmbiente,
                    codigoSistema = siat.codigoSistema,
                    codigoPuntoVenta = codigoPuntoVenta,
                    codigoPuntoVentaSpecified = true,
                    codigoSucursal = codigoSucursal,
                    cuis = cuis,
                };

                var resp = await client.sincronizarActividadesAsync(solicitud);
                if(resp.RespuestaListaActividades.transaccion)
                {
                    response.Data = resp.RespuestaListaActividades.listaActividades.ToList();
                    response.Ok = true;
                }
                else {
                    response.Errors =ConvertToBaseErrors(resp.RespuestaListaActividades.mensajesList);
                    
                }
            }
        }
        catch (System.Exception ex)
        {
            
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async  Task<Response<bool>> OkComunnication()
    {
        var cliente = new ServicioFacturacionSincronizacionClient();
        
        var response = new Response<bool>();
        try
        {
            using (new OperationContextScope(cliente.InnerChannel))
            {
                SoapHeader.Create();
                var responseVerificarComunicacion =  await cliente.verificarComunicacionAsync();
                if(responseVerificarComunicacion.@return.transaccion){
                    if(responseVerificarComunicacion.@return.mensajesList.Length > 0 
                       && responseVerificarComunicacion.@return.mensajesList[0].descripcion== "COMUNICACION EXITOSA" )
                       {
                        response.Data = response.Ok= true;
                       }
                }
                else {
                    response.Data = response.Ok=false;
                    response.Message.SetMessage(MessageTypes.Error,"Error");
                }
            }
        }
        catch (TimeoutException timeProblem)
        {
            response.Message.SetLogMessage(MessageTypes.Error, "Ocurrio un error, por favor comuniquese con la Unidad de Tecnología e Innovación de la CSBP", timeProblem);
        }
        catch (Exception ex)
        {
            response.Message.SetLogMessage(MessageTypes.Error, "Ocurrio un error, por favor comuniquese con la Unidad de Tecnología e Innovación de la CSBP", ex);
        }

        return response;
    }

    private List<BaseError> ConvertToBaseErrors(mensajeServicio[] mensajes){
        var errors = new List<BaseError>();
        foreach (var mensaje in mensajes)
        {
            errors.Add(new BaseError(){
                PropertyName = mensaje.codigo.ToString(),
                ErrorMessage = mensaje.descripcion,
            });
        }
        return errors;
    }
}
