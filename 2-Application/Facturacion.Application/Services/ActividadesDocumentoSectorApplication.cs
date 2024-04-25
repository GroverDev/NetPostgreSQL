using AutoMapper;
using Common.Utilities;
using Common.Utilities.Exceptions;
using Facturacion.Domain;
using Facturacion.Infrastructure;
using Siat.Application;

namespace Facturacion.Application;

public class ActividadesDocumentoSectorApplication(
    ISincronizacionApplication _sincronizacionApplication,
    IActividadesDocumentoSectorRepository _actividadesDocumentoSectorRepository,
    ISincronizacionRequestRepository _sincronizacionRequestRepository,
    IMapper _mapper
    ) : IActividadesDocumentoSectorApplication
{
    public async Task<Response<bool>> UpdateActividadesDocumentoSector(int createdBy)
    {
        var response = new Response<bool>();
        var sinc = await _sincronizacionRequestRepository.GetSincronizacionRequest(0);
        try
        {
            var resp = await _sincronizacionApplication.GetActividadesDocumentoSector(sinc.CodigoPuntoVenta, sinc.CodigoSucursal, sinc.CodigoCUIS);
            if (resp.Ok)
            {
                if (await _actividadesDocumentoSectorRepository.DisableAllActividadDocumentoSector())
                {
                    foreach (var actividadDocumentoSectorSiat in resp.Data)
                    {
                        var actividadDocumentoSectorDB = await _actividadesDocumentoSectorRepository.GetActividadDocumentoSectorByCodigo(actividadDocumentoSectorSiat.codigoDocumentoSector, actividadDocumentoSectorSiat.codigoActividad);
                        if (actividadDocumentoSectorDB.CodigoDocumentoSector == 0 && actividadDocumentoSectorDB.CodigoActividad == "")
                        {
                            var actividadDocumentoSector = _mapper.Map<ActividadesDocumentoSector>(actividadDocumentoSectorSiat);
                            actividadDocumentoSector.Id = Guid.NewGuid();
                            actividadDocumentoSector.Created = actividadDocumentoSector.Modified = DateTime.Now;
                            actividadDocumentoSector.CreatedBy = actividadDocumentoSector.ModifiedBy = createdBy;
                            actividadDocumentoSector.State = true;
                            await _actividadesDocumentoSectorRepository.CreateActividadDocumentoSector(actividadDocumentoSector);
                        } else {
                            actividadDocumentoSectorDB.Modified = DateTime.Now;
                            actividadDocumentoSectorDB.ModifiedBy = createdBy;
                            actividadDocumentoSectorDB.State = true;
                            await _actividadesDocumentoSectorRepository.EnableActividadDocumentoSector(actividadDocumentoSectorDB);
                        }
                    }
                    response.Ok = response.Data = true;
                } else throw new CustomException("No se pudo deshabiliatr los documentos sector");
            }
        }
        catch (CustomException ex) { response.SetMessage(MessageTypes.Warning, ex.Message); }
        catch (Exception ex) { response.SetLogMessage(MessageTypes.Error, "Ocurrio un error, por favor comuniquese con Sistemas.", ex); }

        return response;
    }
}
