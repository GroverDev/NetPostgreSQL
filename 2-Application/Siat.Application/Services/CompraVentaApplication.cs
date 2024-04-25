using System.ServiceModel;
using Common.Utilities;
using Common.Utilities.Bases;
using Facturacion.Domain;
using Microsoft.Extensions.Options;
using Siat.CompraVenta;

namespace Siat.Application;

public class CompraVentaApplication(IOptions<ConfigSiat> configSiat) : ICompraVentaApplication
{
    private readonly  ConfigSiat _configSiat = configSiat.Value;

    public async Task<Response<bool>> OkComunnication()
    {
        var cliente = new ServicioFacturacionClient();

        var response = new Response<bool>();
        try
        {
            using (new OperationContextScope(cliente.InnerChannel))
            {
                ApikeyHeader.Create(_configSiat.ApiKey);

                var responseVerificarComunicacion = await cliente.verificarComunicacionAsync();
                if (responseVerificarComunicacion.@return.transaccion)
                {
                    if (responseVerificarComunicacion.@return.mensajesList.Length > 0
                       && responseVerificarComunicacion.@return.mensajesList[0].descripcion == "COMUNICACION EXITOSA")
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
    public async Task<Response<respuestaRecepcion>> GetRecepcionFactura(int codigoPuntoVenta, int codigoSucursal, string cufd, string cuis,
      byte[] archivo, int codigoDocumentoSector,int codigoEmision, 
      string fechaEnvio, string hashArchivo, int tipoFacturaDocumento)
    {
        var response = new Response<respuestaRecepcion>();
        var client = new ServicioFacturacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                
                var solicitud = GetSolicitudRecepcionFactura(codigoPuntoVenta, codigoSucursal, cufd, cuis, archivo, codigoDocumentoSector, codigoEmision, fechaEnvio, hashArchivo, tipoFacturaDocumento);
                
                var resp = await client.recepcionFacturaAsync(solicitud);
                if (resp.RespuestaServicioFacturacion.transaccion)
                {
                    response.Data = resp.RespuestaServicioFacturacion;
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaServicioFacturacion.mensajesList);
                }
            }
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<respuestaRecepcion>> GetAnulacionFactura(int codigoPuntoVenta, int codigoSucursal, string cufd, string cuis,
      string cuf, int codigoDocumentoSector,int codigoEmision, 
      string fechaEnvio, string hashArchivo, int tipoFacturaDocumento)
    {
        var response = new Response<respuestaRecepcion>();
        var client = new ServicioFacturacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                
                var solicitud = GetSolicitudAnulacionFactura(codigoPuntoVenta, codigoSucursal, cufd, cuis, cuf, codigoDocumentoSector, codigoEmision, tipoFacturaDocumento);
                
                var resp = await client.reversionAnulacionFacturaAsync(solicitud);
                if (resp.RespuestaServicioFacturacion.transaccion)
                {
                    response.Data = resp.RespuestaServicioFacturacion;
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaServicioFacturacion.mensajesList);
                }
            }
        }
        catch (System.Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }
    
    #region Metodos Privados
    private solicitudRecepcionFactura GetSolicitudRecepcionFactura
    ( int codigoPuntoVenta, int codigoSucursal, string cufd, string cuis,
      byte[] archivo, int codigoDocumentoSector,int codigoEmision, 
      string fechaEnvio, string hashArchivo, int tipoFacturaDocumento)
    {
        ApikeyHeader.Create(_configSiat.ApiKey);
        var solicitud = new solicitudRecepcionFactura
        {
            codigoPuntoVenta = codigoPuntoVenta,
            codigoSucursal = codigoSucursal,
            cufd = cufd,
            cuis = cuis,
            archivo = archivo,
            codigoDocumentoSector= codigoDocumentoSector,
            codigoEmision = codigoEmision,
            fechaEnvio = fechaEnvio,
            hashArchivo = hashArchivo,
            tipoFacturaDocumento = tipoFacturaDocumento,
            //Del config
            nit = Convert.ToInt64(_configSiat.Nit),
            codigoAmbiente = Convert.ToInt32(_configSiat.CodigoAmbiente),
            codigoSistema = _configSiat.CodigoSistema,
            codigoModalidad = Convert.ToInt32(_configSiat.CodigoModalidad),
            codigoPuntoVentaSpecified = true
        };
        
        return solicitud;
    }

    private solicitudReversionAnulacion GetSolicitudAnulacionFactura
    ( int codigoPuntoVenta, int codigoSucursal, string cufd, string cuis,
      string cuf, int codigoDocumentoSector,int codigoEmision, 
      int tipoFacturaDocumento)
    {
        ApikeyHeader.Create(_configSiat.ApiKey);
        var solicitud = new solicitudReversionAnulacion
        {
            codigoPuntoVenta = codigoPuntoVenta,
            codigoSucursal = codigoSucursal,
            cufd = cufd,
            cuis = cuis,
            codigoDocumentoSector= codigoDocumentoSector,
            codigoEmision = codigoEmision,
            tipoFacturaDocumento = tipoFacturaDocumento,
            cuf = cuf,
            //Del config
            nit = Convert.ToInt64(_configSiat.Nit),
            codigoAmbiente = Convert.ToInt32(_configSiat.CodigoAmbiente),
            codigoSistema = _configSiat.CodigoSistema,
            codigoModalidad = Convert.ToInt32(_configSiat.CodigoModalidad),
            codigoPuntoVentaSpecified = true
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
