using System.ServiceModel;
using Common.Utilities;
using Common.Utilities.Bases;
using Facturacion.Domain;
using Microsoft.Extensions.Options;
using Siat.Codigos;

namespace Siat.Application;

public class CodigosApplication(IOptions<ConfigSiat> configSiat) : ICodigosApplication
{
    private readonly  ConfigSiat _configSiat = configSiat.Value;

    public async Task<Response<bool>> OkComunnication()
    {
        var cliente = new ServicioFacturacionCodigosClient();

        var response = new Response<bool>();
        try
        {
            using (new OperationContextScope(cliente.InnerChannel))
            {
                ApikeyHeader.Create(_configSiat.ApiKey);

                var responseVerificarComunicacion = await cliente.verificarComunicacionAsync();
                if (responseVerificarComunicacion.RespuestaComunicacion.transaccion)
                {
                    if (responseVerificarComunicacion.RespuestaComunicacion.mensajesList.Length > 0
                       && responseVerificarComunicacion.RespuestaComunicacion.mensajesList[0].descripcion == "COMUNICACION EXITOSA")
                    {
                        response.Data = response.Ok = true;
                    }
                }
                else
                {
                    response.Data = response.Ok = false;
                    response.SetMessage(MessageTypes.Error, "Error");
                }
            }
        }
        catch (TimeoutException timeProblem)
        {
            response.SetLogMessage(MessageTypes.Error, "Ocurrio un error, por favor comuniquese con la Unidad de Tecnología e Innovación de la CSBP", timeProblem);
        }
        catch (Exception ex)
        {
            response.SetLogMessage(MessageTypes.Error, "Ocurrio un error, por favor comuniquese con la Unidad de Tecnología e Innovación de la CSBP", ex);
        }

        return response;
    }

    public async Task<Response<respuestaCuis>> GetCUIS(int codigoPuntoVenta, int codigoSucursal)
    {
        var response = new Response<respuestaCuis>();
        var client = new ServicioFacturacionCodigosClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                
                var solicitud = GetSolicitudCUIS(codigoPuntoVenta, codigoSucursal);
                
                var resp = await client.cuisAsync(solicitud);
                if (resp.RespuestaCuis.transaccion)
                {
                    response.Data = resp.RespuestaCuis;
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaCuis.mensajesList);

                }
            }
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<respuestaCufd>> GetCUFD(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<respuestaCufd>();
        var client = new ServicioFacturacionCodigosClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                
                var solicitud = GetSolicitudCUFD(codigoPuntoVenta, codigoSucursal, cuis);
                
                var resp = await client.cufdAsync(solicitud);
                if (resp.RespuestaCufd.transaccion)
                {
                    response.Data = resp.RespuestaCufd;
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaCufd.mensajesList);

                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<respuestaVerificarNit>> GetVerificarNit(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<respuestaVerificarNit>();
        var client = new ServicioFacturacionCodigosClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                
                var solicitud = GetSolicitudVerificarNit(codigoPuntoVenta, codigoSucursal, cuis);
                
                var resp = await client.verificarNitAsync(solicitud);
                if (resp.RespuestaVerificarNit.transaccion)
                {
                    response.Data = resp.RespuestaVerificarNit;
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaVerificarNit.mensajesList);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<respuestaNotificaRevocado>> GetNotificaRevocacion(string certificado, int codigoSucursal, string cuis, DateTime fechaRevocacion, string razonRevocacion)
    {
        var response = new Response<respuestaNotificaRevocado>();
        var client = new ServicioFacturacionCodigosClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                
                var solicitud = GetSolicitudNotificaRevocacion(certificado, codigoSucursal, cuis, fechaRevocacion ,razonRevocacion);
                
                var resp = await client.notificaCertificadoRevocadoAsync(solicitud);
                if (resp.RespuestaNotificaRevocado.transaccion)
                {
                    response.Data = resp.RespuestaNotificaRevocado;
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaNotificaRevocado.mensajesList);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    #region Metodos Privados
    private solicitudCuis GetSolicitudCUIS(int codigoPuntoVenta, int codigoSucursal)
    {
        ApikeyHeader.Create(_configSiat.ApiKey);
        var solicitud = new solicitudCuis
        {
            codigoPuntoVenta = codigoPuntoVenta,
            codigoSucursal = codigoSucursal,
            //Del config
            nit = Convert.ToInt64(_configSiat.Nit),
            codigoAmbiente = Convert.ToInt32(_configSiat.CodigoAmbiente),
            codigoSistema = _configSiat.CodigoSistema,
            codigoModalidad = Convert.ToInt32(_configSiat.CodigoModalidad),
            codigoPuntoVentaSpecified = true
        };
        
        return solicitud;
    }
    private solicitudCufd GetSolicitudCUFD(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        ApikeyHeader.Create(_configSiat.ApiKey);
        var solicitud = new solicitudCufd
        {
            codigoPuntoVenta = codigoPuntoVenta,
            codigoSucursal = codigoSucursal,
            cuis = cuis,
            //Del config
            nit = Convert.ToInt64(_configSiat.Nit),
            codigoAmbiente = Convert.ToInt32(_configSiat.CodigoAmbiente),
            codigoSistema = _configSiat.CodigoSistema,
            codigoModalidad = Convert.ToInt32(_configSiat.CodigoModalidad),
            codigoPuntoVentaSpecified = true
        };
        
        return solicitud;
    }
    private solicitudVerificarNit GetSolicitudVerificarNit(int nitParaVerificacion, int codigoSucursal, string cuis)
    {
        ApikeyHeader.Create(_configSiat.ApiKey);
        var solicitud = new solicitudVerificarNit
        {
            codigoSucursal = codigoSucursal,
            cuis = cuis,
            nitParaVerificacion = nitParaVerificacion,
            //Del config
            nit = Convert.ToInt64(_configSiat.Nit),
            codigoAmbiente = Convert.ToInt32(_configSiat.CodigoAmbiente),
            codigoSistema = _configSiat.CodigoSistema,
            codigoModalidad = Convert.ToInt32(_configSiat.CodigoModalidad),
        };
        
        return solicitud;
    }
    private solicitudNotifcaRevocado GetSolicitudNotificaRevocacion(string certificado, int codigoSucursal, string cuis, DateTime fechaRevocacion, string razonRevocacion)
    {
        ApikeyHeader.Create(_configSiat.ApiKey);
        var solicitud = new solicitudNotifcaRevocado
        {
            codigoSucursal = codigoSucursal,
            cuis = cuis,
            certificado = certificado,
            fechaRevocacion = fechaRevocacion,
            razonRevocacion = razonRevocacion,
            //Del config
            nit = Convert.ToInt64(_configSiat.Nit),
            codigoAmbiente = Convert.ToInt32(_configSiat.CodigoAmbiente),
            codigoSistema = _configSiat.CodigoSistema,
            
        };
        
        return solicitud;
    }

    private List<BaseError> ConvertToBaseErrors(mensajeServicio[] mensajes)
    {
        var errors = new List<BaseError>();
        foreach (var mensaje in mensajes)
        {
            errors.Add(new BaseError()
            {
                PropertyName = mensaje.codigo.ToString(),
                ErrorMessage = mensaje.descripcion,
            });
        }
        return errors;
    }

    #endregion
}
