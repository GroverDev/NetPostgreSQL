using AutoMapper;
using Common.Utilities;
using Common.Utilities.Exceptions;
using Facturacion.Domain;
using Facturacion.Infrastructure;
using Siat.Application;
using Siat.Codigos;

namespace Facturacion.Application;

public class CufdApplication(
    ICodigosApplication _codigosApplication, 
    ICufdRepository _cufdRepository,
    ISincronizacionRequestRepository _sincronizacionRequestRepository,
    ISincronizacionApplication _sincronizacionApplication,
    IPuntoVentaRepository _puntoVentaRepository,
    IMapper _mapper) : ICufdApplication
{
    public async Task<Response<bool>> UpdateCufd(int createdBy)
    {
        var response = new Response<bool>();
        var sinc = await _sincronizacionRequestRepository.GetSincronizacionRequest(0);
        try
        {
            var resp = await _codigosApplication.GetCUFD(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS);
            if (resp.Ok)
            { 
                var respFechaHora = await _sincronizacionApplication.GetFechaYHora(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS);
                if(!respFechaHora.Ok)   throw new CustomException("Error al obtener la hora del servidro del Siat");
                
                var respPuntoVenta = await _puntoVentaRepository.GetPuntoVentaByCodigo(sinc.CodigoPuntoVenta);
                
                var cufdNueva = _mapper.Map<Cufd>(resp.Data);
                cufdNueva.Id = Guid.NewGuid();
                cufdNueva.FechaInicio = Convert.ToDateTime(respFechaHora.Data);
                cufdNueva.Vigente = true;
                // Todo: Falta obetner el guid desde bd
                cufdNueva.IdPuntoVenta = respPuntoVenta.Id;
                cufdNueva.Created = cufdNueva.Modified = DateTime.Now;
                cufdNueva.CreatedBy = cufdNueva.ModifiedBy = createdBy;
                cufdNueva.State = true;
                await _cufdRepository.CreateCufd(cufdNueva);
            
                response.Ok = response.Data = true;
            }
        }
        catch (CustomException ex) { response.SetMessage(MessageTypes.Warning, ex.Message); }
        catch (Exception ex) { response.SetLogMessage(MessageTypes.Error, "Ocurrio un error, por favor comuniquese con Sistemas.", ex); }

        return response;    }
}
