using System.ServiceModel;
using Common.Utilities;
using Siat.Application;
using Siat.Sincronizacion;
using static Common.Utilities.Message;
namespace Facturacion.Application;
 
public class SiatSincronizacionApplication : ISiatSincronizacionApplication
{
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
}


