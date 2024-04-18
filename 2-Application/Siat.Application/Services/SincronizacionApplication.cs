using System.ServiceModel;
using Common.Utilities;
using Common.Utilities.Bases;
using Siat.Sincronizacion;

namespace Siat.Application;

public class SincronizacionApplication : ISincronizacionApplication
{
    public async Task<Response<bool>> OkComunnication()
    {
        var cliente = new ServicioFacturacionSincronizacionClient();

        var response = new Response<bool>();
        try
        {
            using (new OperationContextScope(cliente.InnerChannel))
            {
                SoapHeader.Create();
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
                    response.Message.SetMessage(MessageTypes.Error, "Error");
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

    public async Task<Response<string>> GetFechaYHora(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<string>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarFechaHoraAsync(solicitud);
                if (resp.RespuestaFechaHora.transaccion)
                {
                    response.Data = resp.RespuestaFechaHora.fechaHora;
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaFechaHora.mensajesList);

                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<actividadesDto>>> GetActividades(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<actividadesDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);
                SoapHeader.Create();    
                
                var resp = await client.sincronizarActividadesAsync(solicitud);
                if (resp.RespuestaListaActividades.transaccion)
                {
                    response.Data = resp.RespuestaListaActividades.listaActividades.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaActividades.mensajesList);

                }
            }
        }
        catch (System.Exception ex)
        {

            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<actividadesDocumentoSectorDto>>> GetActividadesDocumentoSector(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<actividadesDocumentoSectorDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarListaActividadesDocumentoSectorAsync(solicitud);
                if (resp.RespuestaListaActividadesDocumentoSector.transaccion)
                {
                    response.Data = resp.RespuestaListaActividadesDocumentoSector.listaActividadesDocumentoSector.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaActividadesDocumentoSector.mensajesList);

                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricaLeyendasDto>>> GetParametricasLeyendasFactura(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricaLeyendasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarListaLeyendasFacturaAsync(solicitud);
                if (resp.RespuestaListaParametricasLeyendas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricasLeyendas.listaLeyendas.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricasLeyendas.mensajesList);

                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricasDto>>> GetParametricasMensajesServicios(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarListaMensajesServiciosAsync(solicitud);
                if (resp.RespuestaListaParametricas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricas.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricas.mensajesList);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<productosDto>>> GetProductosServicios(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<productosDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarListaProductosServiciosAsync(solicitud);
                if (resp.RespuestaListaProductos.transaccion)
                {
                    response.Data = resp.RespuestaListaProductos.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaProductos.mensajesList);

                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricasDto>>> GetParametricasEventosSignificativos(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarParametricaEventosSignificativosAsync(solicitud);
                if (resp.RespuestaListaParametricas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricas.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricas.mensajesList);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricasDto>>> GetParametricasMotivoAnulacion(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarParametricaMotivoAnulacionAsync(solicitud);
                if (resp.RespuestaListaParametricas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricas.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricas.mensajesList);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricasDto>>> GetParametricasPaisOrigen(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarParametricaPaisOrigenAsync(solicitud);
                if (resp.RespuestaListaParametricas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricas.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricas.mensajesList);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricasDto>>> GetParametricasTipoDocumentoIdentidad(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarParametricaTipoDocumentoIdentidadAsync(solicitud);
                if (resp.RespuestaListaParametricas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricas.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricas.mensajesList);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricasDto>>> GetParametricasTipoDocumentoSector(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarParametricaTipoDocumentoSectorAsync(solicitud);
                if (resp.RespuestaListaParametricas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricas.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricas.mensajesList);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricasDto>>> GetParametricasTipoEmision(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarParametricaTipoEmisionAsync(solicitud);
                if (resp.RespuestaListaParametricas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricas.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricas.mensajesList);

                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricasDto>>> GetParametricasTipoHabitacion(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarParametricaTipoHabitacionAsync(solicitud);
                if (resp.RespuestaListaParametricas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricas.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricas.mensajesList);

                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricasDto>>> GetParametricasTipoMetodoPago(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarParametricaTipoMetodoPagoAsync(solicitud);
                if (resp.RespuestaListaParametricas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricas.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricas.mensajesList);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricasDto>>> GetParametricasTipoMoneda(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarParametricaTipoMonedaAsync(solicitud);
                if (resp.RespuestaListaParametricas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricas.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricas.mensajesList);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricasDto>>> GetParametricasTipoPuntoVenta(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarParametricaTipoPuntoVentaAsync(solicitud);
                if (resp.RespuestaListaParametricas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricas.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricas.mensajesList);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricasDto>>> GetParametricasTiposFactura(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarParametricaTiposFacturaAsync(solicitud);
                if (resp.RespuestaListaParametricas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricas.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricas.mensajesList);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
        return response;
    }

    public async Task<Response<List<parametricasDto>>> GetParametricasUnidadMedida(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
        var response = new Response<List<parametricasDto>>();
        var client = new ServicioFacturacionSincronizacionClient();
        try
        {
            using (new OperationContextScope(client.InnerChannel))
            {
                var solicitud = GetSolicitudSincronizacion(codigoPuntoVenta, codigoSucursal, cuis);

                var resp = await client.sincronizarParametricaUnidadMedidaAsync(solicitud);
                if (resp.RespuestaListaParametricas.transaccion)
                {
                    response.Data = resp.RespuestaListaParametricas.listaCodigos.ToList();
                    response.Ok = true;
                }
                else
                {
                    response.Errors = ConvertToBaseErrors(resp.RespuestaListaParametricas.mensajesList);
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
    private solicitudSincronizacion GetSolicitudSincronizacion(int codigoPuntoVenta, int codigoSucursal, string cuis)
    {
       
        var siat = new SiatParameters();
        var solicitud = new solicitudSincronizacion
        {
            nit = siat.nit,
            codigoAmbiente = siat.codigoAmbiente,
            codigoSistema = siat.codigoSistema,
            codigoPuntoVentaSpecified = true,
            codigoPuntoVenta = codigoPuntoVenta,
            codigoSucursal = codigoSucursal,
            cuis = cuis
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
