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
    ISincronizacionRequestRepository _sincronizacionRequestRepository,
    IMapper _mapper
) : IParametrosApplication
{
    public async Task<Response<bool>> UpdateParametros(int createdBy)
    {
        var response = new Response<bool>();
        var sinc = await _sincronizacionRequestRepository.GetSincronizacionRequest(0);
        try
        {
            var respParametrosSiat = new Response<List<Siat.Sincronizacion.parametricasDto>>();
            foreach (ParametrosEnum parametroEnum in Enum.GetValues(typeof(ParametrosEnum)))
            {
                respParametrosSiat = parametroEnum switch
                {
                    ParametrosEnum.MENSAJES_SERVICIO => await _sincronizacionApplication.GetParametricasMensajesServicios(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                    ParametrosEnum.EVENTOS_SIGNIFICATIVOS => await _sincronizacionApplication.GetParametricasEventosSignificativos(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                    ParametrosEnum.MOTIVO_ANULACION => await _sincronizacionApplication.GetParametricasMotivoAnulacion(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                    ParametrosEnum.PAIS_ORIGEN => await _sincronizacionApplication.GetParametricasPaisOrigen(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                    ParametrosEnum.TIPO_DOCUMENTO_IDENTIDAD => await _sincronizacionApplication.GetParametricasTipoDocumentoIdentidad(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                    ParametrosEnum.TIPO_DOCUMENTO_SECTOR => await _sincronizacionApplication.GetParametricasTipoDocumentoSector(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                    ParametrosEnum.TIPO_EMISION => await _sincronizacionApplication.GetParametricasTipoEmision(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                    ParametrosEnum.TIPO_HABITACION => await _sincronizacionApplication.GetParametricasTipoHabitacion(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                    ParametrosEnum.TIPO_METODO_PAGO => await _sincronizacionApplication.GetParametricasTipoMetodoPago(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                    ParametrosEnum.TIPO_MONEDA => await _sincronizacionApplication.GetParametricasTipoMoneda(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                    ParametrosEnum.TIPO_PUNTO_VENTA => await _sincronizacionApplication.GetParametricasTipoPuntoVenta(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                    ParametrosEnum.TIPOS_FACTURA => await _sincronizacionApplication.GetParametricasTiposFactura(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                    ParametrosEnum.UNIDAD_MEDIDA => await _sincronizacionApplication.GetParametricasTiposFactura(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                    _ => await _sincronizacionApplication.GetParametricasMensajesServicios(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS),
                };
                if (respParametrosSiat.Ok)
                {
                    if (await _parametrosRepository.DisableAllParametros(parametroEnum))
                    {
                        foreach (var parametrosSiat in respParametrosSiat.Data)
                        {
                            var parametroDB = await _parametrosRepository.GetParametroByCodigo(parametrosSiat.codigoClasificador, parametrosSiat.descripcion, parametroEnum);
                            if (parametroDB.CodigoClasificador ==0)
                            {
                                var parametroNuevo = _mapper.Map<Parametros>(parametrosSiat);
                                parametroNuevo.CodigoTipoParametro = Enum.GetName(parametroEnum);
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


        }
        catch (CustomException ex) { response.SetMessage(MessageTypes.Warning, ex.Message); }
        catch (Exception ex) { response.SetLogMessage(MessageTypes.Error, "Ocurrio un error, por favor comuniquese con Sistemas.", ex); }

        return response;

    }
}
